using Microsoft.AspNetCore.Mvc;

namespace org.huage.Admin.Configuration.Api.controller;

/**
 * 希望可以根据动态路由携带的org_id去查询不同application的配置值。
 */

[ApiController]
[Route("api/[controller]/[action]")]
public class ConfigurationController : ControllerBase
{
    
}