using FileService.Application;
using FileService.Infrastructure;
using System.Reflection;
using Base.Grpc.Registration;

namespace FileService.Grpc.Registration
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(
            this WebApplicationBuilder builder,
            IConfiguration configuration)
        {
            var services = builder.Services;

            // مپینگ
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // کانفیگ پایه grpc
            builder.RegisterBaseGrpc();

            // کانفیگ لایه اینفراستراکچر
            services.RegisterInfrastructure(configuration);

            // کانفیگ های لایه اپلیکیشن
            builder.RegisterApplication(configuration, Assembly.GetExecutingAssembly());
        }



    }
}
