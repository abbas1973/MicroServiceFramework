using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using Application.Behaviors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text.Json;
using Application.DTOs;
using Microsoft.AspNetCore.Http;
using Application.IdentityConfigs;
using Microsoft.AspNetCore.Authorization;
using Utilities;
using Base.Application.Common.IdentityConfigs.Handlers;
using Application.Exceptions;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Base.Application
{
    public static class CommonApplicationRegistration
    {
        /// <summary>
        /// کانفیگ های پایه و عمومی که در همه پروژه ها لازم است
        /// </summary>
        public static void RegisterCommonApplication(
            this WebApplicationBuilder builder,
            Assembly assembly, Type type,
            IConfiguration configuration,
            List<string> scopes)
        {
            var services = builder.Services;

            //راه اندازی سریلاگ
            builder.ConfigSerilog();

            // کانفیگ احراز هویت و دسترسی های توابع با آیدنتیتی سرور
            services.AddIdentityServerConfig(configuration, scopes);

            services.AddFluentValidation(type);
            services.AddMediatR(assembly);
        }


        #region افزودن mediateR به سرویس
        /// <summary>
        /// افزودن mediateR به سرویس
        /// </summary>
        /// <param name="services"></param>
        public static void AddMediatR(this IServiceCollection services, Assembly  assembly)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            
            //پایپ لاین های mediateR
            services.ConfigurePipelines();
        }



        #region پایپ لاین ها - behaviors
        public static void ConfigurePipelines(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
        #endregion
        #endregion



        #region افزودن FluentValidation به سرویس
        /// <summary>
        /// افزودن FluentValidation به سرویس
        /// <para>
        /// services.AddFluentValidation(typeof(ApplicationRegistration))
        /// </para>
        /// </summary>
        /// <param name="type">نوع یک کلاس از لایه اپلیکیشن پروزه ی فراخوانی کننده</param>
        public static void AddFluentValidation(this IServiceCollection services, Type type)
        {
            services.AddValidatorsFromAssemblyContaining(type);
            FluentValidationDisplayNameResolver();
        }


        #region نمایش اتریبیوت DisplayName بجای نام پروپرتی در فروئنت ولیدیشن
        public static void FluentValidationDisplayNameResolver()
        {
            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                if (memberInfo != null)
                {
                    var attr = memberInfo.GetCustomAttribute(typeof(DisplayAttribute), false) as DisplayAttribute;
                    if (attr != null)
                        return attr.Name;
                }
                return memberInfo?.Name;
            };
        }
        #endregion
        #endregion



        #region کانفیگ های آیدنتیتی سرور برای سرویس های زیر مجموعه
        /// <summary>
        /// کانفیگ های آیدنتیتی سرور برای سرویس های زیر مجموعه
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="Scopes">همه اسکوپ های دسترسی جزء درون سرویس فعلی</param>
        public static void AddIdentityServerConfig(
            this IServiceCollection services,
            IConfiguration configuration,
            List<string> Scopes)
        {
            var identityServerUrl = configuration.GetValue<string>("IdentityServerConfig:Url");

            #region کانفیگ کردن آیدنتیتی سرور برای چک کردن اعتبار توکن
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.IncludeErrorDetails = true;
                options.Authority = identityServerUrl;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new RsaSecurityKey(new RSACryptoServiceProvider(2048).ExportParameters(true)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidTypes = new[] { "at+jwt" },

                    ValidateIssuer = true, // چک میکند که توکن توسط همین آیدنتیتی سرور تولید شده باشد.
                    ValidateAudience = false,
                    //ValidAudiences = new List<string>() { ApiResourceName } // چک میکند که توکن برای همین سرویس تولید شده باشد
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                        throw new UnAuthorizedException("احراز هویت شما با خطا همراه بوده است!"),
                    
                    OnChallenge = context => 
                        throw new UnAuthorizedException("احراز هویت شما با خطا همراه بوده است!"),
                };
            });
            //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => Configuration.Bind("CookieSettings", options));
            #endregion

            #region تعریف پالیسی ها برای دسترسی
            services.AddAuthorization(options =>
            {
                foreach (var scope in Scopes)
                {
                    #region بدست آوردن دسترسی full برای هر سر دسته
                    var sections = scope.Split('.');
                    var AllowedScopes = new List<string> { IdentityScopes.Full, scope };
                    for (int i = 0; i < sections.Length - 1; i++)
                    {
                        var tmp = sections.Take(i + 1);
                        var prefix = string.Join('.', tmp);
                        AllowedScopes.Add($"{prefix}.Full");
                    }
                    #endregion

                    #region مدیریت پالیسی با روش مستقیم
                    //options.AddPolicy(scope, policy =>
                    //{
                    //    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    //    policy.RequireAuthenticatedUser();
                    //    policy.RequireClaim(nameof(UserTokenDTO.Mic).ToCamelCase(), Boolean.TrueString);
                    //    policy.RequireClaim("access", AllowedScopes);
                    //});
                    #endregion

                    #region مدیریت پالیسی با کلاس هندلر بیرونی
                    options.AddPolicy(scope, policy =>
                    {
                        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        policy.RequireAuthenticatedUser();
                        policy.Requirements.Add(new ClientClaimRequirement(scope, AllowedScopes));
                    });
                    #endregion
                }
            })
            .AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationResultTransformer>();
            services.AddSingleton<IAuthorizationHandler, ClientClaimRequirementHandler>();
            #endregion
        }
        #endregion




        #region کانفیگ سریلاگ
        public static void ConfigSerilog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(new JsonFormatter(),
                    "logs/important-logs-.json",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Warning)

                .WriteTo.File("logs/daily-log-.logs",
                    rollingInterval: RollingInterval.Day)

                .MinimumLevel.Debug()

                .CreateLogger();

            builder.Host.UseSerilog();
        }
        #endregion

    }
}
