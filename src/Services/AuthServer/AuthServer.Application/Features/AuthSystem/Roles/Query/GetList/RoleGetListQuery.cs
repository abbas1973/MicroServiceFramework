using Application.DTOs;
using AuthServer.Application.Interface;
using AuthServer.Domain.Entities;
using LinqKit;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace AuthServer.Application.Features.Roles
{
    #region Request
    public class RoleGetListQuery : IRequest<BaseResult<ItemListDTO<RoleListItemDTO>>>, IPaginationDTO
    {
        #region Properties
        /// <summary>
        /// شماره صفحه
        /// </summary>
        [Display(Name = "شماره صفحه")]
        public int? Page { get; set; }

        /// <summary>
        /// تعداد آیتم های هر صفحه
        /// </summary>
        [Display(Name = "تعداد آیتم های صفحه")]
        public int? PageLength { get; set; }

        /// <summary>
        /// نام کاربر
        /// </summary>
        [Display(Name = "نام کاربر")]
        public string Name { get; set; }

        /// <summary>
        /// فعال/غیرفعال
        /// </summary>
        [Display(Name = "فعال/غیرفعال")]
        public bool? IsEnabled { get; set; }
        #endregion


        #region Functions
        #region گرفتن فیلتر متناظر
        public Expression<Func<Role, bool>> GetFilter()
        {
            var filter = PredicateBuilder.New<Role>(true);

            // نام
            if (!string.IsNullOrEmpty(Name))
                filter.And(x => x.Name.Contains(Name));

            // فعال/غیرفعال
            if (IsEnabled != null)
                filter.And(x => x.IsEnabled == IsEnabled);


            return filter;
        }
        #endregion
        #endregion
    }
    #endregion


    #region Handler
    public class RoleGetListQueryHandler : IRequestHandler<RoleGetListQuery, BaseResult<ItemListDTO<RoleListItemDTO>>>
    {
        protected IUnitOfWork _uow { get; }
        public RoleGetListQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<BaseResult<ItemListDTO<RoleListItemDTO>>> Handle(RoleGetListQuery request, CancellationToken cancellationToken)
        {
            var res = await _uow.Roles.GetList(request);
            return new BaseResult<ItemListDTO<RoleListItemDTO>>(res);
        }
    } 
    #endregion
}
