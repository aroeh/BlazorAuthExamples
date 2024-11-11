namespace ProtectedWebApi.Models;

public struct MongoUpdateResult
{
    /// <summary>
    /// Represents if the update operation was acknowledged by the MongoDB Database
    /// </summary>
    public bool IsAcknowledged { get; set; }

    /// <summary>
    /// The number of records that were modified in the update operation
    /// </summary>
    public long ModifiedCount { get; set; }
}
