using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Interface;
using Infrastructure.Services;
using FileService.Infrastructure.Persistence;
using FileService.Application.Interface;
using FileService.Infrastructure.Repositories;
using FileService.Application.Contracts;
using FileService.Infrastructure.Services.Files;

namespace FileService.Infrastructure
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
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString,
                                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddScoped<DbContext, ApplicationContext>();
            #endregion


            #region DI
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtManager, JwtManager>();
            services.AddTransient<IFileHelper, FileHelper>();
            #endregion

        }


    }
}
