using org.huage.Admin.Configuration.Entity.Entity;

namespace org.huage.Admin.Configuration.Entity.Service;

public interface IClientSecurityProxy
{
    Task<ConfigurationBase> GetConfiguration(string clientId);
}