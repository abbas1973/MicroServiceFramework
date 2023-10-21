using Application.DTOs;
using Application.Exceptions;
using AuthServer.Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.Features.Roles
{
    #region Request
    public class RoleDeleteCommand
    : IRequest<BaseResult>, IBaseEntityDTO
    {
        #region Constructors
        public RoleDeleteCommand(long id) {
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
    public class RoleDeleteCommandHandler : IRequestHandler<RoleDeleteCommand, BaseResult>
    {
        private readonly IUnitOfWork _uow;
        public RoleDeleteCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<BaseResult> Handle(RoleDeleteCommand request, CancellationToken cancellationToken)
        {
            var role = await _uow.Roles.FirstOrDefaultAsync(
                x => x.Id == request.Id,
                includes: x => x.Include(z => z.UserRoles).Include(z => z.RoleClaims));
            if (role.UserRoles.Any())
                throw new BadRequestException("ابتدا کاربران مرتبط با این نقش را حذف کنید!");
            _uow.RoleClaims.RemoveRange(role.RoleClaims);
            _uow.Roles.Remove(request.Id);
            await _uow.CommitAsync();
            return new BaseResult(true);
        }
    }

    #endregion
}
