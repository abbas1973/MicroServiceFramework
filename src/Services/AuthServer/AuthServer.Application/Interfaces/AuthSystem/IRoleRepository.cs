using Application.DTOs;
using Application.Interface;
using AuthServer.Application.Features.Roles;
using AuthServer.Domain.Entities;

namespace AuthServer.Application.Interface
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        /// <summary>
        /// جستجو لیست نقش ها
        /// </summary>
        Task<ItemListDTO<RoleListItemDTO>> GetList(RoleGetListQuery search);


        /// <summary>
        /// گرفتن نقش به همراه دسترسی ها
        /// </summary>
        Task<Role> GetWithClaims(long id);


        /// <summary>
        /// اطلاعات نقش با آیدی
        /// </summary>
        Task<RoleItemDTO> GetDetails(long id);
    }
}
