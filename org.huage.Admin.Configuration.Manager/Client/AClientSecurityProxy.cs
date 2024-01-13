using org.huage.Admin.Configuration.Entity.Entity;
using org.huage.Admin.Configuration.Entity.Service;

namespace org.huage.Admin.Configuration.Manager.Client;

public class AClientSecurityProxy : ClientSecurityProxyBase
{
    public override async Task<ConfigurationBase> GetConfiguration(string clientId)
    {
        return new ConfigurationBase()
        {
            configurations =new Dictionary<string, List<Config>>()
            {
                {"A",new List<Config>()}
            }
        };
    }
}