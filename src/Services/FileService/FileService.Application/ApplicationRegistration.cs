using System.Reflection;
using Base.Application;
using Microsoft.Extensions.Configuration;
using Application.IdentityConfigs;
using Microsoft.AspNetCore.Builder;

namespace FileService.Application
{
    public static class ApplicationRegistration
    {
        public static void RegisterApplication(
            this WebApplicationBuilder builder,
            IConfiguration configuration,
            Assembly presentationLayerAssembly = null)
        {
            var services = builder.Services;

            var scopes = IdentityScopes.AuthService.GetClaims().ToList();
            builder.RegisterBaseApplication(
                Assembly.GetExecutingAssembly(),
                typeof(ApplicationRegistration),
                configuration,
                scopes, 
                presentationLayerAssembly
                );
        }

    }
}
