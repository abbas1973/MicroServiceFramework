using Api.Base.Controllers;
using Application.DTOs;
using Application.IdentityConfigs;
using AuthServer.Application.Features.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AuthServer.Api.Controllers
{
    /// <summary>
    /// احراز هویت
    /// </summary>
    [ApiExplorerSettings(GroupName = "احراز هویت")]
    public class AuthController : BaseApiController
    {
        #region رجیستر
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Register([FromBody] RegisterCommand model)
        {
            var res = await Mediator.Send(model);
            return Ok(res);
        }
        #endregion



        #region لاگین
        /// <summary>
        /// لاگین
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResult<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Login([FromBody] LoginQuery model)
        {
            var token = await Mediator.Send(model);
            return Ok(token);
        }
        #endregion




        #region گرفتن اکسس توکن با رفرش توکن
        /// <summary>
        /// لاگین
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResult<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> RefreshToken([FromBody] RefreshTokenQuery model)
        {
            var token = await Mediator.Send(model);
            return Ok(token);
        }
        #endregion
    }
}
