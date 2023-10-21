using Application.DTOs;
using Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Utilities;

namespace Base.Application.Common.IdentityConfigs.Handlers
{
    public class ClientClaimRequirementHandler : AuthorizationHandler<ClientClaimRequirement>
    {
        public ClientClaimRequirementHandler()
        {
        }


        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, ClientClaimRequirement requirement)
        {
            #region بررسی تایید موبایل
            var mobileIsConfirmed = context.User.Claims.Where(x => x.Type == nameof(UserTokenDTO.Mic).ToCamelCase());
            if(mobileIsConfirmed == null || !mobileIsConfirmed.Any() || mobileIsConfirmed.First().Value != Boolean.TrueString)
                throw new ForbiddenException("شما به این بخش دسترسی ندارید!");
            #endregion

            #region دسترسی به کلایم های مورد نظر
            var accessList = context.User.Claims.Where(x => x.Type == "access");
            var existedAccess = accessList.Where(x => requirement.AllowedClaims.Any(z => z == x.Value));
            if (existedAccess.Any())
                context.Succeed(requirement);
            else
                throw new ForbiddenException("شما به این بخش دسترسی ندارید!"); 
            #endregion
            return Task.CompletedTask;
        }
    }
}
