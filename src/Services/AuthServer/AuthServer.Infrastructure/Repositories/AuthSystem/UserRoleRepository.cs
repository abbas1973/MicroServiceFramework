using AuthServer.Application.Interface;
using AuthServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Application.DTOs;

namespace AuthServer.Infrastructure.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(DbContext context) : base(context)
        {
        }




        #region گرفتن نقش های یک کاربر
        /// <summary>
        /// گرفتن نقش های یک کاربر
        /// </summary>
        /// <param name="userId">آیدی کاربر</param>
        /// <returns></returns>
        public async Task<List<SelectListDTO>> GetUserRoles(long userId)
        {
            return await GetDTOAsync(
                x => new SelectListDTO(x.RoleId,x.Role.Name),
                x => x.UserId == userId);
        }
        #endregion




        #region گرفتن آیدی نقش های یک کاربر
        /// <summary>
        /// گرفتن آیدی نقش های یک کاربر
        /// </summary>
        /// <param name="userId">آیدی کاربر</param>
        /// <returns></returns>
        public async Task<List<long>> GetUserRoleIds(long userId)
        {
            return await GetDTOAsync(
                x => x.RoleId,
                x => x.UserId == userId);
        }
        #endregion

    }
}
