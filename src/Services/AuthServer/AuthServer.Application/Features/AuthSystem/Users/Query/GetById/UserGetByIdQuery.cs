using Application.DTOs;
using AuthServer.Application.Interface;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.Features.Users
{
    #region Request
    public class UserGetByIdQuery : IRequest<BaseResult<UserItemDTO>>, IBaseEntityDTO
    {
        #region Constructors
        public UserGetByIdQuery(long id)
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
    public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, BaseResult<UserItemDTO>>
    {
        protected IUnitOfWork _uow { get; }
        public UserGetByIdQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<BaseResult<UserItemDTO>> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _uow.Users.GetDetails(request.Id);
            if(res == null)
                return new BaseResult<UserItemDTO>(false, "کاربر یافت نشد!");
            return new BaseResult<UserItemDTO>(res);
        }
    } 
    #endregion
}
