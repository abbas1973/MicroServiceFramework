using Application.DTOs;
using Application.Interface;
using AuthServer.Domain.Entities;

namespace AuthServer.Application.Interface
{
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
        /// <summary>
        /// گرفتن نقش های یک کاربر
        /// </summary>
        /// <param name="userId">آیدی کاربر</param>
        /// <returns></returns>
        Task<List<SelectListDTO>> GetUserRoles(long userId);



        /// <summary>
        /// گرفتن آیدی نقش های یک کاربر
        /// </summary>
        /// <param name="userId">آیدی کاربر</param>
        /// <returns></returns>
        Task<List<long>> GetUserRoleIds(long userId);
    }
}
