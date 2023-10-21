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
    public class FileDeleteCommand
    : IRequest<BaseResult>
    {
        #region Constructors
        public FileDeleteCommand()
        {
        }
        #endregion


        #region Properties
        [Display(Name = "نام فایل")]
        public string FileName { get; set; }

        [Display(Name = "دسته بندی فایل")]
        public MediaFileGroup? Group { get; set; }
        #endregion

    }
    #endregion



    #region Handler
    public class FileDeleteCommandHandler : IRequestHandler<FileDeleteCommand, BaseResult>
    {
        private readonly IFileHelper _fileHelper;
        public FileDeleteCommandHandler(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }


        public async Task<BaseResult> Handle(FileDeleteCommand request, CancellationToken cancellationToken)
        {
            await _fileHelper.DeleteFile(request.FileName, request.Group.Value);
            return new BaseResult(true);
        }
    }

    #endregion
}
