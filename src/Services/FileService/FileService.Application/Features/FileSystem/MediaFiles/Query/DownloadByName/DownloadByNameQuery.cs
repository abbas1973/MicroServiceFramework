using Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using FileService.Application.Contracts;
using Microsoft.AspNetCore.Http;
using FileService.Application.Interface;
using Utilities.Files;
using Application.Exceptions;
using FileService.Application.DTOs.MediaFiles;

namespace FileService.Application.Features.MediaFiles
{
    #region Request
    public class DownloadByNameQuery
    : IRequest<BaseResult<DownloadDTO>>
    {
        #region Constructors
        public DownloadByNameQuery()
        {
        }
        #endregion


        #region Properties
        [Display(Name = "نام فایل")]
        public string FileName { get; set; }

        [Display(Name = "دسته بندی فایل")]
        public MediaFileGroup? Group { get; set; }

        [Display(Name = "تصویر/فایل")]
        public bool? IsPic { get; set; }

        [Display(Name = "تصویر کوچک")]
        public bool IsThumb { get; set; }
        #endregion

    }
    #endregion



    #region Handler
    public class MediaDownloadByNameQueryHandler : IRequestHandler<DownloadByNameQuery, BaseResult<DownloadDTO>>
    {
        private readonly IFileHelper _fileHelper;
        private readonly IUnitOfWork _uow;
        public MediaDownloadByNameQueryHandler(IFileHelper fileHelper, IUnitOfWork uow)
        {
            _fileHelper = fileHelper;
            _uow = uow;
        }


        public async Task<BaseResult<DownloadDTO>> Handle(DownloadByNameQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var fullPath = "";
                if (request.IsPic == true)
                    if(request.IsThumb)
                        fullPath = _fileHelper.GetPicThumbPath(request.Group.Value, request.FileName);
                    else
                        fullPath = _fileHelper.GetPicLargePath(request.Group.Value, request.FileName);
                else
                    fullPath = _fileHelper.GetFilePath(request.Group.Value, request.FileName);

                if(string.IsNullOrEmpty(fullPath) || !File.Exists(fullPath))
                    throw new FileException($"فایل {request.FileName} در دسته بندی {request.Group.ToString()} یافت نشد!");

                var stream = File.OpenRead(fullPath);
                var mimeType = fullPath.GetMimeType();
                return new BaseResult<DownloadDTO>(new DownloadDTO(stream, mimeType));
            });
        }
    }

    #endregion
}
