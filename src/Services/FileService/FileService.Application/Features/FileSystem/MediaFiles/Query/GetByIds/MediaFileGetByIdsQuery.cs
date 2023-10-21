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
    public class MediaFileGetByIdsQuery
    : IRequest<BaseResult<List<MediaFile>>>
    {
        #region Constructors
        public MediaFileGetByIdsQuery(List<long> ids)
        {
            Ids = ids;
        }
        #endregion


        #region Properties
        [Display(Name = "شناسه های فایل ها")]
        public List<long> Ids { get; set; }
        #endregion

    }
    #endregion



    #region Handler
    public class MediaMediaFileGetByIdsQueryHandler : IRequestHandler<MediaFileGetByIdsQuery, BaseResult<List<MediaFile>>>
    {
        private readonly IUnitOfWork _uow;
        public MediaMediaFileGetByIdsQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<BaseResult<List<MediaFile>>> Handle(MediaFileGetByIdsQuery request, CancellationToken cancellationToken)
        {
            var mediaFile = await _uow.MediaFiles.GetAsync(x => request.Ids.Contains(x.Id));
            if (mediaFile == null || !mediaFile.Any())
                throw new NotFoundException($"فایل های درخواستی با شناسه های {string.Join(',', request.Ids)} یافت نشدند!");

            return new BaseResult<List<MediaFile>>(mediaFile);
        }
    }
    #endregion
}
