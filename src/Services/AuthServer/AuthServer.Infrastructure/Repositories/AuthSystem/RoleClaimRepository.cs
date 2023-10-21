using AuthServer.Application.Interface;
using AuthServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Application.IdentityConfigs;
using Utilities;

namespace AuthServer.Infrastructure.Repositories
{
    public class RoleClaimRepository : BaseRepository<RoleClaim>, IRoleClaimRepository
    {
        public RoleClaimRepository(DbContext context) : base(context)
        {
        }


        #region گرفتن کلایم های یک نقش
        /// <summary>
        /// گرفتن کلایم های یک نقش
        /// </summary>
        /// <param name="roleId">آیدی نقش</param>
        /// <returns></returns>
        public async Task<List<string>> GetRoleClaims(long roleId)
        {
            return await GetDTOAsync(
                x => x.ClaimValue,
                x => x.RoleId == roleId);
        }
        #endregion



        #region گرفتن کلایم های چندین نقش
        /// <summary>
        /// گرفتن کلایم های چندین نقش
        /// </summary>
        /// <param name="roleIds">آیدی نقش ها</param>
        /// <returns></returns>
        public async Task<List<string>> GetRolesClaims(List<long> roleIds)
        {
            var model = await GetDTOAsync(
                x => x.ClaimValue,
                x => roleIds.Contains(x.RoleId));

            #region اگه دسترسی فول داشتیم، بقیه دسترسی ها اضافه است
            if (model.Any(x => x.Equals(nameof(IdentityScopes.Full))))
                return new List<string> { nameof(IdentityScopes.Full) };
            #endregion


            #region حذف دسترسی های تکراری و زیر مجموعه
            model = model.Distinct().ToList();
            var fullAccess = model.Where(x => x.EndsWith(".Full"));
            var subClaims = new List<string>();
            foreach (var item in fullAccess)
            {
                var tmp = item.ReplaceEnd(".Full", string.Empty);
                var subs = model.Where(x => x.Contains(tmp) && x != item);
                subClaims.AddRange(subs);
            }
            model = model.Except(subClaims).ToList(); 
            #endregion

            return model;
        }
        #endregion



    }
}
