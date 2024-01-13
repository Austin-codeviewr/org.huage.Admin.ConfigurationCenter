using org.huage.Admin.Configuration.Entity.Entity;
using org.huage.Admin.Configuration.Entity.Service;

namespace org.huage.Admin.Configuration.Manager.Client;

public class ClientSecurityProxyBase : IClientSecurityProxy
{
    public virtual Task<ConfigurationBase> GetConfiguration(string clientId)
    {
        throw new NotImplementedException();
    }
}