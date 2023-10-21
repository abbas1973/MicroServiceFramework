using FileService.Grpc.Registration;
using FileService.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// کانفیگ همه نیازمندی ها در سرویس
builder.RegisterServices(builder.Configuration);

var app = builder.Build();

app.MapGrpcService<MediaFileService>();
app.MapGet("/", () => "this is auth server grpc");

app.Run();
