using Api.Base.Controllers;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.IdentityConfigs;
using AuthServer.Application.Features.Users;

namespace AuthServer.Api.Controllers
{
    /// <summary>
    /// مدیریت کاربران
    /// </summary>
    [ApiExplorerSettings(GroupName = "مدیریت کاربران")]
    public class UsersController : BaseApiController
    {
        #region لیست کاربران
        [HttpGet]
        [Authorize(IdentityScopes.AuthService.Users.Read)]
        [ProducesResponseType(typeof(BaseResult<ItemListDTO<UserListItemDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ItemListDTO<UserListItemDTO>>> GetList([FromQuery] UserGetListQuery model)
        {
            var res = await Mediator.Send(model);
            return Ok(res);
        }
        #endregion



        #region گرفتن اطلاعات با آیدی
        [HttpGet]
        [Route("{id:long}")]
        [Authorize(Policy = IdentityScopes.AuthService.Users.Read)]
        [ProducesResponseType(typeof(BaseResult<UserItemDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserItemDTO>> GetById([FromRoute] long id)
        {
            var res = await Mediator.Send(new UserGetByIdQuery(id));
            return Ok(res);
        }
        #endregion



        #region ایجاد
        [HttpPost]
        [Authorize(IdentityScopes.AuthService.Users.Create)]
        [ProducesResponseType(typeof(BaseResult<long>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<long>> Create([FromBody] UserCreateCommand model)
        {
            var res = await Mediator.Send(model);
            return Ok(res);
        }
        #endregion




        #region ویرایش
        [HttpPut]
        [Authorize(IdentityScopes.AuthService.Users.Update)]
        [ProducesResponseType(typeof(BaseResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Update([FromBody] UserUpdateCommand model)
        {
            var res = await Mediator.Send(model);
            return Ok(res);
        }
        #endregion
        



        #region حذف
        [HttpDelete]
        [Route("{id:long}")]
        [Authorize(IdentityScopes.AuthService.Users.Delete)]
        [ProducesResponseType(typeof(BaseResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            var res = await Mediator.Send(new UserDeleteCommand(id));
            return Ok(res);
        }
        #endregion
    }
}
