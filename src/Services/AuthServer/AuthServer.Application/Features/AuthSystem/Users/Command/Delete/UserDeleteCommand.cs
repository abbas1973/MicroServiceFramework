using Application.DTOs;
using AuthServer.Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.Features.Users
{
    #region Request
    public class UserDeleteCommand
    : IRequest<BaseResult>, IBaseEntityDTO
    {
        #region Constructors
        public UserDeleteCommand(long id) {
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
    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, BaseResult>
    {
        private readonly IUnitOfWork _uow;
        public UserDeleteCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<BaseResult> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var user = await _uow.Users.FirstOrDefaultAsync(
                x => x.Id == request.Id,
                includes: x => x.Include(z => z.UserRoles).Include(z => z.Claims));
            _uow.UserRoles.RemoveRange(user.UserRoles);
            _uow.UserClaims.RemoveRange(user.Claims);
            _uow.Users.Remove(request.Id);
            await _uow.CommitAsync();
            return new BaseResult(true);
        }
    }

    #endregion
}
