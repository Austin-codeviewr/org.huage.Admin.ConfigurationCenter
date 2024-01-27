using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace org.huage.Admin.Configuration.Api.filter;

public class ExceptionFilter : IAsyncExceptionFilter
{
    Action<string, string, Exception>? _action;
    private readonly IWebHostEnvironment _environment;
    
    public ExceptionFilter(Action<string, string, Exception> action, IWebHostEnvironment environment)
    {
        _action = action;
        _environment = environment;
    }

    public ExceptionFilter(IWebHostEnvironment environment)
    {
        
    }

    public Task OnExceptionAsync(ExceptionContext context)
    {
        // 如果异常没有被处理则进行处理
        if (context.ExceptionHandled == false)
        {
            if (_action != null)
            {
                _action(((ControllerActionDescriptor)context.ActionDescriptor).ControllerName,
                    ((ControllerActionDescriptor)context.ActionDescriptor).ActionName, context.Exception);
            }

            if (_environment.IsDevelopment())
            {
                context.Result = new JsonResult("系统异常:" + context.Exception.Message);
            }
            else
            {
                context.Result = new JsonResult("系统异常:请联系管理员或稍后再试");
            }
        } // 设置为true，表示异常已经被处理了

        context.ExceptionHandled = true;
        return Task.CompletedTask;
    }
}