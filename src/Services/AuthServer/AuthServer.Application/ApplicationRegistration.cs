using System.Reflection;
using Base.Application;
using Microsoft.Extensions.Configuration;
using Application.IdentityConfigs;
using Microsoft.AspNetCore.Builder;

namespace AuthServer.Application
{
    public static class ApplicationRegistration
    {
        public static void RegisterApplication(
            this WebApplicationBuilder builder,
            IConfiguration configuration)
        {
            var services = builder.Services;

            var scopes = IdentityScopes.AuthService.GetClaims().ToList();
            builder.RegisterCommonApplication(
                Assembly.GetExecutingAssembly(),
                typeof(ApplicationRegistration),
                configuration,
                scopes
                );
        }

    }
}
