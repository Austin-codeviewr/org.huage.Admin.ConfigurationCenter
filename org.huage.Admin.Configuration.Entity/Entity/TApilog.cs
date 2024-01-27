using Microsoft.AspNetCore.Http;

namespace org.huage.Admin.Configuration.Entity.Entity;

//日志记录接口
public class TApilog
{
    public string? Ip { get; set; }
    public string Action { get; set; }
    public string Intime { get; set; }
    public string Input { get; set; }
    public string Output { get; set; }
    public string Outtime { get; set; }
}