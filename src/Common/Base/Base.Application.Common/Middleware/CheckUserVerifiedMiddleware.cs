using Application.Exceptions;
using Application.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Middleware
{
    #region میدلور برای بررسی تایید موبایل توسط کاربر
    /// <summary>
    /// میدلور برای بررسی تایید موبایل توسط کاربر
    /// </summary>
    public class CheckUserVerifiedMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public CheckUserVerifiedMiddleware(
            RequestDelegate next,
            IServiceProvider serviceProvider,
            ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _jwtManager = scope.ServiceProvider.GetRequiredService<IJwtManager>();
                var mobileIsConfirmed = _jwtManager.MobileIsConfimed();
                if (!mobileIsConfirmed)
                    throw new UserNotVerifiedException();
            }
            await _next(context);
        }
    }
    #endregion



    #region middleware use extension
    public static class CheckUserVerifiedMiddlewareExtension
    {
        public static IApplicationBuilder UseCheckUserVerifiedMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckUserVerifiedMiddleware>();
        }
    }
    #endregion
}
