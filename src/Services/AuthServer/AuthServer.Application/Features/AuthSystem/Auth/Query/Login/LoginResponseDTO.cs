namespace AuthServer.Application.Features.Auth
{
    public class LoginResponseDTO
    {
        #region Constructors
        public LoginResponseDTO() { }

        public LoginResponseDTO(string accessToken, string refreshToken, bool mobileIsConfirmed, string accessTokenExpireDate)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            MobileIsConfirmed = mobileIsConfirmed;
            AccessTokenExpireDate = accessTokenExpireDate;
        }
        #endregion


        /// <summary>
        /// توکن دسترسی
        /// </summary>
        public string AccessToken { get; set; }


        /// <summary>
        /// رفرش توکن
        /// </summary>
        public string RefreshToken { get; set; }


        /// <summary>
        /// تلفن همراه کاربر تایید شده است؟
        /// </summary>
        public bool MobileIsConfirmed { get; set; }


        /// <summary>
        /// تاریخ منقضی شدن اکسس توکن
        /// </summary>
        public string AccessTokenExpireDate { get; set; }
    }
}
