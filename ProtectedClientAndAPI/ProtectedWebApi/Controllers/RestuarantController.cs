using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using ProtectedWebApi.DomainLogic;
using Shared.Models;

namespace ProtectedWebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class RestuarantController(ILogger<RestuarantController> log, IRestuarantLogic logic) : ControllerBase
{
    private readonly ILogger<RestuarantController> logger = log;
    private readonly IRestuarantLogic restuarantLogic = logic;

    /// <summary>
    /// Get All Restuarants
    /// </summary>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpGet]
    public async Task<IResult> Get()
    {
        logger.LogInformation("Get all restuarants request received");
        List<Restuarant> restuarants = await restuarantLogic.GetAllRestuarants();

        logger.LogInformation("Get all restuarants request complete...returning results");
        return TypedResults.Ok(restuarants);
    }

    /// <summary>
    /// Find Restuarants using matching criteria from query strings
    /// </summary>
    /// <param name="search">Object containing properties for search parameters</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpPost("find")]
    public async Task<IResult> Find([FromBody] SearchCriteria search)
    {
        logger.LogInformation("Find restuarants request received");
        List<Restuarant> restuarants = await restuarantLogic.FindRestuarants(search.Name, search.Cuisine);

        logger.LogInformation("Find restuarants request complete...returning results");
        return TypedResults.Ok(restuarants);
    }

    /// <summary>
    /// Get a Restuarant using the provided id
    /// </summary>
    /// <param name="id">Unique Identifier for a restuarant</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpGet("{id}")]
    public async Task<IResult> Restuarant(string id)
    {
        logger.LogInformation("Get restuarant request received");
        Restuarant restuarant = await restuarantLogic.GetRestuarant(id);

        if (restuarant is null || string.IsNullOrWhiteSpace(restuarant.Id))
        {
            return TypedResults.NotFound();
        }

        logger.LogInformation("Get restuarant request complete...returning results");
        return TypedResults.Ok(restuarant);
    }

    /// <summary>
    /// Inserts a new restuarant
    /// </summary>
    /// <param name="restuarant">Restuarant object to insert</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpPost]
    public async Task<IResult> Post([FromBody] Restuarant restuarant)
    {
        logger.LogInformation("Add restuarant request received");
        bool success = await restuarantLogic.InsertRestuarant(restuarant);

        logger.LogInformation("Add restuarant request complete...returning results");
        return TypedResults.Created("api/restuarant", success);
    }

    /// <summary>
    /// Inserts many new restuarants
    /// </summary>
    /// <param name="restuarants">Restuarant array with many items to insert</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpPost("bulk")]
    public async Task<IResult> PostMany([FromBody] Restuarant[] restuarants)
    {
        logger.LogInformation("Add restuarant request received");
        bool success = await restuarantLogic.InsertRestuarants(restuarants);

        logger.LogInformation("Add restuarant request complete...returning results");
        return TypedResults.Created("api/restuarant/bulk", success);
    }

    /// <summary>
    /// Updates an existing restuarant
    /// </summary>
    /// <param name="restuarant">Restuarant object to update</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpPut]
    public async Task<IResult> Put([FromBody] Restuarant restuarant)
    {
        logger.LogInformation("Update restuarant request received");
        bool success = await restuarantLogic.UpdateRestuarant(restuarant);

        logger.LogInformation("Update restuarant request complete...returning results");
        return TypedResults.Ok(success);
    }
}
