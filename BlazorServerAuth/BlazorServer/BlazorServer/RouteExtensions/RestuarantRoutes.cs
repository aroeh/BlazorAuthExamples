using BlazorServer.Client.HttpClientHelper;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace BlazorServer.RouteExtensions;

public static class RestuarantRoutes
{
    public static void MapRestuarantRoutes(this WebApplication app)
    {
        // setup the versioned api and base route with version
        var routeGroup = app.MapGroup("/restuarant");

        // map endpoints for the group

        // Get All Restuarants
        routeGroup.MapGet("/", ([FromServices] IRestuarantService restuarantService) =>
        {
            return restuarantService.Get();
        }).RequireAuthorization();

        // Get a Restuarant using the provided id
        routeGroup.MapGet("/{id}", ([FromServices] IRestuarantService restuarantService, string id) =>
        {
            return restuarantService.Get(id);
        }).RequireAuthorization();

        // Inserts a new restuarant
        routeGroup.MapPost("/", ([FromServices] IRestuarantService restuarantService, [FromBody] Restuarant restuarant) =>
        {
            return restuarantService.Create(restuarant);
        }).RequireAuthorization();

        // Updates an existing restuarant
        routeGroup.MapPut("/", ([FromServices] IRestuarantService restuarantService, [FromBody] Restuarant restuarant) =>
        {
            return restuarantService.Update(restuarant);
        }).RequireAuthorization();
    }
}
