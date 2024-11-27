using MongoDB.Driver;
using Shared.Models;

namespace ProtectedWebApi.DataAccess;

public interface IMongoDbWrapper
{
    /// <summary>
    /// Finds items in the specified collection using a filter definition
    /// </summary>
    /// <typeparam name="T">Generic Type for the input and output objects</typeparam>
    /// <param name="collectionName">MongoDb Collection Name</param>
    /// <param name="filter">Expression Filter to match documents</param>
    /// <returns>Task List of Generic Type T</returns>
    Task<List<T>> FindMany<T>(string collectionName, FilterDefinition<T> filter);

    /// <summary>
    /// Finds one item in the specified collection using a filter definition
    /// </summary>
    /// <typeparam name="T">Generic Type for the input and output objects</typeparam>
    /// <param name="collectionName">MongoDb Collection Name</param>
    /// <param name="filter">Expression Filter to match documents</param>
    /// <returns>Task of Generic Type T</returns>
    Task<T> FindOne<T>(string collectionName, FilterDefinition<T> filter);

    /// <summary>
    /// Insert a new document into the specified collection
    /// </summary>
    /// <typeparam name="T">Generic Type for the input and output objects</typeparam>
    /// <param name="collectionName">MongoDb Collection Name</param>
    /// <param name="document">New Document to be inserted</param>
    /// <returns>Task of Generic Type T</returns>
    Task<T> InsertOne<T>(string collectionName, T document);

    /// <summary>
    /// Insert multiple new documents into the specified collection
    /// </summary>
    /// <typeparam name="T">Generic Type for the input and output objects</typeparam>
    /// <param name="collectionName">MongoDb Collection Name</param>
    /// <param name="documents">New Document to be inserted</param>
    /// <returns>Task Enumerable of T with updated ids from the insert operation</returns>
    Task<IEnumerable<T>> InsertMany<T>(string collectionName, IEnumerable<T> documents);

    /// <summary>
    /// Replaces a document with a later version using a filter to match the record
    /// </summary>
    /// <typeparam name="T">Generic Type for the input and output objects</typeparam>
    /// <param name="collectionName">MongoDb Collection Name</param>
    /// <param name="filter">Expression Filter to match documents</param>
    /// <param name="document">Latest version of the document</param>
    /// <returns>Task of type MongoUpdateResult</returns>
    Task<MongoUpdateResult> ReplaceOne<T>(string collectionName, FilterDefinition<T> filter, T document);
}
