using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using AuthServer.Infrastructure.Persistence;
using AuthServer.Domain.Entities;
using AuthServer.Application.Interface;
using AuthServer.Infrastructure.Repositories;
using AuthServer.Infrastructure.Persistence.Identity.Configs;
using IdentityServer4.Services;
using Application.Interface;
using Infrastructure.Services;

namespace AuthServer.Infrastructure
{
    public static class InfrastructureRegistration
    {
        /// <summary>
        /// کانفیگ دیتابیس به uow در سرویس
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region DB Context
            var connectionString = configuration.GetConnectionString("ApplicationContext");
            services.AddDbContext<ApplicationContext>(
                options => options.UseSqlServer(connectionString,
                                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            #region کانفیگ آیدنتیتی
            services.AddIdentity<User, Role>(options =>
                {
                    // user options
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
                    options.User.RequireUniqueEmail = true;

                    // sign in options
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = true;

                    // password options
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 3;

                    // lockout
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                })
                    .AddEntityFrameworkStores<ApplicationContext>()
                    .AddDefaultTokenProviders()
                    .AddErrorDescriber<PersianIdentityErrors>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
            }); 
            #endregion

            services.AddScoped<DbContext, ApplicationContext>();
            #endregion


            #region UOW
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtManager, JwtManager>();
            #endregion


            #region آیدنتیتی سرور
            // address : /.well-known/openid-configuration
            var migrationAssembly = typeof(ApplicationContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
                        optionBuilders => optionBuilders.MigrationsAssembly(migrationAssembly)
                                                        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                    options.DefaultSchema = "IS4";
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
                        optionBuilders => optionBuilders.MigrationsAssembly(migrationAssembly)
                                                        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                    options.DefaultSchema = "IS4";
                })
                .AddResourceOwnerValidator<UserValidator>();
            services.AddTransient<IClaimsService, CustomClaimsService>();
            #endregion
        }


    }
}
