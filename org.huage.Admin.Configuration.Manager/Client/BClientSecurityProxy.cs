using Npoi.Mapper;
using org.huage.Admin.Configuration.Entity.Entity;
using org.huage.Admin.Configuration.Entity.Service;

namespace org.huage.Admin.Configuration.Manager.Client;

public class BClientSecurityProxy: ClientSecurityProxyBase
{
    public override async Task<ConfigurationBase> GetConfiguration(string clientId)
    {
        return new ConfigurationBase()
        {
            configurations =new Dictionary<string, List<Config>>
            {
                {"B",new List<Config>()}
            }
        };
    }
    
    public override async Task ExportConfiguration(List<ConfigurationBase> configuration)
    {
        /*if (!configuration.Any())
            return;*/
        //查询数据库拿到数据导出
        
        var dict = new Dictionary<string, List<Config>>();
        dict.Add("BCompany", new List<Config>()
        {
            new()
            {
                Key = "first",
                Value = "FV"
            },
            new()
            {
                Key = "second",
                Value = "SV"
            },
            new()
            {
                Key = "third",
                Value = "TV"
            },
            new()
            {
                Key = "fourth",
                Value = "FOV"
            }
        });

        configuration.Add(new ConfigurationBase
        {
            configurations = dict
        });

        foreach (var configurationBase in configuration)
        {
            var mapper = new Mapper();
            var configs = configurationBase.configurations.FirstOrDefault().Value;
            //mapper.Put<Config>(configs,"ACompanyConfig");
            mapper.Save("BConfig.xlsx", configs, "BCompanyConfig", overwrite: true, xlsx: true, leaveOpen: true);
            Console.WriteLine("BConfig导入完成");
        }
    }
}