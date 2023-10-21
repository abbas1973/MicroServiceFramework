using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Utilities.Files;
using Domain.Enums;
using FileService.Application.Contracts;
using FileService.Application.DTOs.MediaFiles;

namespace FileService.Application.Features.MediaFiles
{
    #region Request
    public class PictureUploadCommand
    : IRequest<BaseResult<List<MediaFileUploadResultDTO>>>
    {
        #region Constructors
        public PictureUploadCommand()
        {
            LargeMaxWidth = 900;
            LargeMaxHeight = 600;
            ThumbMaxWidth = 360;
            ThumbMaxHeight = 240;
        }
        #endregion


        #region Properties
        [Display(Name = "فایل")]
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();

        [Display(Name = "دسته بندی فایل")]
        public MediaFileGroup? Group { get; set; }

        public int LargeMaxWidth { get; set; }
        public int LargeMaxHeight { get; set; }
        public int ThumbMaxWidth { get; set; }
        public int ThumbMaxHeight { get; set; }
        #endregion

    }
    #endregion



    #region Handler
    public class PictureUploadCommandHandler : IRequestHandler<PictureUploadCommand, BaseResult<List<MediaFileUploadResultDTO>>>
    {
        private readonly IFileHelper _fileHelper;
        public PictureUploadCommandHandler(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }


        public async Task<BaseResult<List<MediaFileUploadResultDTO>>> Handle(PictureUploadCommand request, CancellationToken cancellationToken)
        {
            var result = new List<MediaFileUploadResultDTO>();
            foreach (var file in request.Files)
            {
                #region آیا فایل مجاز است
                var fileFormat = file.FileName.GetFormat();
                if (!file.IsValidFile(fileFormat))
                    throw new NotSupportedException("فایل بارگذاری شده غیر مجاز می باشد.");
                #endregion

                #region ذخیره تصویر
                if (file.Length > 0L)
                {
                    var res = await _fileHelper.SavePic(
                        file, request.Group.Value,
                        request.LargeMaxWidth, request.LargeMaxHeight,
                        request.ThumbMaxWidth, request.ThumbMaxHeight);
                    result.Add(res);
                }
                #endregion
            }

            return new BaseResult<List<MediaFileUploadResultDTO>>(result);
        }
    }

    #endregion
}
