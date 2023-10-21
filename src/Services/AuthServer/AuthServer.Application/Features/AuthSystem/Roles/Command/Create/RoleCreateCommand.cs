using Application.DTOs;
using AuthServer.Application.Interface;
using AuthServer.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using static Application.IdentityConfigs.IdentityScopes.AuthService;

namespace AuthServer.Application.Features.Roles
{
    #region Request
    public class RoleCreateCommand
    : IRequest<BaseResult<long>>
    {
        #region Constructors
        public RoleCreateCommand() {
            Claims = new List<string>();
        }
        #endregion


        #region Properties
        [Display(Name = "عنوان نقش")]
        public string Name { get; set; }


        [Display(Name = "دسترسی های نقش")]
        public List<string> Claims { get; set; }
        #endregion


        #region Mapping
        public Role MapToRole()
        {
            var Role = new Role
            {
                Name = Name,
                IsEnabled = true,
                RoleClaims = Claims.Select(
                    x => new RoleClaim
                    {
                        ClaimType = x,
                        ClaimValue = x
                    }).ToList()
            };
            return Role;
        }
        #endregion
    }
    #endregion



    #region Handler
    public class RoleCreateCommandHandler : IRequestHandler<RoleCreateCommand, BaseResult<long>>
    {
        private readonly IUnitOfWork _uow;
        public RoleCreateCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<BaseResult<long>> Handle(RoleCreateCommand request, CancellationToken cancellationToken)
        {
            var role = request.MapToRole();
            await _uow.Roles.AddAsync(role);
            await _uow.CommitAsync();
            return new BaseResult<long>(role.Id);
        }
    }

    #endregion
}
