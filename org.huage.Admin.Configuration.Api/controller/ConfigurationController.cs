using Microsoft.AspNetCore.Mvc;
using org.huage.Admin.Configuration.Entity.Service;

namespace org.huage.Admin.Configuration.Api.controller;

/**
 * 希望可以根据动态路由携带的org_id去查询不同application的配置值。
 */
[ApiController]
[Route("api/[controller]/[action]")]
public class ConfigurationController : ControllerBase
{
    private readonly Func<string, IClientSecurityProxy> _clientProxyFactory;

    public ConfigurationController(Func<string, IClientSecurityProxy> clientProxyFactory)
    {
        _clientProxyFactory = clientProxyFactory;
    }
    
    [HttpGet("{clientId}/getSetting")]
    public async Task<IActionResult> TestConfigurationAsync([FromRoute] string clientId)
    {
        var resp = await _clientProxyFactory(clientId).GetConfiguration(clientId);
        return Ok(resp);
    }
}