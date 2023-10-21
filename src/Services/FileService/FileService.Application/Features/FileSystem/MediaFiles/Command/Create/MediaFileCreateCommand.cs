using Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using FileService.Application.Interface;
using AutoMapper;
using FileService.Domain.Entities;
using Base.Application.Mapping;
using FileService.Application.Contracts;

namespace FileService.Application.Features.MediaFiles
{
    #region Request
    public class MediaFileCreateCommand
    : IRequest<BaseResult<long>>, IMapFrom<MediaFile>
    {

        #region Properties
        [Display(Name = "عنوان فارسی")]
        public string TitleFa { get; set; }

        [Display(Name = "عنوان لاتین")]
        public string TitleEn { get; set; }

        [Display(Name = "نام فایل")]
        public string FileName { get; set; }

        [Display(Name = "حجم فایل")]
        public long? Size { get; set; }

        [Display(Name = "تصویر است یا فایل؟")]
        public bool IsPic { get; set; }

        [Display(Name = "نوع فایل")]
        public FileFormat? Format { get; set; }

        [Display(Name = "دسته بندی فایل")]
        public MediaFileGroup? Group { get; set; }
        #endregion



        #region Mapping
        public void Mapping(Profile profile) => 
            profile.CreateMap<MediaFile, MediaFileCreateCommand>().ReverseMap();
        #endregion

    }
    #endregion



    #region Handler
    public class MediaFileCreateCommandHandler : IRequestHandler<MediaFileCreateCommand, BaseResult<long>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IFileHelper _fileHelper;
        public MediaFileCreateCommandHandler(IUnitOfWork uow, IMapper mapper, IFileHelper fileHelper)
        {
            _uow = uow;
            _mapper = mapper;
            _fileHelper = fileHelper;
        }


        public async Task<BaseResult<long>> Handle(MediaFileCreateCommand request, CancellationToken cancellationToken)
        {
            var fileIsExist = await _fileHelper.IsExist(request.FileName, request.Group.Value, request.IsPic);
            if (!fileIsExist)
                throw new FileNotFoundException($"{(request.IsPic ? "فایل" : "تصویر")} {request.FileName} در گروه {request.Group.Value} یافت نشد!");
            
            var mediaFile = _mapper.Map<MediaFile>(request);
            await _uow.MediaFiles.AddAsync(mediaFile);
            await _uow.CommitAsync();
            return new BaseResult<long>(mediaFile.Id);
        }
    }

    #endregion
}
