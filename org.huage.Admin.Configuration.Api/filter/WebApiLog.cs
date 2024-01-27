using System.Text;
using org.huage.Admin.Configuration.Entity.Entity;
using org.huage.Admin.Configuration.Entity.Service;

namespace org.huage.Admin.Configuration.Api.filter;

public class WebApiLog
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly List<string> _ignoreActions = new List<string> { "Index1", "Default/Index2" };

    public WebApiLog(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!_ignoreActions.Exists(s => context.Request.Path.ToString().Contains(s)))
        {
            //首先记录一些基本的参数，IP,Action,Time等
            TApilog apilog = new TApilog();
            apilog.Ip = Convert.ToString(context.Connection.RemoteIpAddress);
            apilog.Action = context.Request.Path;
            apilog.Intime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            
            //这里保存userToken
            using var scope = _serviceScopeFactory.CreateScope();
            /*string token = context.Request.Headers["token"];
            if (!string.IsNullOrEmpty(token))
            {
                var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
                Apilog.Useraccount = tokenService.ParseToken(context)?.UserAccount;
            }*/


            //传入参数解析
            StringBuilder inarg = new StringBuilder();
            if (context.Request.HasFormContentType)
            {
                foreach (var item in context.Request.Form)
                {
                    inarg.AppendLine(item.Key + ":" + item.Value);
                }
            }
            else if (context.Request.Query.Count > 0)
            {
                foreach (var item in context.Request.Query)
                {
                    inarg.AppendLine(item.Key + ":" + item.Value);
                }
            }
            else
            {
                context.Request.EnableBuffering();
                StreamReader streamReader = new StreamReader(context.Request.Body);
                inarg.AppendLine(await streamReader.ReadToEndAsync());
                context.Request.Body.Seek(0, SeekOrigin.Begin);
            }

            apilog.Input = inarg.ToString();

            
            //传入参数解析
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _next(context);
                apilog.Output = await GetResponse(context.Response);
                await responseBody.CopyToAsync(originalBodyStream);
            } 
            
            apilog.Outtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var _tApilogServices = scope.ServiceProvider.GetRequiredService<ITApilogServices>();
            try
            {
                //这里记录执行流程
                await _tApilogServices.InsertAsync(apilog);
            }
            catch
            {
                // ignored
            }
        }
        else
        {
            await _next(context);
        }
    }

    private async Task<string> GetResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return text;
    }
}



