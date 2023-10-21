using Application.DTOs;
using AuthServer.Application.Interface;
using AuthServer.Domain.Entities;
using LinqKit;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace AuthServer.Application.Features.Users
{
    #region Request
    public class UserGetListQuery : IRequest<BaseResult<ItemListDTO<UserListItemDTO>>>, IPaginationDTO
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
        /// نام کاربری
        /// </summary>
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        /// <summary>
        /// نام کاربر
        /// </summary>
        [Display(Name = "نام کاربر")]
        public string Name { get; set; }


        /// <summary>
        /// موبایل
        /// </summary>
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        /// <summary>
        /// ثبت از تاریخ
        /// </summary>
        [Display(Name = "ثبت از تاریخ")]
        public DateTime? StartDate { get; set; }


        /// <summary>
        /// ثبت تا تاریخ
        /// </summary>
        [Display(Name = "ثبت تا تاریخ")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// فعال/غیرفعال
        /// </summary>
        [Display(Name = "فعال/غیرفعال")]
        public bool? IsEnabled { get; set; }
        #endregion


        #region Functions
        #region گرفتن فیلتر متناظر
        public Expression<Func<User, bool>> GetFilter()
        {
            var filter = PredicateBuilder.New<User>(true);

            //ثبت از تاریخ
            if (StartDate != null)
            {
                StartDate = StartDate + new TimeSpan(0, 0, 0);
                filter.And(x => x.CreateDate > StartDate);
            }

            // ثبت تا تاریخ
            if (EndDate != null)
            {
                EndDate = EndDate + new TimeSpan(23, 59, 59);
                filter.And(x => x.CreateDate <= EndDate);
            }

            // نام کاربری
            if (!string.IsNullOrEmpty(Username))
                filter.And(x => x.UserName.Contains(Username));

            // نام
            if (!string.IsNullOrEmpty(Name))
                filter.And(x => x.Name.Contains(Name));

            // موبایل
            if (!string.IsNullOrEmpty(Mobile))
                filter.And(x => x.PhoneNumber.Contains(Mobile));

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
    public class UserGetListQueryHandler : IRequestHandler<UserGetListQuery, BaseResult<ItemListDTO<UserListItemDTO>>>
    {
        protected IUnitOfWork _uow { get; }
        public UserGetListQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<BaseResult<ItemListDTO<UserListItemDTO>>> Handle(UserGetListQuery request, CancellationToken cancellationToken)
        {
            var res = await _uow.Users.GetList(request);
            return new BaseResult<ItemListDTO<UserListItemDTO>>(res);
        }
    } 
    #endregion
}
