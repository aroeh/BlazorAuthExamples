using MongoDB.Driver;
using ProtectedWebApi.Constants;
using Protected.Shared.Models;

namespace ProtectedWebApi.DataAccess;

public class RestuarantData(ILogger<RestuarantData> log, IMongoDbWrapper db) : IRestuarantData
{
    private readonly ILogger<RestuarantData> logger = log;
    private readonly IMongoDbWrapper mongo = db;


    /// <summary>
    /// Returns a list of all restuarants in the database
    /// </summary>
    /// <returns>Collection of available restuarant records.  Returns empty list if there are no records</returns>
    public async Task<List<Restuarant>> GetAllRestuarants()
    {
        FilterDefinitionBuilder<Restuarant> builder = Builders<Restuarant>.Filter;
        var filter = builder.Where(d => true);

        logger.LogInformation("Finding all restuarants");
        return await mongo.FindMany<Restuarant>(DataAccessConstants.MongoCollection, filter);
    }

    /// <summary>
    /// Simple method for finding restuarants by name and type of cuisine.
    /// This could be enhanced to include more criteria like location
    /// </summary>
    /// <param name="name">Search Parameter on the Restuarant Name</param>
    /// <param name="cuisine">Search Parameter on the Restuarant CuisineType</param>
    /// <returns>Collection of available restuarant records.  Returns empty list if there are no records found matching criteria</returns>
    public async Task<List<Restuarant>> FindRestuarants(string name, string cuisine)
    {
        FilterDefinitionBuilder<Restuarant> builder = Builders<Restuarant>.Filter;
        var filter = builder.Where(d => d.Name.Contains(name) && d.CuisineType == cuisine);

        logger.LogInformation("Finding restuarants by name and cuisine type");
        return await mongo.FindMany<Restuarant>(DataAccessConstants.MongoCollection, filter);
    }

    /// <summary>
    /// Retrieves a restuarant record based on the matching id
    /// </summary>
    /// <param name="id">Unique Identifier for a restuarant</param>
    /// <returns>Restuarant record if found.  Returns new Restuarant if not found</returns>
    public async Task<Restuarant> GetRestuarant(string id)
    {
        FilterDefinitionBuilder<Restuarant> builder = Builders<Restuarant>.Filter;
        var filter = builder.Eq(d => d.Id, id);

        logger.LogInformation("Finding restuarant by id");
        return await mongo.FindOne<Restuarant>(DataAccessConstants.MongoCollection, filter);
    }

    /// <summary>
    /// Inserts a new Restuarant Record
    /// </summary>
    /// <param name="restuarant">Restuarant object to insert</param>
    /// <returns>Restuarant object updated with the new id</returns>
    public async Task<Restuarant> InsertRestuarant(Restuarant restuarant)
    {
        logger.LogInformation("Adding new restuarant");
        Restuarant newRestuarant = await mongo.InsertOne<Restuarant>(DataAccessConstants.MongoCollection, restuarant);

        return newRestuarant;
    }

    /// <summary>
    /// Inserts many new Restuarant Records
    /// </summary>
    /// <param name="restuarants">Array of new restuarant objects to add</param>
    /// <returns>Restuarant objects updated with the new id</returns>
    public async Task<Restuarant[]> InsertRestuarants(Restuarant[] restuarants)
    {
        logger.LogInformation("Adding new restuarants");
        IEnumerable<Restuarant> newRestuarants = await mongo.InsertMany<Restuarant>(DataAccessConstants.MongoCollection, restuarants);
        return newRestuarants.ToArray();
    }

    /// <summary>
    /// Updates and existing restuarant record
    /// </summary>
    /// <param name="restuarant">Restuarant object to update</param>
    /// <returns>MongoDb replace results for the update operation</returns>
    public async Task<MongoUpdateResult> UpdateRestuarant(Restuarant restuarant)
    {
        FilterDefinitionBuilder<Restuarant> builder = Builders<Restuarant>.Filter;
        var filter = builder.Eq(d => d.Id, restuarant.Id);

        logger.LogInformation("replacing restuarant document");
        return await mongo.ReplaceOne<Restuarant>(DataAccessConstants.MongoCollection, filter, restuarant);
    }
}
