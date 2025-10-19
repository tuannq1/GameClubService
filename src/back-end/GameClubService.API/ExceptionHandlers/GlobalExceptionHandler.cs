using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace GameClubService.API.ExceptionHandlers;

public static class GlobalExceptionHandler
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionHandler?.Error;

                context.Response.ContentType = "application/json";

                switch (exception)
                {
                    case ArgumentException argEx:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            error = "Invalid argument.",
                            message = argEx.Message
                        });
                        break;

                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            error = "An unexpected error occurred.",
                            message = exception?.Message
                        });
                        break;
                }
            });
        });
    }
}
