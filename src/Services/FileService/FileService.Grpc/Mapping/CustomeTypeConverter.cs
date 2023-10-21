using Application.DTOs;
using AutoMapper;
using Utilities.Files;
using FileService.Application.Features.MediaFiles;
using FileService.Domain.Entities;
using FileService.Grpc.Protos;

namespace FileService.Grpc.Mapping
{
    #region BaseResult => MediaFileProtoCreateResponse
    public class BaseResultToMediaFileProtoCreateResponse : ITypeConverter<BaseResult<long>, MediaFileProtoCreateResponse>
    {
        public MediaFileProtoCreateResponse Convert(BaseResult<long> source,
                                                MediaFileProtoCreateResponse destination,
                                                ResolutionContext context)
        {
            var model = new MediaFileProtoCreateResponse
            {
                IsSuccess = source.IsSuccess
            };
            if (source.IsSuccess)
                model.Value = source.Value;
            model.Errors.Add(source.Errors);
            return model;
        }
    }
    #endregion



    #region BaseResult => MediaFileProtoBaseResponse
    public class BaseResultToMediaFileProtoBaseResponse : ITypeConverter<BaseResult, MediaFileProtoBaseResponse>
    {
        public MediaFileProtoBaseResponse Convert(BaseResult source,
                                                MediaFileProtoBaseResponse destination,
                                                ResolutionContext context)
        {
            var model = new MediaFileProtoBaseResponse
            {
                IsSuccess = source.IsSuccess
            };
            model.Errors.Add(source.Errors);
            return model;
        }
    }
    #endregion



    #region MediaFile => MediaFileProtoModel
    public class MediaFileToMediaFileProtoModel : ITypeConverter<MediaFile, MediaFileProtoModel>
    {
        public MediaFileProtoModel Convert(MediaFile source,
                                                MediaFileProtoModel destination,
                                                ResolutionContext context)
        {
            var model = new MediaFileProtoModel
            {
                Id = source.Id,
                Format = (int)source.Format,
                Group = (int) source.Group,
                DownloadPath = source.Id.GetDownloadUrl(false),
                StreamPath =source.Id.GetStreamUrl(false),
                ThumbStreamPath = source.IsPic ? source.Id.GetStreamUrl(true) : null,
                FileName = source.FileName,
                IsPic = source.IsPic,
                Size = source.Size,
                TitleEn = source.TitleEn,
                TitleFa = source.TitleFa
            };
            return model;
        }
    }
    #endregion


    #region BaseResult<MediaFile> => MediaFileProtoGetByIdResponse
    public class BaseResultToMediaFileProtoGetByIdResponse : ITypeConverter<BaseResult<MediaFile>, MediaFileProtoGetByIdResponse>
    {
        public MediaFileProtoGetByIdResponse Convert(BaseResult<MediaFile> source,
                                                MediaFileProtoGetByIdResponse destination,
                                                ResolutionContext context)
        {


            var model = new MediaFileProtoGetByIdResponse
            {
                IsSuccess = source.IsSuccess
            };
            if (source.Value != null)
            {
                var mappedValue = context.Mapper.Map<MediaFile, MediaFileProtoModel>(source.Value);
                model.Value = mappedValue;
            }
            model.Errors.Add(source.Errors);
            return model;
        }
    }
    #endregion



    #region BaseResult<MediaFile> => MediaFileProtoGetByIdsResponse
    public class BaseResultToMediaFileProtoGetByIdsResponse : ITypeConverter<BaseResult<List<MediaFile>>, MediaFileProtoGetByIdsResponse>
    {
        public MediaFileProtoGetByIdsResponse Convert(BaseResult<List<MediaFile>> source,
                                                MediaFileProtoGetByIdsResponse destination,
                                                ResolutionContext context)
        {


            var model = new MediaFileProtoGetByIdsResponse
            {
                IsSuccess = source.IsSuccess
            };
            if (source.Value != null)
            {
                var mappedValue = context.Mapper.Map<List<MediaFileProtoModel>>(source.Value);
                model.Value.AddRange(mappedValue);
            }
            model.Errors.Add(source.Errors);
            return model;
        }
    }
    #endregion
}
