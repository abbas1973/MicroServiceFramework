using Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using FileService.Application.Interface;
using AutoMapper;
using FileService.Domain.Entities;
using Base.Application.Mapping;
using FileService.Application.Contracts;
using Application.Exceptions;

namespace FileService.Application.Features.MediaFiles
{
    #region Request
    public class MediaFileUpdateCommand
    : IRequest<BaseResult<long>>, IBaseEntityDTO, IMapFrom<MediaFile>
    {

        #region Properties
        [Display(Name = "شناسه")]
        public long Id { get; set; }

        [Display(Name = "عنوان فارسی")]
        public string TitleFa { get; set; }

        [Display(Name = "عنوان لاتین")]
        public string TitleEn { get; set; }
        #endregion




        #region Mapping
        public void Mapping(Profile profile) =>
            profile.CreateMap<MediaFile, MediaFileUpdateCommand>().ReverseMap();
        #endregion

    }
    #endregion



    #region Handler
    public class MediaFileUpdateCommandHandler : IRequestHandler<MediaFileUpdateCommand, BaseResult<long>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IFileHelper _fileHelper;
        public MediaFileUpdateCommandHandler(IUnitOfWork uow, IMapper mapper, IFileHelper fileHelper)
        {
            _uow = uow;
            _mapper = mapper;
            _fileHelper = fileHelper;
        }


        public async Task<BaseResult<long>> Handle(MediaFileUpdateCommand request, CancellationToken cancellationToken)
        {
            var mediaFile = await _uow.MediaFiles.GetByIdAsync(request.Id);
            if (mediaFile == null)
                throw new NotFoundException($"مدیافایل مورد نظر با شناسه {request.Id} یافت نشد!");

            mediaFile = _mapper.Map(request, mediaFile);
            _uow.MediaFiles.Update(mediaFile);
            await _uow.CommitAsync();
            return new BaseResult<long>(mediaFile.Id);
        }
    }

    #endregion
}
