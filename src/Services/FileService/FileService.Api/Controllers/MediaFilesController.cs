using Api.Base.Controllers;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.IdentityConfigs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FileService.Application.Features.MediaFiles;
using Domain.Enums;
using FileService.Infrastructure.Services.Files;
using FileService.Application.Contracts;

namespace FileService.Api.Controllers
{
    /// <summary>
    /// مدیریت فایل ها
    /// </summary>
    [ApiExplorerSettings(GroupName = "مدیریت فایل ها")]
    public class MediaFilesController : BaseApiController
    {
        #region لیست فایل ها
        //[HttpGet]
        //[Authorize(IdentityScopes.AuthService.Roles.Read)]
        //[ProducesResponseType(typeof(BaseResult<ItemListDTO<RoleListItemDTO>>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<ItemListDTO<RoleListItemDTO>>> GetList([FromQuery] RoleGetListQuery model)
        //{
        //    var res = await Mediator.Send(model);
        //    return Ok(res);
        //}
        #endregion



        #region گرفتن اطلاعات با آیدی
        //[HttpGet]
        //[Route("{id:long}")]
        //[Authorize(Policy = IdentityScopes.AuthService.Roles.Read)]
        //[ProducesResponseType(typeof(BaseResult<RoleItemDTO>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<RoleItemDTO>> GetById([FromRoute] long id)
        //{
        //    var res = await Mediator.Send(new RoleGetByIdQuery(id));
        //    return Ok(res);
        //}
        #endregion



        #region ایجاد
        //[HttpPost]
        //[Authorize(IdentityScopes.AuthService.Roles.Create)]
        //[ProducesResponseType(typeof(BaseResult<long>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<long>> Create([FromBody] RoleCreateCommand model)
        //{
        //    var res = await Mediator.Send(model);
        //    return Ok(res);
        //}
        #endregion




        #region ویرایش
        //[HttpPut]
        //[Authorize(IdentityScopes.AuthService.Roles.Update)]
        //[ProducesResponseType(typeof(BaseResult), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult> Update([FromBody] RoleUpdateCommand model)
        //{
        //    var res = await Mediator.Send(model);
        //    return Ok(res);
        //}
        #endregion




        #region حذف
        //[HttpDelete]
        //[Route("{id:long}")]
        //[Authorize(IdentityScopes.AuthService.Roles.Delete)]
        //[ProducesResponseType(typeof(BaseResult), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult> Delete([FromRoute] long id)
        //{
        //    var res = await Mediator.Send(new RoleDeleteCommand(id));
        //    return Ok(res);
        //}
        #endregion



        #region دانلود فایل و عکس
        #region دانلود فایل با آیدی
        [HttpGet]
        [Route("[action]/{id:long}")]
        public async Task<IActionResult> Download([FromRoute] long id, [FromQuery] bool isThumb = false)
        {
            var res = await Mediator.Send(new DownloadByIdQuery(id, isThumb));
            return File(res.Value.Stream, res.Value.MimeType);
        }
        #endregion

        #region دانلود فایل با نام
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Download([FromBody, FromForm, FromQuery] DownloadByNameQuery model)
        {
            var res = await Mediator.Send(model);
            return File(res.Value.Stream, res.Value.MimeType);
        }
        #endregion
        #endregion




        #region استریم فایل و عکس
        #region استریم فایل با آیدی
        [HttpGet]
        [Route("[action]/{id:long}")]
        public async Task<IActionResult> GetStream([FromRoute] long id, [FromQuery] bool isThumb = false)
        {
            var res = await Mediator.Send(new DownloadByIdQuery(id, isThumb));
            return File(res.Value.Stream, "application/octet-stream");
        }
        #endregion

        #region استریم فایل با نام
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetStream([FromBody, FromForm, FromQuery] DownloadByNameQuery model)
        {
            var res = await Mediator.Send(model);
            return File(res.Value.Stream, "application/octet-stream");
        }
        #endregion
        #endregion




        #region کار با فایل
        #region آپلود فایل فیزیکی
        [HttpPost("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(BaseResult<List<SelectListDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseResult>> UploadFile([FromForm, FromBody] FileUploadCommand model)
        {
            var res = await Mediator.Send(model);
            return Ok(res);
        }
        #endregion


        #region حذف فایل فیزیکی
        [HttpDelete("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(BaseResult<List<SelectListDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseResult>> DeleteFile([FromQuery] FileDeleteCommand model)
        {
            var res = await Mediator.Send(model);
            return Ok(res);
        }
        #endregion
        #endregion



        #region کار با تصویر
        #region آپلود تصویر فیزیکی
        [HttpPost("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(BaseResult<List<SelectListDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseResult>> UploadPicture([FromForm, FromBody] PictureUploadCommand model)
        {
            var res = await Mediator.Send(model);
            return Ok(res);
        }
        #endregion




        #region حذف تصویر فیزیکی
        [HttpDelete("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(BaseResult<List<SelectListDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseResult>> DeletePicture([FromQuery] PictureDeleteCommand model)
        {
            var res = await Mediator.Send(model);
            return Ok(res);
        }
        #endregion 
        #endregion


    }
}
