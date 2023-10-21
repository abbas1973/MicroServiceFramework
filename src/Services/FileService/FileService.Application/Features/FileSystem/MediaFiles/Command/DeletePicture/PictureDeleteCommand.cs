using Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using FileService.Application.Contracts;

namespace FileService.Application.Features.MediaFiles
{
    #region Request
    public class PictureDeleteCommand
    : IRequest<BaseResult>
    {
        #region Constructors
        public PictureDeleteCommand()
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
    public class PictureDeleteCommandHandler : IRequestHandler<PictureDeleteCommand, BaseResult>
    {
        private readonly IFileHelper _fileHelper;
        public PictureDeleteCommandHandler(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }


        public async Task<BaseResult> Handle(PictureDeleteCommand request, CancellationToken cancellationToken)
        {
            await _fileHelper.DeletePic(request.FileName, request.Group.Value);
            return new BaseResult(true);
        }
    }

    #endregion
}
