using Api.Base.Controllers;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.IdentityConfigs;
using AuthServer.Application.Features.Menus;

namespace AuthServer.Api.Controllers
{
    /// <summary>
    /// مدیریت منوها
    /// </summary>
    [ApiExplorerSettings(GroupName = "مدیریت منوها")]
    public class MenusController : BaseApiController
    {
        #region لیست منوها بصورت درختی
        [HttpGet]
        [Authorize(IdentityScopes.AuthService.Menus.Full)]
        [ProducesResponseType(typeof(BaseResult<ItemListDTO<IdentityClaimDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ItemListDTO<IdentityClaimDTO>>> GetList()
        {
            var res = await Mediator.Send(new MenuGetListQuery());
            return Ok(res);
        }
        #endregion


        #region لیست منوها بصورت فلت
        [HttpGet("[action]")]
        [Authorize(IdentityScopes.AuthService.Menus.Full)]
        [ProducesResponseType(typeof(BaseResult<ItemListDTO<IdentityClaimDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ItemListDTO<IdentityClaimDTO>>> GetFlatList()
        {
            var res = await Mediator.Send(new MenuGetFlatListQuery());
            return Ok(res);
        }
        #endregion
    }
}
