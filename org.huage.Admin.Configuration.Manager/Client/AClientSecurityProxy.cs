using Npoi.Mapper;
using NPOI.SS.Formula.Functions;
using org.huage.Admin.Configuration.Entity.Entity;

namespace org.huage.Admin.Configuration.Manager.Client;

public class AClientSecurityProxy : ClientSecurityProxyBase
{
    public override async Task<ConfigurationBase> GetConfiguration(string clientId)
    {
        return new ConfigurationBase()
        {
            configurations = new Dictionary<string, List<Config>>()
            {
                { "A", new List<Config>() }
            }
        };
    }


    /// <summary>
    /// 导出A配置的数据
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public override async Task ExportConfiguration(List<ConfigurationBase> configuration)
    {
        /*if (!configuration.Any())
            return;*/
        //查询数据库拿到数据导出
        
        var dict = new Dictionary<string, List<Config>>();
        dict.Add("ACompany", new List<Config>()
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
            dynamic mapper = new Mapper();
            var configs = configurationBase.configurations.FirstOrDefault().Value;
            //mapper.Put<Config>(configs,"ACompanyConfig");
            mapper.Save("AConfig.xlsx", configs, "ACompanyConfig", overwrite: true, xlsx: true, leaveOpen: true);
            Console.WriteLine("导入完成");
        }
    }
}