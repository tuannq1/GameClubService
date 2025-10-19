var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/clubs", async Task<Results<Created<object>, Conflict<string>>> (
    IClubRepository repo,
    ClubRequest req) =>
{
    var result = await repo.CreateClubAsync(req.Name, req.Description);

    return result switch
    {
        null => TypedResults.Conflict("Club already exists."),
        Guid id => TypedResults.Created($"/clubs/{id}", new { Id = id, req.Name, req.Description })
    };
});

app.MapGet("/clubs", async Task<Ok<IEnumerable<object>>> (
    IClubRepository repo,
    string? search) =>
{
    var clubs = await repo.GetClubsAsync(search);
    var response = clubs.Select(c => new { c.Id, c.Name, c.Description });
    return TypedResults.Ok(response);
});

app.MapPost("/clubs/{id:guid}/events", async Task<Results<Created<object>, NotFound<string>>> (
    IClubRepository repo,
    Guid id,
    EventRequest req) =>
{
    var evt = await repo.CreateEventAsync(id, req.Title, req.Description, req.ScheduledAt);

    return evt switch
    {
        null => TypedResults.NotFound("Club not found."),
        var e => TypedResults.Created($"/clubs/{id}/events/{e.Id}", new { e.Id, e.Title, e.Description, e.ScheduledAt })
    };
});

app.MapGet("/clubs/{id:guid}/events", async Task<Results<Ok<IEnumerable<object>>, NotFound<string>>> (
    IClubRepository repo,
    Guid id) =>
{
    var eventsList = await repo.GetEventsAsync(id);

    return eventsList.Any() switch
    {
        false => TypedResults.NotFound("Club not found or has no events."),
        true => TypedResults.Ok(eventsList.Select(e => new { e.Id, e.Title, e.ScheduledAt }))
    };
});

app.Run();

record ClubRequest(string Name, string Description);
record EventRequest(string Title, string Description, DateTime ScheduledAt);
