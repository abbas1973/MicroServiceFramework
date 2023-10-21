using Application.DTOs;
using Application.Exceptions;
using AuthServer.Application.Interface;
using AuthServer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.Features.Roles
{
    #region Request
    public class RoleUpdateCommand
    : IRequest<BaseResult>, IBaseEntityDTO
    {
        #region Constructors
        public RoleUpdateCommand() { }
        #endregion


        #region Properties
        [Display(Name = "شناسه")]
        public long Id { get; set; }

        [Display(Name = "عنوان نقش")]
        public string Name { get; set; }

        [Display(Name = "فعال/غیر فعال")]
        public bool IsEnabled { get; set; }

        [Display(Name = "دسترسی های نقش")]
        public List<string> Claims { get; set; }
        #endregion


        #region Mapping
        public void MapToRole(ref Role Role)
        {
            Role.Id = Id;
            Role.Name = Name;
            Role.IsEnabled = IsEnabled;

            Role.RoleClaims.Clear();
            Role.RoleClaims = Claims.Select(
                    x => new RoleClaim
                    {
                        ClaimType = x,
                        ClaimValue = x
                    }).ToList();
        }
        #endregion
    }
    #endregion



    #region Handler
    public class RoleUpdateCommandHandler : IRequestHandler<RoleUpdateCommand, BaseResult>
    {
        private readonly IUnitOfWork _uow;
        public RoleUpdateCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<BaseResult> Handle(RoleUpdateCommand request, CancellationToken cancellationToken)
        {
            #region ویرایش نقش و افزودن دسترسی های جدید
            var role = await _uow.Roles.FirstOrDefaultAsync(
                x => x.Id == request.Id,
                includes: x => x.Include(z => z.RoleClaims));
            if (role == null)
                throw new NotFoundException("کاربر مورد نظر یافت نشد!");

            _uow.RoleClaims.RemoveRange(role.RoleClaims);
            request.MapToRole(ref role);
            _uow.Roles.Update(role);
            #endregion

            await _uow.CommitAsync();
            return new BaseResult(true);
        }
    }

    #endregion
}
