using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Application.Exceptions;

namespace Base.Application.Common.IdentityConfigs.Handlers
{
    /// <summary>
    /// تولید خطای استاندارد در صورتی که پولیسی خطا بدهد
    /// و کاربر به بخش مورد نظر دسترسی نداشته باشد
    /// </summary>
    public class AuthorizationResultTransformer : IAuthorizationMiddlewareResultHandler
    {
        private readonly IAuthorizationMiddlewareResultHandler _handler;

        public AuthorizationResultTransformer()
        {
            _handler = new AuthorizationMiddlewareResultHandler();
        }

        public async Task HandleAsync(
            RequestDelegate requestDelegate,
            HttpContext httpContext,
            AuthorizationPolicy authorizationPolicy,
            PolicyAuthorizationResult policyAuthorizationResult)
        {
            if (policyAuthorizationResult.Forbidden && policyAuthorizationResult.AuthorizationFailure != null)
                throw new ForbiddenException("شما به بخش مورد نظر دسترسی ندارید!");

            await _handler.HandleAsync(requestDelegate, httpContext, authorizationPolicy, policyAuthorizationResult);
        }
    }
}
