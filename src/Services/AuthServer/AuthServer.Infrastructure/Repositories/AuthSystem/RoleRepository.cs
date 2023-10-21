using AuthServer.Application.Interface;
using Microsoft.AspNetCore.Identity;
using AuthServer.Domain.Entities;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using AuthServer.Application.Features.Roles;

namespace AuthServer.Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        protected RoleManager<Role> _roleManager { get; }
        public RoleRepository(DbContext context, RoleManager<Role> roleManager) : base(context)
        {
            _roleManager = roleManager;
        }


        #region جستجو لیست نقش ها
        /// <summary>
        /// جستجو لیست نقش ها
        /// </summary>
        public async Task<ItemListDTO<RoleListItemDTO>> GetList(RoleGetListQuery search)
        {
            var filter = search.GetFilter();
            var totalCount = await CountAsync();
            var filteredCount = await CountAsync(filter);
            var data = await GetDTOAsync(
                RoleListItemDTO.RoleSelector,
                filter,
                x => x.OrderByDescending(z => z.Id),
                (search.Page.Value - 1) * search.PageLength.Value,
                search.PageLength.Value
                );

            var model = new ItemListDTO<RoleListItemDTO>(
                search.Page.Value,
                search.PageLength.Value,
                totalCount,
                filteredCount,
                data
                );

            return model;
        }
        #endregion


        #region گرفتن نقش به همراه دسترسی ها
        /// <summary>
        /// گرفتن نقش به همراه دسترسی ها
        /// </summary>
        public async Task<Role> GetWithClaims(long id)
        {
            var role = await FirstOrDefaultAsync(
                x => x.Id == id,
                includes: x => x.Include(z => z.RoleClaims));
            return role;
        }
        #endregion


        #region گرفتن اطلاعات نقش با آیدی
        /// <summary>
        /// اطلاعات نقش با آیدی
        /// </summary>
        public async Task<RoleItemDTO> GetDetails(long id)
        {
            var role = await GetOneDTOAsync(
                RoleItemDTO.RoleSelector,
                x => x.Id == id);
            return role;
        }
        #endregion
    }
}
