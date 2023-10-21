using Application.DTOs;
using AuthServer.Application.Features.Users;
using AuthServer.Grpc.Protos;
using AutoMapper;
using Azure;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using IdentityServer4.Models;

namespace AuthServer.Grpc.Mapping
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// تبدیل DTO به مدل GRPC
        /// </summary>
        public MappingProfile()
        {

            //مپینگ برای گرفتن نام، موبایل یا یوزرنیم
            CreateMap(typeof(BaseResult<string>), typeof(UserProtoBaseResponse))
                .ConvertUsing(typeof(BaseResultToUserProtoBaseResponseConverter));

            // مپینگ برای اطلاعات پایه کاربر
            CreateMap<UserBaseInfoDTO, UserProtoInfoModel>().ReverseMap();
            CreateMap(typeof(BaseResult<UserBaseInfoDTO>), typeof(UserProtoGetInfoResponse))
                .ConvertUsing(typeof(BaseResultToUserProtoInfoResponseConverter));

        }
    }



}
