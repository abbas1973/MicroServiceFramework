using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System.Reflection;

namespace Base.Api.Registration
{
    public static class BaseServiceRegistration
    {
        #region کانفیگ های پایه مشترک بین سرویس ها
        public static void RegisterBaseServices(
            this WebApplicationBuilder builder,
            IConfiguration configuration,
            string swaggerTitle = null,
            string swaggerDescription = null)
        {
            var services = builder.Services;

            services.AddControllers();

            //گرفتن httpcontext در کلاس لایبرری ها
            services.AddHttpContextAccessor();


            // کانفیگ swagger
            services.AddSwagger(swaggerTitle, swaggerDescription);


            // Cors
            services.AddCors(configuration);

            // کانفیگ هدر فرواردینگ برای ریدایرکت ها
            services.ConfigureForwardedHeader();

            services.AddEndpointsApiExplorer();

            // ورژن Api
            services.AddApiVersionExplorer();

        }
        #endregion


        #region کانفیگ هدر فروارد برای ریدایرکت شدن ها
        public static void ConfigureForwardedHeader(this IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }
        #endregion



        #region API Versioning
        /// <summary>
        /// کانفیگ کردن ورژن Api ها
        /// </summary>
        /// <param name="services"></param>
        public static void AddApiVersionExplorer(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });
        }
        #endregion




        #region Cors
        public static void AddCors(this IServiceCollection services, IConfiguration configuration)
        {
            var corsAllowed = configuration.GetSection("CorsAllowed").Get<string[]>().ToList();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                  .SetIsOriginAllowed(origin =>
                  {
                      if (string.IsNullOrWhiteSpace(origin)) return false;

                      if (corsAllowed == null || !corsAllowed.Any())
                          return true;

                      return corsAllowed.Any(url => origin.ToLower() == url || origin.ToLower().StartsWith(url + "/"));
                  });
            }));
        }
        #endregion




        #region Swagger
        public static void AddSwagger(this IServiceCollection services, string title, string description)
        {
            var openApiInfo = new OpenApiInfo
            {
                Version = "v1",
                Title = title,
                Description = description
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", openApiInfo);


                #region احراز هویت با توکن درون هدر
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please enter into field the word 'Bearer JWT_TOKEN'",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
                #endregion


                #region نمایش نام گروه ها(کنترلرها) در سوگر
                //c.EnableAnnotations();
                c.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }

                    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    if (controllerActionDescriptor != null)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });
                c.DocInclusionPredicate((name, api) => true);
                #endregion


                #region خواندن کامنت ها برای توضیحات swagger
                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                .Union(new AssemblyName[] { currentAssembly.GetName() })
                .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                .Where(f => File.Exists(f)).ToArray();

                Array.ForEach(xmlDocs, (d) =>
                {
                    c.IncludeXmlComments(d);
                });
                #endregion
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }
        #endregion


    }
}
