using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using Application.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using System.Security.AccessControl;
using Azure;

namespace Base.Application
{
    public static class BaseApplicationRegistration
    {
        /// <summary>
        /// کانفیگ های پایه و عمومی که در همه پروژه ها لازم است
        /// </summary>
        public static void RegisterBaseApplication(
            this WebApplicationBuilder builder,
            Assembly assembly,
            Type type,
            IConfiguration configuration,
            List<string> scopes,
            Assembly presentationLayerAssembly = null)
        {
            var services = builder.Services;

            services.AddCustomAutoMapper(assembly, presentationLayerAssembly);
            builder.RegisterCommonApplication(assembly, type, configuration, scopes);
        }



        #region افزودن AutoMapper به سرویس ها
        public static void AddCustomAutoMapper(this IServiceCollection services, Assembly assembly, Assembly presentationLayerAssembly = null)
        {
            if(presentationLayerAssembly == null)
                services.AddAutoMapper(assembly);
            else
                services.AddAutoMapper(assembly, presentationLayerAssembly);

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                #region مپینگ لایه اپلیکیشن
                cfg.ShouldMapMethod = x => false;
                cfg.AddProfile(new MappingProfile(assembly));
                #endregion

                #region مپینگ های لایه grpc و api
                if (presentationLayerAssembly != null)
                {
                    var types = presentationLayerAssembly.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t));

                    foreach (var type in types)
                    {
                        var instance = Activator.CreateInstance(type);
                        cfg.AddProfile((Profile)instance);
                    }
                } 
                #endregion

            }).CreateMapper());
        }
        #endregion



    }
}
