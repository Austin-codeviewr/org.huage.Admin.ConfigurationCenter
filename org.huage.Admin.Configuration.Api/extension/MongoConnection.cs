using MongoDB.Driver;
using org.huage.Admin.Configuration.Manager.connection;

namespace org.huage.Admin.Configuration.Api.extension;

public class MongoConnection : IMongoConnection
{
    public MongoClient MongoDBClient { get; set; }

    public IMongoDatabase DatabaseName { get; set; }
    
    
    public MongoConnection(IConfiguration configuration)
    {
        MongoDBClient = new MongoClient(configuration["MongoSettings:Connection"]);
        DatabaseName = MongoDBClient.GetDatabase(configuration["MongoSettings:DatabaseName"]);
    }

}