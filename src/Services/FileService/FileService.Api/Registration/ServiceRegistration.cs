using Base.Api.Registration;
using FileService.Application;
using FileService.Infrastructure;

namespace FileService.Registration
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(
            this WebApplicationBuilder builder,
            IConfiguration configuration,
            string serviceNameFa)
        {
            var services = builder.Services;


            // رجیستر کردن سرویس های پایه
            builder.RegisterBaseServices(configuration, serviceNameFa);


            // context و uow
            services.RegisterInfrastructure(configuration);


            // کانفیگ های لایه اپلیکیشن
            builder.RegisterApplication(configuration);           
        }


    }
}
