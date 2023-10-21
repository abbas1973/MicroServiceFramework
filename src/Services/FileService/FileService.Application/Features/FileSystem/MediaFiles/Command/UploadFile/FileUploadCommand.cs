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
    public class FileUploadCommand
    : IRequest<BaseResult<List<MediaFileUploadResultDTO>>>
    {
        #region Constructors
        public FileUploadCommand()
        {
        }
        #endregion


        #region Properties
        [Display(Name = "فایل")]
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();

        [Display(Name = "دسته بندی فایل")]
        public MediaFileGroup? Group { get; set; }
        #endregion

    }
    #endregion



    #region Handler
    public class MediaFileUploadCommandHandler : IRequestHandler<FileUploadCommand, BaseResult<List<MediaFileUploadResultDTO>>>
    {
        private readonly IFileHelper _fileHelper;
        public MediaFileUploadCommandHandler(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }


        public async Task<BaseResult<List<MediaFileUploadResultDTO>>> Handle(FileUploadCommand request, CancellationToken cancellationToken)
        {
            var result = new List<MediaFileUploadResultDTO>();
            foreach (var file in request.Files)
            {
                #region آیا فایل مجاز است
                var fileFormat = file.FileName.GetFormat();
                if (!file.IsValidFile(fileFormat))
                    throw new NotSupportedException("فایل بارگذاری شده غیر مجاز می باشد.");
                #endregion

                #region ذخیره فایل
                if (file.Length > 0L)
                {
                    var res = await _fileHelper.SaveFile(file, request.Group.Value);
                    result.Add(res);
                }
                #endregion
            }

            return new BaseResult<List<MediaFileUploadResultDTO>>(result);
        }
    }

    #endregion
}
