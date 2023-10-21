using Application.DTOs;
using Application.Exceptions;
using AuthServer.Application.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.Features.Users
{
    #region Request
    public class UserGetBaseInfoQuery : IRequest<BaseResult<UserBaseInfoDTO>>, IBaseEntityDTO
    {
        #region Constructors
        public UserGetBaseInfoQuery(long id)
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
    public class UserGetBaseInfoQueryHandler : IRequestHandler<UserGetBaseInfoQuery, BaseResult<UserBaseInfoDTO>>
    {
        protected IUnitOfWork _uow { get; }
        protected ILogger<UserGetBaseInfoQueryHandler> _logger { get; }
        public UserGetBaseInfoQueryHandler(IUnitOfWork uow, ILogger<UserGetBaseInfoQueryHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<BaseResult<UserBaseInfoDTO>> Handle(UserGetBaseInfoQuery request, CancellationToken cancellationToken)
        {
            var res = await _uow.Users.GetBaseInfo(request.Id);
            if (res == null)
                throw new NotFoundException($"اطلاعات کاربر با آیدی {request.Id} یافت نشد!");
            return new BaseResult<UserBaseInfoDTO>(res);
        }
    }
    #endregion
}
