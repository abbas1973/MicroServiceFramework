using Application.DTOs;

namespace Application.Interface
{
    public interface IJwtManager
    {
        /// <summary>
        /// گرفتن توکن از هدر
        /// </summary>
        /// <returns></returns>
        string GetToken();


        /// <summary>
        /// گرفتن آیدی کلاینت درخواست دهنده از توکن
        /// </summary>
        /// <returns></returns>
        string GetClientId(string token);


        /// <summary>
        /// گرفتن اطلاعات کاربر از توکن
        /// </summary>
        /// <returns></returns>
        UserTokenDTO GetUser(string token = null);


        /// <summary>
        /// گرفتن آیدی کاربر از توکن
        /// </summary>
        /// <returns></returns>
        long? GetUserId(string token = null);


        /// <summary>
        /// بررسی تایید موبایل کاربر
        /// </summary>
        bool MobileIsConfimed(string token = null);


        /// <summary>
        ///  گرفتن کلایم درون توکن
        /// </summary>
        /// <param name="token">توکن</param>
        /// <param name="claimType">پارامتر مورد نیاز</param>
        /// <returns></returns>
        string GetClaim(string token, string claimType);



        /// <summary>
        /// گرفتن زمان اکسپایر شدن توکن
        /// </summary>
        /// <param name="token">توکن</param>
        /// <returns></returns>
        DateTime? GetExpireDate(string token);
    }
}
