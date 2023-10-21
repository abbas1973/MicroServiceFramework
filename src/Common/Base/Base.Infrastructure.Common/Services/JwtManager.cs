using Application.DTOs;
using Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.ComponentModel;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities;

namespace Infrastructure.Services
{
    public class JwtManager : IJwtManager
    {
        private IHttpContextAccessor _accessor;
        public JwtManager(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }


        #region گرفتن توکن از هدر
        /// <summary>
        /// گرفتن توکن از هدر
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            var token = _accessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString();
            if (string.IsNullOrEmpty(token))
                return null;

            if (token.IndexOf(" ") > 0)
            {
                var arr = token?.Split(' ');
                if (arr[0].ToLower() == "bearer")
                    return arr[1];
            }
            return null;
        }
        #endregion


        #region گرفتن آیدی کلاینت درخواست دهنده از توکن
        /// <summary>
        /// گرفتن آیدی کلاینت درخواست دهنده از توکن
        /// </summary>
        /// <returns></returns>
        public string GetClientId(string token = null)
        {
            return GetClaim(token ?? GetToken(), "client_id");
        }
        #endregion



        #region گرفتن اطلاعات کاربر از درون توکن
        /// <summary>
        /// گرفتن اطلاعاتی از یوزر که درون توکن است
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public UserTokenDTO GetUser(string token = null)
        {
            if (string.IsNullOrEmpty(token))
                token = GetToken();
            if (string.IsNullOrEmpty(token))
                return null;

            var user = new UserTokenDTO();
            foreach (var prop in typeof(UserTokenDTO).GetProperties())
            {
                var propType = prop.PropertyType;
                var claimValue = GetClaim(token, prop.Name.ToCamelCase());
                var converter = TypeDescriptor.GetConverter(propType);
                var convertedValue = converter.ConvertFromString(claimValue);
                prop.SetValue(user, convertedValue);
            }

            return user;
        }
        #endregion



        #region گرفتن آیدی کاربر از توکن
        /// <summary>
        /// گرفتن آیدی کاربر از توکن
        /// </summary>
        /// <returns></returns>
        public long? GetUserId(string token = null)
        {
            try
            {
                var id = GetClaim(token ?? GetToken(), nameof(UserTokenDTO.Id).ToCamelCase());
                return long.Parse(id);
            }
            catch
            {
                return null;
            }
        }
        #endregion



        #region بررسی تایید موبایل کاربر
        /// <summary>
        /// بررسی تایید موبایل کاربر
        /// </summary>
        public bool MobileIsConfimed(string token = null)
        {
            try
            {
                token = token ?? GetToken();
                if (token == null)
                    return true;
                    
                var user = GetUser(token);
                return user.Mic;
            }
            catch
            {
                return false;
            }
        } 
        #endregion



        #region گرفتن کلایم از توکن
        /// <summary>
        ///  گرفتن کلایم درون توکن
        /// </summary>
        /// <param name="claimType">پارامتر مورد نیاز</param>
        /// <param name="token">توکن</param>
        /// <returns></returns>
        public string GetClaim(string token, string claimType)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }
        #endregion



        #region گرفتن زمان اکسپایر شدن توکن
        /// <summary>
        /// گرفتن زمان اکسپایر شدن توکن
        /// </summary>
        /// <param name="token">توکن</param>
        /// <returns></returns>
        public DateTime? GetExpireDate(string token)
        {
            try
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
                return jwtSecurityToken.ValidTo.ToLocalTime();
            }
            catch (Exception)
            {
                return null;
            }
        } 
        #endregion



    }
}
