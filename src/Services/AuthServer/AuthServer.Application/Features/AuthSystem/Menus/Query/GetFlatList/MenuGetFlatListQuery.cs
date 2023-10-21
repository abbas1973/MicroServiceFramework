using Application.DTOs;
using Application.IdentityConfigs;
using AuthServer.Application.Interface;
using MediatR;

namespace AuthServer.Application.Features.Menus
{
    #region Request
    public class MenuGetFlatListQuery : IRequest<BaseResult<List<IdentityClaimDTO>>>
    {
    }
    #endregion


    #region Handler
    public class MenuGetFlatListQueryHandler : IRequestHandler<MenuGetFlatListQuery, BaseResult<List<IdentityClaimDTO>>>
    {
        protected IUnitOfWork _uow { get; }
        public MenuGetFlatListQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<BaseResult<List<IdentityClaimDTO>>> Handle(MenuGetFlatListQuery request, CancellationToken cancellationToken)
        {
            var res = IdentityScopes.GetClaimsFlat().ToList();
            return new BaseResult<List<IdentityClaimDTO>>(res);
        }
    } 
    #endregion
}
