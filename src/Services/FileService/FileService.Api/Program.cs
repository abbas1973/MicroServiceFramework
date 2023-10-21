using Base.Api.Registration;
using FileService.Infrastructure.Persistence;
using FileService.Registration;

string ServiceNameFa = "سرویس مدیریت فایل ها";

var builder = WebApplication.CreateBuilder(args);

// کانفیگ همه نیازمندی ها در سرویس
builder.RegisterServices(builder.Configuration, ServiceNameFa);

var app = builder.Build();

// مایگریت کردن و مقدار دهی اولیه به دیتابیس ها
app.SeedDatabases();

// میدلورهای کاستوم نوشته شده
app.UseCustomMiddlewares(ServiceNameFa);

app.Run();
