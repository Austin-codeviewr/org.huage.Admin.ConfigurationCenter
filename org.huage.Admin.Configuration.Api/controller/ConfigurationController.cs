using System.Threading.Channels;
using Microsoft.AspNetCore.Mvc;

namespace org.huage.Admin.Configuration.Api.controller;

/**
 * 希望可以根据动态路由携带的org_id去查询不同application的配置值。
 */

[ApiController]
[Route("api/[controller]/[action]")]
public class ConfigurationController : ControllerBase
{
    public void TestChannel()
    {
        
        var channel = Channel.CreateBounded<int>(new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.Wait,
            /*否是单一的消费者或者生产者
            SingleReader = true,
            SingleWriter = true*/
        });

        //channel.Writer.TryWrite("test");

    }
    
}