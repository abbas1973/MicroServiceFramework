using Base.Grpc.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace Base.Grpc.Registration
{
    public static class BaseGrpcRegistration
    {
        #region کانفیگ های پایه مشترک بین سرویس ها
        public static void RegisterBaseGrpc(
            this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            //گرفتن httpcontext در کلاس لایبرری ها
            services.AddHttpContextAccessor();

            // راه اندازی grpc با اکسپشن هندلر
            builder.Services.AddGrpc(c => c.Interceptors.Add<GrpcGlobalExceptionHandlerInterceptor>());

        }
        #endregion

    }
}
