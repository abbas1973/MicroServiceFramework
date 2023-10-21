using Application.DTOs;
using AuthServer.Application.Features.Users;
using AuthServer.Grpc.Protos;
using AutoMapper;

namespace AuthServer.Grpc.Mapping
{
    #region تبدیل ریزالت پایه به مدل گرفتن یک فیلد رشته از کاربر
    public class BaseResultToUserProtoBaseResponseConverter : ITypeConverter<BaseResult<string>, UserProtoBaseResponse>
    {
        public UserProtoBaseResponse Convert(BaseResult<string> source,
                                                UserProtoBaseResponse destination,
                                                ResolutionContext context)
        {
            var model = new UserProtoBaseResponse
            {
                IsSuccess = source.IsSuccess
            };
            if (source.Value != null)
                model.Value = source.Value;
            model.Errors.Add(source.Errors);
            return model;
        }
    }
    #endregion




    #region تبدیل ریزالت پایه به مدل گرفتن اطلاعت کاربر
    public class BaseResultToUserProtoInfoResponseConverter : ITypeConverter<BaseResult<UserBaseInfoDTO>, UserProtoGetInfoResponse>
    {
        public UserProtoGetInfoResponse Convert(BaseResult<UserBaseInfoDTO> source,
                                                UserProtoGetInfoResponse destination,
                                                ResolutionContext context)
        {


            var model = new UserProtoGetInfoResponse
            {
                IsSuccess = source.IsSuccess
            };
            if (source.Value != null)
            {
                var mappedValue = context.Mapper.Map<UserBaseInfoDTO, UserProtoInfoModel>(source.Value);
                model.Value = mappedValue;
            }
            model.Errors.Add(source.Errors);
            return model;
        }
    }
    #endregion
}
