using Application.DTOs;
using System.Linq.Expressions;

namespace AuthServer.Application.Features.Roles
{
    public class RoleListItemDTO: IBaseEntityDTO
    {
        #region Properties
        /// <summary>
        /// شناسه
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// فعال/غیرفعال
        /// </summary>
        public bool IsEnabled { get; set; }
        #endregion



        #region Selectors
        #region سلکتور از جدول نقش ها
        public static Expression<Func<Domain.Entities.Role, RoleListItemDTO>> RoleSelector
        {
            get
            {
                return model => new RoleListItemDTO()
                {
                    Id = model.Id,
                    Name = model.Name,
                    IsEnabled = model.IsEnabled
                };
            }
        }
        #endregion
        #endregion
    }
}
