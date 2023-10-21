using Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;
using FileService.Application.Interface;
using AutoMapper;
using FileService.Application.Contracts;
using Application.Exceptions;

namespace FileService.Application.Features.MediaFiles
{
    #region Request
    public class MediaFileDeleteCommand
    : IRequest<BaseResult>, IBaseEntityDTO
    {
        #region Constructors
        public MediaFileDeleteCommand(long id)
        {
            Id = id;
        }
        #endregion


        #region Properties
        [Display(Name = "شناسه")]
        public long Id { get; set; }
        #endregion

    }
    #endregion



    #region Handler
    public class MediaFileDeleteCommandHandler : IRequestHandler<MediaFileDeleteCommand, BaseResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IFileHelper _fileHelper;
        public MediaFileDeleteCommandHandler(IUnitOfWork uow, IMapper mapper, IFileHelper fileHelper)
        {
            _uow = uow;
            _mapper = mapper;
            _fileHelper = fileHelper;
        }


        public async Task<BaseResult> Handle(MediaFileDeleteCommand request, CancellationToken cancellationToken)
        {
            #region پیدا کردن مدیا فایل
            var mediaFile = await _uow.MediaFiles.GetByIdAsync(request.Id);
            if (mediaFile == null)
                throw new NotFoundException($"مدیافایل مورد نظر با شناسه {request.Id} یافت نشد!");
            #endregion


            #region حذف فیزیکی تصویر
            if (mediaFile.IsPic)
                await _fileHelper.DeletePic(mediaFile.FileName, mediaFile.Group);
            else
                await _fileHelper.DeleteFile(mediaFile.FileName, mediaFile.Group); 
            #endregion

            _uow.MediaFiles.Remove(mediaFile);
            await _uow.CommitAsync();
            return new BaseResult(true, "فایل مورد نظر با موفقیت حذف شد");
        }
    }

    #endregion
}
