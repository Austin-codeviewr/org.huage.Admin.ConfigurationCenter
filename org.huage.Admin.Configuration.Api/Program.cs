using System.IO.Compression;
using System.Text.Json;
using Microsoft.AspNetCore.ResponseCompression;
using org.huage.Admin.Configuration.Api.extension;
using org.huage.Admin.Configuration.Api.filter;
using org.huage.Admin.Configuration.Entity.Entity;
using org.huage.Admin.Configuration.Entity.Service;
using org.huage.Admin.Configuration.Manager.Client;
using org.huage.Admin.Configuration.Manager.connection;
using org.huage.Admin.Configuration.Manager.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//响应压缩
builder.Services.AddResponseCompression(options =>
{
    //可以添加多种压缩类型，程序会根据级别自动获取最优方式
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    //添加自定义压缩策略
    options.Providers.Add<MyCompressionProvider>();
    //针对指定的MimeType来使用压缩策略
    options.MimeTypes =
        ResponseCompressionDefaults.MimeTypes.Concat(
            new[] { "application/json" });
});
//针对不同的压缩类型，设置对应的压缩级别
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    //使用最快的方式进行压缩，单不一定是压缩效果最好的方式
    options.Level = CompressionLevel.Fastest;

    //不进行压缩操作
    //options.Level = CompressionLevel.NoCompression;

    //即使需要耗费很长的时间，也要使用压缩效果最好的方式
    //options.Level = CompressionLevel.Optimal;
});

//注入各个子系统的具体代理实现
builder.Services.AddSingleton<AClientSecurityProxy>();
builder.Services.AddSingleton<BClientSecurityProxy>();
builder.Services.AddSingleton<IMongoConnection, MongoConnection>();
builder.Services.AddSingleton<ITApilogServices, ApilogServices>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ExceptionFilter((controller, action, exception) =>
    {
        //异常日志记录
        Console.WriteLine("Something  wrong");
    },builder.Environment));
});


Func<string, bool> isA = clientId => clientId.EndsWith(".A", StringComparison.OrdinalIgnoreCase)
                                     || clientId.EndsWith(".axxx", StringComparison.OrdinalIgnoreCase)
                                     || clientId.Equals("A", StringComparison.OrdinalIgnoreCase)
                                     || clientId.Equals("a", StringComparison.OrdinalIgnoreCase);

Func<string, bool> isB = clientId => clientId.EndsWith(".b", StringComparison.OrdinalIgnoreCase)
                                     || clientId.EndsWith(".bxx", StringComparison.OrdinalIgnoreCase)
                                     || clientId.Equals("B", StringComparison.OrdinalIgnoreCase)
                                     || clientId.Equals("b", StringComparison.OrdinalIgnoreCase);


//注入一个Fun,来判断属于那个系统
builder.Services.AddSingleton(sp => new Func<string, IClientSecurityProxy>(c =>
{
    if (isA(c))
        return sp.GetRequiredService<AClientSecurityProxy>();
    if (isB(c))
        return sp.GetRequiredService<BClientSecurityProxy>();
    throw new NotSupportedException($"Unsupported clientId:{c}.");
}));


builder.Services.RegisterAllServices();
var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<WebApiLog>();

app.MapGet("/test", ( IServiceProvider serviceProvider) =>
{
    var service = serviceProvider.GetRequiredService<ITestServices>();
    Console.WriteLine(service.GetValue());
});
app.Run();