using Application.DTOs;
using Application.Exceptions;
using AuthServer.Application.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.Features.Users
{
    #region Request
    public class UserGetMobileByIdQuery : IRequest<BaseResult<string>>, IBaseEntityDTO
    {
        #region Constructors
        public UserGetMobileByIdQuery(long id)
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
    public class UserGetMobileByIdQueryHandler : IRequestHandler<UserGetMobileByIdQuery, BaseResult<string>>
    {
        protected IUnitOfWork _uow { get; }
        protected ILogger<UserGetMobileByIdQueryHandler> _logger { get; }
        public UserGetMobileByIdQueryHandler(IUnitOfWork uow, ILogger<UserGetMobileByIdQueryHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<BaseResult<string>> Handle(UserGetMobileByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _uow.Users.GetOneDTOAsync(x => x.PhoneNumber, x => x.Id == request.Id);
            if (res == null)
                throw new NotFoundException($"موبایل کاربر با آیدی {request.Id} یافت نشد!");
            return new BaseResult<string>(res);
        }
    }
    #endregion
}
