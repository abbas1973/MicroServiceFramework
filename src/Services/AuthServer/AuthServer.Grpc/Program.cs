using AuthServer.Grpc.Services;
using AuthServer.Grpc.Registration;

var builder = WebApplication.CreateBuilder(args);

// کانفیگ همه نیازمندی ها در سرویس
builder.RegisterServices(builder.Configuration);

var app = builder.Build();

app.MapGrpcService<UsersProtoService>();
app.MapGet("/", () => "this is auth server grpc");

app.Run();
