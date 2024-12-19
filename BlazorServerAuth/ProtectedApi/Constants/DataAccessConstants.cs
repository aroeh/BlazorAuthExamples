namespace ProtectedApi.Constants;

public static class DataAccessConstants
{
    // key name to point to connection string secret
    public const string MongoConn = "MONGODB_CONN";

    // database name to connect to
    public const string MongoDatabase = "samples";

    // name of the collection containing records
    public const string MongoCollection = "restuarants";
}
