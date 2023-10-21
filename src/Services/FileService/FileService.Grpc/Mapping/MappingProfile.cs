using Application.DTOs;
using AutoMapper;
using Domain.Enums;
using FileService.Application.Features.MediaFiles;
using FileService.Domain.Entities;
using FileService.Grpc.Protos;
using System.Net;
using Utilities.Files;
using FileFormat = Domain.Enums.FileFormat;

namespace FileService.Grpc.Mapping
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// تبدیل DTO به مدل GRPC
        /// </summary>
        public MappingProfile()
        {

            #region Create
            CreateMap<MediaFileProtoCreateRequest, MediaFileCreateCommand>()
                    .ForMember(dest => dest.Format, opt => opt.MapFrom(src => (FileFormat)src.Format))
                    .ForMember(dest => dest.Group, opt => opt.MapFrom(src => (MediaFileGroup)src.Group))
                    .ReverseMap()
                    .ForMember(dest => dest.Format, opt => opt.MapFrom(src => (int)src.Format.Value))
                    .ForMember(dest => dest.Group, opt => opt.MapFrom(src => (int)src.Group.Value));

            // مپپینگ برای خروجی Create
            CreateMap(typeof(BaseResult<long>), typeof(MediaFileProtoCreateResponse))
                .ConvertUsing(typeof(BaseResultToMediaFileProtoCreateResponse));
            #endregion



            #region Update
            CreateMap<MediaFileProtoUpdateRequest, MediaFileUpdateCommand>()
                .ReverseMap();
            #endregion



            #region BaseResponse
            CreateMap(typeof(BaseResult), typeof(MediaFileProtoBaseResponse))
                .ConvertUsing(typeof(BaseResultToMediaFileProtoBaseResponse));
            #endregion




            #region GetById
            //CreateMap<MediaFile, MediaFileProtoModel>()
            //        .ForMember(dest => dest.Format, opt => opt.MapFrom(src => (int)src.Format))
            //        .ForMember(dest => dest.Group, opt => opt.MapFrom(src => (int)src.Group))
            //        .ForMember(dest => dest.DownloadPath, opt => opt.MapFrom(src => src.Id.GetDownloadUrl(false)))
            //        .ForMember(dest => dest.StreamPath, opt => opt.MapFrom(src => src.Id.GetStreamUrl(false)))
            //        .ForMember(dest => dest.ThumbStreamPath, opt => opt.MapFrom(src => src.IsPic ? src.Id.GetStreamUrl(false) : null));


            CreateMap(typeof(MediaFile), typeof(MediaFileProtoModel))
                .ConvertUsing(typeof(MediaFileToMediaFileProtoModel));

            CreateMap(typeof(BaseResult<MediaFile>), typeof(MediaFileProtoGetByIdResponse))
                .ConvertUsing(typeof(BaseResultToMediaFileProtoGetByIdResponse));
            #endregion





            #region GetByIds
            CreateMap(typeof(BaseResult<List<MediaFile>>), typeof(MediaFileProtoGetByIdsResponse))
                .ConvertUsing(typeof(BaseResultToMediaFileProtoGetByIdsResponse));
            #endregion
        }
    }



}
