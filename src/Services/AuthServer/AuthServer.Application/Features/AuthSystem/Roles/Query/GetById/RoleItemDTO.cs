using Application.DTOs;
using System.Linq.Expressions;

namespace AuthServer.Application.Features.Roles
{
    public class RoleItemDTO: IBaseEntityDTO
    {
        #region Constructors
        public RoleItemDTO()
        {
            Claims = new List<string>();
        }
        #endregion


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


        /// <summary>
        /// دسترسی های نقش
        /// </summary>
        public List<string> Claims { get; set; }
        #endregion



        #region Selectors
        #region سلکتور از جدول نقش ها
        public static Expression<Func<Domain.Entities.Role, RoleItemDTO>> RoleSelector
        {
            get
            {
                return model => new RoleItemDTO()
                {
                    Id = model.Id,
                    Name = model.Name,
                    IsEnabled = model.IsEnabled,
                    Claims = model.RoleClaims.Select(x => x.ClaimValue).ToList()
                };
            }
        }
        #endregion
        #endregion
    }
}
