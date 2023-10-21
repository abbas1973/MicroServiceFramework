using Application.DTOs;
using System.Linq.Expressions;

namespace AuthServer.Application.Features.Users
{
    public class UserListItemDTO: IBaseEntityDTO
    {
        #region Constructors
        public UserListItemDTO()
        {
            Roles = new List<SelectListDTO>();
        }
        #endregion


        #region Properties
        /// <summary>
        /// شناسه
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// نام کاربری
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// موبایل
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// ایمیل
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// فعال/غیرفعال
        /// </summary>
        public bool IsEnabled { get; set; }


        /// <summary>
        /// نقش ها
        /// </summary>
        public IEnumerable<SelectListDTO> Roles { get; set; }
        #endregion



        #region Selectors
        #region سلکتور از جدول کاربران
        public static Expression<Func<Domain.Entities.User, UserListItemDTO>> UserSelector
        {
            get
            {
                return model => new UserListItemDTO()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Username = model.UserName,
                    Email = model.Email,
                    Mobile = model.PhoneNumber,
                    IsEnabled = model.IsEnabled,
                    Roles = model.UserRoles.Select(x => new SelectListDTO(x.RoleId, x.Role.Name)).ToList()
                };
            }
        }
        #endregion
        #endregion
    }
}
