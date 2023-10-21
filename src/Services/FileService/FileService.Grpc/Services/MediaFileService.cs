using AutoMapper;
using FileService.Application.Features.MediaFiles;
using FileService.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace FileService.Grpc.Services
{
    public class MediaFileService : MediaFilesProto.MediaFilesProtoBase
    {

        #region Constructors
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public MediaFileService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        #endregion



        #region افزودن فایل جدید
        public override async Task<MediaFileProtoCreateResponse> Create(MediaFileProtoCreateRequest request, ServerCallContext context)
        {
            var command = _mapper.Map<MediaFileCreateCommand>(request);
            var res = await _mediator.Send(command);
            return _mapper.Map<MediaFileProtoCreateResponse>(res);
        }
        #endregion





        #region ویرایش مدیا فایل
        public override async Task<MediaFileProtoBaseResponse> Update(MediaFileProtoUpdateRequest request, ServerCallContext context)
        {
            var command = _mapper.Map<MediaFileUpdateCommand>(request);
            var res = await _mediator.Send(command);
            return _mapper.Map<MediaFileProtoBaseResponse>(res);
        }
        #endregion





        #region حذف مدیا فایل
        public override async Task<MediaFileProtoBaseResponse> Delete(MediaFileProtoRequestById request, ServerCallContext context)
        {
            var res = await _mediator.Send(new MediaFileDeleteCommand(request.Id));
            return _mapper.Map<MediaFileProtoBaseResponse>(res);
        }
        #endregion




        #region گرفتن مدیا فایل با آیدی
        public override async Task<MediaFileProtoGetByIdResponse> GetById(MediaFileProtoRequestById request, ServerCallContext context)
        {
            var res = await _mediator.Send(new MediaFileGetByIdQuery(request.Id));
            return _mapper.Map<MediaFileProtoGetByIdResponse>(res);
        }
        #endregion





        #region گرفتن مدیا فایل با آیدی
        public override async Task<MediaFileProtoGetByIdsResponse> GetByIds(MediaFileProtoGetByIdsRequest request, ServerCallContext context)
        {
            var res = await _mediator.Send(new MediaFileGetByIdsQuery(request.Ids.ToList()));
            return _mapper.Map<MediaFileProtoGetByIdsResponse>(res);
        }
        #endregion

    }
}
