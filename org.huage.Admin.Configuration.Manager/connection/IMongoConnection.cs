using MongoDB.Driver;

namespace org.huage.Admin.Configuration.Manager.connection;

public class IMongoConnection
{
    public MongoClient MongoDBClient { get; set; }
    public IMongoDatabase DatabaseName { get; set; }
}