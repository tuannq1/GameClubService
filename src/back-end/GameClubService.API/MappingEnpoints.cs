using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using GameClubService.Domain.Interfaces;
using GameClubService.Domain.Common;

namespace GameClubService.API;

public static class ClubsEndpoints
{
    public record ClubRequest(string Name, string Description);
    public record EventRequest(string Title, string Description, DateTime ScheduledAt);

    public static async Task<Results<
        Created,
        Conflict<string>,
        ProblemHttpResult
    >> CreateClubApi(
        [FromServices] IClubRepository repo,
        [FromBody] ClubRequest req)
    {
        var (id, status) = await repo.CreateClubAsync(req.Name, req.Description);

        return status switch
        {
            PersistenceStatusEnum.Conflict => TypedResults.Conflict("Club already exists."),
            PersistenceStatusEnum.Success => TypedResults.Created($"/clubs/{id}"),
            _ => TypedResults.Problem("Unexpected error occured when trying to create new club")
        };
    }

    public static async Task<Results<
        Ok<IEnumerable<object>>,
        ProblemHttpResult
    >> GetClubsApi(
        [FromServices] IClubRepository repo,
        [FromQuery] string? search)
    {
        var (clubs, status) = await repo.GetClubsAsync(search);
        var response = clubs?.Select(c => new { c.Id, c.Name, c.Description }).AsEnumerable<object>();

        return status switch
        {
            PersistenceStatusEnum.Success => TypedResults.Ok(response),
            _ => TypedResults.Problem("Unexpected error occured when trying to get list of clubs")
        };
    }

    public static async Task<Results<
        Created,
        NotFound<string>,
        BadRequest<string>,
        ProblemHttpResult
    >> CreateEventApi(
        [FromServices] IClubRepository repo,
        [FromRoute] Guid clubId,
        [FromBody] EventRequest req)
    {
        var (evt, status) = await repo.CreateEventAsync(clubId, req.Title, req.Description, req.ScheduledAt);
        return status switch
        {
            PersistenceStatusEnum.NotFound => TypedResults.NotFound("Club not found."),
            PersistenceStatusEnum.Success => TypedResults.Created($"/clubs/{clubId}/events/{evt.Id}"),
            _ => TypedResults.Problem("Unexpected error occured when trying to create new event")
        };
    }

    public static async Task<Results<
        Ok<IEnumerable<object>>,
        NotFound<string>,
        ProblemHttpResult
    >> GetEventsApi(
        [FromServices] IClubRepository repo,
        [FromRoute] Guid clubId)
    {
        var (events, status) = await repo.GetEventsAsync(clubId);
        return status switch
        {
            PersistenceStatusEnum.NotFound => TypedResults.NotFound("Club not found"),
            PersistenceStatusEnum.Success when events == null || !events.Any() => TypedResults.NotFound("Club has no events"),
            PersistenceStatusEnum.Success=> TypedResults.Ok(events.Select(e => new { e.Id, e.Title, e.ScheduledAt }).AsEnumerable<object>()),
            _ => TypedResults.Problem("Unexpected error occured when trying to get list of events")
        };
    }

    public static WebApplication MapClubEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/clubs").WithTags("ClubsEndpoints");

        group.MapPost("/", CreateClubApi)
            .WithOpenApi(op =>
            {
                op.Summary = "Create a new club.";
                return op;
            });

        group.MapGet("/", GetClubsApi)
            .WithOpenApi(op =>
            {
                op.Summary = "Retrieve all clubs, optionally filtered by search.";
                return op;
            });

        group.MapPost("/{clubId:guid}/events", CreateEventApi)
            .WithOpenApi(op =>
            {
                op.Summary = "Create a new event for a club.";
                return op;
            });

        group.MapGet("/{clubId:guid}/events", GetEventsApi)
            .WithOpenApi(op =>
            {
                op.Summary = "Retrieve all events for a specific club.";
                return op;
            });

        return app;
    }
}
