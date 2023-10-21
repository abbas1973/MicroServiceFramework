using Application.DTOs;
using AuthServer.Application.Interface;
using AuthServer.Domain.Entities;
using IdentityModel;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Security.Claims;

namespace AuthServer.Infrastructure.Persistence.Identity.Configs
{
    /// <summary>
    /// تغییر در کلایم های توکن ایجاد شده و افزودن توکن های کاربر
    /// </summary>
    public class CustomClaimsService : DefaultClaimsService
    {
        protected IUnitOfWork _uow { get; }
        public CustomClaimsService(IProfileService profile, ILogger<DefaultClaimsService> logger, IUnitOfWork uow) : base(profile, logger)
        {
            _uow = uow;
        }

        public override async Task<IEnumerable<Claim>> GetAccessTokenClaimsAsync(ClaimsPrincipal subject, ResourceValidationResult resourceResult, ValidatedRequest request)
        {
            var baseResult = await base.GetAccessTokenClaimsAsync(subject, resourceResult, request);
            var claims = baseResult.ToList();

            var username = claims.First(x => x.Type == JwtClaimTypes.Subject)?.Value;
            var userClaims = await GetUserClaims(username);

            if (userClaims.Any())
            {
                claims = claims.Where(x => x.Type != JwtClaimTypes.Scope).ToList();
                //claims = claims.Where(x => !userClaims.Any(z => z.Value == x.Type)).ToList();
                claims.AddRange(userClaims);
            }

            return claims;
        }




        #region استخراج کلایم های کاربر با استفاده از یوزرنیم
        public async Task<List<Claim>> GetUserClaims(string username)
        {
            var claims = new List<Claim>();

            #region پیدا کردن کاربر
            if (string.IsNullOrEmpty(username))
                return claims;
            var user = await _uow.Users.GetByUsername(username);
            if (user == null)
                return claims;
            #endregion

            #region کلایم های پایه
            var tokenDTO = new UserTokenDTO(user.Id, user.UserName, user.Name, user.PhoneNumberConfirmed);
            claims = tokenDTO.GetClaims();
            #endregion

            #region کلایم های دسترسی کاربر
            var roleIds = await _uow.UserRoles.GetUserRoleIds(user.Id);
            var userClaims = await _uow.RoleClaims.GetRolesClaims(roleIds);
            foreach (var claim in userClaims)
                claims.Add(new Claim("access", claim)); 
            #endregion

            return claims;
        } 
        #endregion
    }
}
