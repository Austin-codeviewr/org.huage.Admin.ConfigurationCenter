namespace org.huage.Admin.Configuration.Entity.Entity;

public class ConfigurationBase
{
    public Dictionary<string, List<Config>> configurations { get; set; } = new Dictionary<string, List<Config>>();
}