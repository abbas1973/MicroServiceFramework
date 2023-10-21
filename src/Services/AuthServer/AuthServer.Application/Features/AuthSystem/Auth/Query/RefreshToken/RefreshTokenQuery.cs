using MediatR;
using System.ComponentModel.DataAnnotations;
using Application.Exceptions;
using Microsoft.Extensions.Configuration;
using IdentityModel.Client;
using AuthServer.Application.Interface;
using Application.DTOs;
using Application.Interface;

namespace AuthServer.Application.Features.Auth
{
    #region Request
    public class RefreshTokenQuery : IRequest<BaseResult<LoginResponseDTO>>
    {
        #region Constructors
        public RefreshTokenQuery() { }
        #endregion



        #region Properties
        [Display(Name = "رفرش توکن")]
        public string RefreshToken { get; set; }

        //[Display(Name = "اکسس توکن")]
        //public string AccessToken { get; set; }
        #endregion
    } 
    #endregion




    #region Handler
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, BaseResult<LoginResponseDTO>>
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtManager _jwtManager;

        public RefreshTokenQueryHandler(IConfiguration configuration, IJwtManager jwtManager)
        {
            _configuration = configuration;
            _jwtManager = jwtManager;
        }


        public async Task<BaseResult<LoginResponseDTO>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            #region دریافت توکن از آیدنتیتی سرور
            using (var httpClient = new HttpClient())
            {
                #region دریافت discovery document
                var identityServerUrl = _configuration["IdentityServerConfig:Url"];
                var clientId = _configuration["IdentityServerConfig:ClientId"];
                var clientPassword = _configuration["IdentityServerConfig:ClientPassword"];
                var discoveryDoc = await httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
                {
                    Address = identityServerUrl,
                    Policy =
                    {
                        RequireHttps = false
                    }
                });
                if (discoveryDoc.IsError)
                    throw new InternalServerException("اتصال به سرور احراز هویت با خطا همراه بوده است!");
                #endregion

                #region دریافت توکن
                using (var client = new HttpClient())
                {
                    var tokenResult = await client.RequestRefreshTokenAsync(new RefreshTokenRequest()
                    {
                        Address = discoveryDoc.TokenEndpoint,
                        ClientId = clientId,
                        ClientSecret = clientPassword,
                        RefreshToken = request.RefreshToken
                    });
                    if (tokenResult.IsError)
                        throw new BadRequestException("رفرش توکن معتبر نمی باشد!");

                    var user = _jwtManager.GetUser(tokenResult.AccessToken);
                    var expireDate = _jwtManager.GetExpireDate(tokenResult.AccessToken);
                    var model = new BaseResult<LoginResponseDTO>(
                        new LoginResponseDTO()
                        {
                            AccessToken = tokenResult.AccessToken,
                            RefreshToken = tokenResult.RefreshToken,
                            MobileIsConfirmed = user.Mic,
                            AccessTokenExpireDate = expireDate?.ToString("yyyy/MM/dd HH:mm")
                        }); 
                    return model;
                }
                #endregion
            }
            #endregion
        }
    }
    #endregion
}
