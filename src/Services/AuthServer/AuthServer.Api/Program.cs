using Base.Api.Registration;
using AuthServer.Registration;
using AuthServer.Infrastructure.Persistence.Identity.Configs;

string ServiceNameFa = "احراز هویت سرویس های پردکو";

var builder = WebApplication.CreateBuilder(args);

// کانفیگ همه نیازمندی ها در سرویس
builder.RegisterServices(builder.Configuration, ServiceNameFa);

var app = builder.Build();

// مایگریت کردن و مقدار دهی اولیه به دیتابیس ها
app.SeedDatabases();

// میدلورهای کاستوم نوشته شده
app.UseCustomMiddlewares(ServiceNameFa);

app.UseIdentityServer();
app.Run();
