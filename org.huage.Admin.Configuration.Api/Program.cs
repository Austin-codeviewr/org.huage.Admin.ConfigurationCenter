using org.huage.Admin.Configuration.Entity.Service;
using org.huage.Admin.Configuration.Manager.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
//注入各个子系统的具体代理实现
builder.Services.AddSingleton<AClientSecurityProxy>();
builder.Services.AddSingleton<BClientSecurityProxy>();


Func<string, bool> isA = clientId => clientId.EndsWith(".A", StringComparison.OrdinalIgnoreCase)
                                         || clientId.EndsWith(".axxx", StringComparison.OrdinalIgnoreCase)
                                         || clientId.Equals("A", StringComparison.OrdinalIgnoreCase)
                                         || clientId.Equals("a", StringComparison.OrdinalIgnoreCase);


Func<string, bool> isB = clientId => clientId.EndsWith(".b", StringComparison.OrdinalIgnoreCase)
                                       || clientId.EndsWith(".bxx", StringComparison.OrdinalIgnoreCase)
                                       || clientId.Equals("B", StringComparison.OrdinalIgnoreCase)
                                       || clientId.Equals("b", StringComparison.OrdinalIgnoreCase);


//注入一个Fun,来判断属于那个系统
builder.Services.AddSingleton(sp =>new Func<string, IClientSecurityProxy>(c =>
{
    if (isA(c))
        return sp.GetRequiredService<AClientSecurityProxy>();
    if (isB(c))
        return sp.GetRequiredService<BClientSecurityProxy>();
    throw new NotSupportedException($"Unsupported clientId:{c}.");
}));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
