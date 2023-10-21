using Application.Interface;
using AuthServer.Domain.Entities;

namespace AuthServer.Application.Interface
{
    public interface IRoleClaimRepository : IBaseRepository<RoleClaim>
    {

        /// <summary>
        /// گرفتن کلایم های یک نقش
        /// </summary>
        /// <param name="roleId">آیدی نقش</param>
        /// <returns></returns>
        Task<List<string>> GetRoleClaims(long roleId);



        /// <summary>
        /// گرفتن کلایم های چندین نقش
        /// </summary>
        /// <param name="roleIds">آیدی نقش ها</param>
        /// <returns></returns>
        Task<List<string>> GetRolesClaims(List<long> roleId);
    }
}
