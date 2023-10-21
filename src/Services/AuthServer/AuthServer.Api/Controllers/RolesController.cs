using Api.Base.Controllers;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.IdentityConfigs;
using AuthServer.Application.Features.Roles;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AuthServer.Api.Controllers
{
    /// <summary>
    /// مدیریت نقش ها
    /// </summary>
    [ApiExplorerSettings(GroupName = "مدیریت نقش ها")]
    public class RolesController : BaseApiController
    {
        #region لیست نقش ها
        [HttpGet]
        [Authorize(IdentityScopes.AuthService.Roles.Read)]
        [ProducesResponseType(typeof(BaseResult<ItemListDTO<RoleListItemDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ItemListDTO<RoleListItemDTO>>> GetList([FromQuery] RoleGetListQuery model)
        {
            var res = await Mediator.Send(model);
            return Ok(res);
        }
        #endregion



        #region گرفتن اطلاعات با آیدی
        [HttpGet]
        [Route("{id:long}")]
        [Authorize(Policy = IdentityScopes.AuthService.Roles.Read)]
        [ProducesResponseType(typeof(BaseResult<RoleItemDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RoleItemDTO>> GetById([FromRoute] long id)
        {
            var res = await Mediator.Send(new RoleGetByIdQuery(id));
            return Ok(res);
        }
        #endregion



        #region ایجاد
        [HttpPost]
        [Authorize(IdentityScopes.AuthService.Roles.Create)]
        [ProducesResponseType(typeof(BaseResult<long>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<long>> Create([FromBody] RoleCreateCommand model)
        {
            var res = await Mediator.Send(model);
            return Ok(res);
        }
        #endregion




        #region ویرایش
        [HttpPut]
        [Authorize(IdentityScopes.AuthService.Roles.Update)]
        [ProducesResponseType(typeof(BaseResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Update([FromBody] RoleUpdateCommand model)
        {
            var res = await Mediator.Send(model);
            return Ok(res);
        }
        #endregion




        #region حذف
        [HttpDelete]
        [Route("{id:long}")]
        [Authorize(IdentityScopes.AuthService.Roles.Delete)]
        [ProducesResponseType(typeof(BaseResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            var res = await Mediator.Send(new RoleDeleteCommand(id));
            return Ok(res);
        }
        #endregion



        #region گرفتن لیست نقش ها بصورت دراپدون
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(BaseResult<List<SelectListDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<SelectListDTO>>> GetDropdown()
        {
            var res = await Mediator.Send(new RoleGetDropdownQuery());
            return Ok(res);
        }
        #endregion
    }
}
