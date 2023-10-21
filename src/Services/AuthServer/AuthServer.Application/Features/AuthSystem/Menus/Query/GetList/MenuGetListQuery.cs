using Application.DTOs;
using Application.IdentityConfigs;
using AuthServer.Application.Interface;
using MediatR;

namespace AuthServer.Application.Features.Menus
{
    #region Request
    public class MenuGetListQuery : IRequest<BaseResult<IdentityClaimDTO>>
    {
    }
    #endregion


    #region Handler
    public class MenuGetListQueryHandler : IRequestHandler<MenuGetListQuery, BaseResult<IdentityClaimDTO>>
    {
        protected IUnitOfWork _uow { get; }
        public MenuGetListQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<BaseResult<IdentityClaimDTO>> Handle(MenuGetListQuery request, CancellationToken cancellationToken)
        {
            var res = IdentityScopes.GetClaimsTree();
            return new BaseResult<IdentityClaimDTO>(res);
        }
    } 
    #endregion
}
