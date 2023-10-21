using Application.DTOs;
using System.Linq.Expressions;

namespace AuthServer.Application.Features.Users
{
    public class UserBaseInfoDTO: IBaseEntityDTO
    {
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
        #endregion



        #region Selectors
        #region سلکتور از جدول کاربران
        public static Expression<Func<Domain.Entities.User, UserBaseInfoDTO>> UserSelector
        {
            get
            {
                return model => new UserBaseInfoDTO()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Username = model.UserName,
                    Mobile = model.PhoneNumber
                };
            }
        }
        #endregion
        #endregion
    }
}
