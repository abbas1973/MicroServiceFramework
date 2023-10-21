using Application.DTOs;
using AuthServer.Application.Interface;
using AuthServer.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace AuthServer.Application.Features.Roles
{
    #region Request
    public class RoleGetDropdownQuery : IRequest<BaseResult<List<SelectListDTO>>>
    {

        #region Selectors
        #region سلکتور از جدول نقش ها
        public static Expression<Func<Role, SelectListDTO>> RoleSelector
        {
            get
            {
                return model => new SelectListDTO()
                {
                    Id = model.Id,
                    Title = model.Name
                };
            }
        }
        #endregion
        #endregion
    }
    #endregion


    #region Handler
    public class RoleGetDropdownQueryHandler : IRequestHandler<RoleGetDropdownQuery, BaseResult<List<SelectListDTO>>>
    {
        protected IUnitOfWork _uow { get; }
        public RoleGetDropdownQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<BaseResult<List<SelectListDTO>>> Handle(RoleGetDropdownQuery request, CancellationToken cancellationToken)
        {
            var res = await _uow.Roles.GetDTOAsync(
                RoleGetDropdownQuery.RoleSelector,
                x => x.IsEnabled
                );
            return new BaseResult<List<SelectListDTO>>(res);
        }
    } 
    #endregion
}
