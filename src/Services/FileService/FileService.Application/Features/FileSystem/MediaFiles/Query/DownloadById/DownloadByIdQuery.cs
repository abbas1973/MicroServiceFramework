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
    public class DownloadByIdQuery
    : IRequest<BaseResult<DownloadDTO>> , IBaseEntityDTO
    {
        #region Constructors
        public DownloadByIdQuery(long id, bool isThumb)
        {
            Id = id;
            IsThumb = isThumb;
        }
        #endregion


        #region Properties
        [Display(Name = "شناسه")]
        public long Id { get; set; }

        [Display(Name = "تصویر کوچک")]
        public bool IsThumb { get; set; }
        #endregion

    }
    #endregion



    #region Handler
    public class MediaDownloadByIdQueryHandler : IRequestHandler<DownloadByIdQuery, BaseResult<DownloadDTO>>
    {
        private readonly IFileHelper _fileHelper;
        private readonly IUnitOfWork _uow;
        public MediaDownloadByIdQueryHandler(IFileHelper fileHelper, IUnitOfWork uow)
        {
            _fileHelper = fileHelper;
            _uow = uow;
        }


        public async Task<BaseResult<DownloadDTO>> Handle(DownloadByIdQuery request, CancellationToken cancellationToken)
        {
            var mediaFile = await _uow.MediaFiles.GetByIdAsync(request.Id);
            if (mediaFile == null)
                throw new NotFoundException($"فایل درخواستی با شناسه {request.Id} یافت نشد!");

            return await Task.Run(() =>
            {
                var fullPath = "";
                if (mediaFile.IsPic)
                    if (request.IsThumb)
                        fullPath = _fileHelper.GetPicThumbPath(mediaFile.Group, mediaFile.FileName);
                    else
                        fullPath = _fileHelper.GetPicLargePath(mediaFile.Group, mediaFile.FileName);
                else
                    fullPath = _fileHelper.GetFilePath(mediaFile.Group, mediaFile.FileName);

                if(string.IsNullOrEmpty(fullPath) || !File.Exists(fullPath))
                    throw new FileException($"فایل با آیدی {mediaFile.Id} و نام {mediaFile.FileName} در دسته بندی {mediaFile.Group.ToString()} یافت نشد!");

                var stream = File.OpenRead(fullPath);
                var mimeType = fullPath.GetMimeType();
                return new BaseResult<DownloadDTO>(new DownloadDTO(stream, mimeType));
            });
        }
    }

    #endregion
}
