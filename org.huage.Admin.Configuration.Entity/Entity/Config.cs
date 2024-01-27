using System.ComponentModel.DataAnnotations.Schema;

namespace org.huage.Admin.Configuration.Entity.Entity;

public class Config
{
    public string Key { get; set; }
    
    public string Value { get; set; }
}