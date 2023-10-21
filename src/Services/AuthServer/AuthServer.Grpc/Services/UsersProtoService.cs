using AuthServer.Application.Features.Users;
using AuthServer.Grpc.Protos;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;

namespace AuthServer.Grpc.Services
{
    public class UsersProtoService : UsersProto.UsersProtoBase
    {
        #region Constructors
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UsersProtoService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        #endregion



        #region گرفتن نام کاربر
        public override async Task<UserProtoBaseResponse> GetName(UserProtoRequestById request, ServerCallContext context)
        {
            var res = await _mediator.Send(new UserGetNameByIdQuery(request.Id));
            var model = _mapper.Map<UserProtoBaseResponse>(res);
            return model;
        }
        #endregion



        #region گرفتن نام کاربری کاربر
        public override async Task<UserProtoBaseResponse> GetUsername(UserProtoRequestById request, ServerCallContext context)
        {
            var res = await _mediator.Send(new UserGetUserNameByIdQuery(request.Id));
            var model = _mapper.Map<UserProtoBaseResponse>(res);
            return model;
        }
        #endregion



        #region گرفتن موبایل کاربر
        public override async Task<UserProtoBaseResponse> GetMobile(UserProtoRequestById request, ServerCallContext context)
        {
            var res = await _mediator.Send(new UserGetMobileByIdQuery(request.Id));
            var model = _mapper.Map<UserProtoBaseResponse>(res);
            return model;
        }
        #endregion



        #region گرفتن اطلاعات کاربر
        public override async Task<UserProtoGetInfoResponse> GetInfo(UserProtoRequestById request, ServerCallContext context)
        {
            var res = await _mediator.Send(new UserGetBaseInfoQuery(request.Id));
            var model = _mapper.Map<UserProtoGetInfoResponse>(res);
            return model;
        }
        #endregion
    }
}