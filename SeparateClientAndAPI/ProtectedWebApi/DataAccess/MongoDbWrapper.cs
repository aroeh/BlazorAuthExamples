using MongoDB.Driver;
using ProtectedWebApi.Models;

namespace ProtectedWebApi.DataAccess;

public class MongoDbWrapper(ILoggerFactory logFactory, IConfiguration config) : IMongoDbWrapper
{
    private readonly MongoDbService mongoService = new(logFactory, config);


    /// <summary>
    /// Finds items in the specified collection using a filter definition
    /// </summary>
    /// <typeparam name="T">Generic Type for the input and output objects</typeparam>
    /// <param name="collectionName">MongoDb Collection Name</param>
    /// <param name="filter">Expression Filter to match documents</param>
    /// <returns>Task List of Generic Type T</returns>
    public Task<List<T>> FindMany<T>(string collectionName, FilterDefinition<T> filter) => mongoService.FindMany<T>(collectionName, filter);

    /// <summary>
    /// Finds one item in the specified collection using a filter definition
    /// </summary>
    /// <typeparam name="T">Generic Type for the input and output objects</typeparam>
    /// <param name="collectionName">MongoDb Collection Name</param>
    /// <param name="filter">Expression Filter to match documents</param>
    /// <returns>Task of Generic Type T</returns>
    public Task<T> FindOne<T>(string collectionName, FilterDefinition<T> filter) => mongoService.FindOne<T>(collectionName, filter);

    /// <summary>
    /// Insert a new document into the specified collection
    /// </summary>
    /// <typeparam name="T">Generic Type for the input and output objects</typeparam>
    /// <param name="collectionName"></param>
    /// <param name="document"></param>
    /// <returns>Task of Generic Type T</returns>
    public Task<T> InsertOne<T>(string collectionName, T document) => mongoService.InsertOne<T>(collectionName, document);

    /// <summary>
    /// Insert multiple new documents into the specified collection
    /// </summary>
    /// <typeparam name="T">Generic Type for the input and output objects</typeparam>
    /// <param name="collectionName">MongoDb Collection Name</param>
    /// <param name="documents">New Document to be inserted</param>
    /// <returns>Task Enumerable of T with updated ids from the insert operation</returns>
    public Task<IEnumerable<T>> InsertMany<T>(string collectionName, IEnumerable<T> documents) => mongoService.InsertMany<T>(collectionName, documents);

    /// <summary>
    /// Replaces a document with a later version using a filter to match the record
    /// </summary>
    /// <typeparam name="T">Generic Type for the input and output objects</typeparam>
    /// <param name="collectionName">MongoDb Collection Name</param>
    /// <param name="filter">Expression Filter to match documents</param>
    /// <param name="document">Latest version of the document</param>
    /// <returns>MongoUpdateResult</returns>
    public Task<MongoUpdateResult> ReplaceOne<T>(string collectionName, FilterDefinition<T> filter, T document) => mongoService.ReplaceOne<T>(collectionName, filter, document);
}
