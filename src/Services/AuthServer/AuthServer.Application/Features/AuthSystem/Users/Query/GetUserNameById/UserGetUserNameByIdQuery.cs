using Application.DTOs;
using Application.Exceptions;
using AuthServer.Application.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.Features.Users
{
    #region Request
    public class UserGetUserNameByIdQuery : IRequest<BaseResult<string>>, IBaseEntityDTO
    {
        #region Constructors
        public UserGetUserNameByIdQuery(long id)
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
    public class UserGetUserNameByIdQueryHandler : IRequestHandler<UserGetUserNameByIdQuery, BaseResult<string>>
    {
        protected IUnitOfWork _uow { get; }
        protected ILogger<UserGetUserNameByIdQueryHandler> _logger { get; }
        public UserGetUserNameByIdQueryHandler(IUnitOfWork uow, ILogger<UserGetUserNameByIdQueryHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<BaseResult<string>> Handle(UserGetUserNameByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _uow.Users.GetOneDTOAsync(x => x.UserName, x => x.Id == request.Id);
            if (res == null)
                throw new NotFoundException($"نام کاربری کاربر با آیدی {request.Id} یافت نشد!");
            return new BaseResult<string>(res);
        }
    }
    #endregion
}
