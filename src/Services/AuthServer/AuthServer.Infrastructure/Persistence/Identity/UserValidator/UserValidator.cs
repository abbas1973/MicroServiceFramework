using Application.DTOs;
using Application.Exceptions;
using AuthServer.Application.Interface;
using AuthServer.Domain.Entities;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;

namespace AuthServer.Infrastructure.Persistence
{
    /// <summary>
    /// ولیدیت کردن یوزرنیم و پسورد کاربر در آیدنتیتی سرور
    /// </summary>
    public class UserValidator : IResourceOwnerPasswordValidator
    {
        protected UserManager<User> _userManager { get; }
        public UserValidator(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var username = context.UserName;
            var password = context.Password;

            try
            {
                #region یافتن کاربر و بررسی کلمه عبور
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                    throw new Exception("نام کاربری یا کلمه عبور اشتباه است!");

                var passwordIsValid = await _userManager.CheckPasswordAsync(user, password);
                if (!passwordIsValid)
                    throw new Exception("نام کاربری یا کلمه عبور اشتباه است!");
                #endregion


                #region ارسال خروجی
                var claims = new List<Claim>();
                context.Result = new GrantValidationResult(username, AuthenticationMethods.Password, claims);
                await Task.CompletedTask;
                #endregion


            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(
                        error: IdentityServer4.Models.TokenRequestErrors.UnauthorizedClient,
                        errorDescription: ex.Message);
            }
        }

    }
}
