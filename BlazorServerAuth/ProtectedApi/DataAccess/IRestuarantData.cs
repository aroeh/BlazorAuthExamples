using Shared.Models;

namespace ProtectedApi.DataAccess;

public interface IRestuarantData
{
    /// <summary>
    /// Returns a collection of all restuarants in the database
    /// </summary>
    /// <returns>Collection of available restuarant records.  Returns empty list if there are no records</returns>
    Task<List<Restuarant>> GetAllRestuarants();

    /// <summary>
    /// Simple method for finding restuarants by name and type of cuisine.
    /// This could be enhanced to include more criteria like location
    /// </summary>
    /// <param name="name">Search Parameter on the Restuarant Name</param>
    /// <param name="cuisine">Search Parameter on the Restuarant CuisineType</param>
    /// <returns>Collection of available restuarant records.  Returns empty list if there are no records found matching criteria</returns>
    Task<List<Restuarant>> FindRestuarants(string name, string cuisine);

    /// <summary>
    /// Retrieves a restuarant record based on the matching id
    /// </summary>
    /// <param name="id">Unique Identifier for a restuarant</param>
    /// <returns>Restuarant record if found.  Returns new Restuarant if not found</returns>
    Task<Restuarant> GetRestuarant(string id);

    /// <summary>
    /// Inserts a new Restuarant Record
    /// </summary>
    /// <param name="restuarant">Restuarant object to insert</param>
    /// <returns>Restuarant object updated with the new id</returns>
    Task<Restuarant> InsertRestuarant(Restuarant restuarant);

    /// <summary>
    /// Inserts many new Restuarant Records
    /// </summary>
    /// <param name="restuarants">Array of new restuarant objects to add</param>
    /// <returns>Restuarant objects updated with the new id</returns>
    Task<Restuarant[]> InsertRestuarants(Restuarant[] restuarants);

    /// <summary>
    /// Updates and existing restuarant record
    /// </summary>
    /// <param name="restuarant">Restuarant object to update</param>
    /// <returns>MongoDb replace results for the update operation</returns>
    Task<MongoUpdateResult> UpdateRestuarant(Restuarant restuarant);
}
