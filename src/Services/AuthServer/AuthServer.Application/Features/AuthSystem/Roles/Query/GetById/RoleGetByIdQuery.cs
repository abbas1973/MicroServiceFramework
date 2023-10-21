using Application.DTOs;
using AuthServer.Application.Interface;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.Features.Roles
{
    #region Request
    public class RoleGetByIdQuery : IRequest<BaseResult<RoleItemDTO>>, IBaseEntityDTO
    {
        #region Constructors
        public RoleGetByIdQuery(long id)
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
    public class RoleGetByIdQueryHandler : IRequestHandler<RoleGetByIdQuery, BaseResult<RoleItemDTO>>
    {
        protected IUnitOfWork _uow { get; }
        public RoleGetByIdQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<BaseResult<RoleItemDTO>> Handle(RoleGetByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _uow.Roles.GetDetails(request.Id);
            if(res == null)
                return new BaseResult<RoleItemDTO>(false, "کاربر یافت نشد!");
            return new BaseResult<RoleItemDTO>(res);
        }
    } 
    #endregion
}
