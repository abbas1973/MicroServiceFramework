using Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;
using FileService.Application.Contracts;
using FileService.Application.Interface;
using Application.Exceptions;
using FileService.Domain.Entities;

namespace FileService.Application.Features.MediaFiles
{
    #region Request
    public class MediaFileGetByIdQuery
    : IRequest<BaseResult<MediaFile>> , IBaseEntityDTO
    {
        #region Constructors
        public MediaFileGetByIdQuery(long id)
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
    public class MediaMediaFileGetByIdQueryHandler : IRequestHandler<MediaFileGetByIdQuery, BaseResult<MediaFile>>
    {
        private readonly IFileHelper _fileHelper;
        private readonly IUnitOfWork _uow;
        public MediaMediaFileGetByIdQueryHandler(IFileHelper fileHelper, IUnitOfWork uow)
        {
            _fileHelper = fileHelper;
            _uow = uow;
        }


        public async Task<BaseResult<MediaFile>> Handle(MediaFileGetByIdQuery request, CancellationToken cancellationToken)
        {
            var mediaFile = await _uow.MediaFiles.GetByIdAsync(request.Id);
            if (mediaFile == null)
                throw new NotFoundException($"فایل درخواستی با شناسه {request.Id} یافت نشد!");

            return new BaseResult<MediaFile>(mediaFile);
        }
    }
    #endregion
}
