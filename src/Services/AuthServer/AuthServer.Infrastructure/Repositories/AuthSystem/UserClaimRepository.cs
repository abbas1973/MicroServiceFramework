using AuthServer.Application.Interface;
using AuthServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;

namespace AuthServer.Infrastructure.Repositories
{
    public class UserClaimRepository : BaseRepository<UserClaim>, IUserClaimRepository
    {
        public UserClaimRepository(DbContext context) : base(context)
        {
        }





        #region گرفتن کلایم های یک کاربر
        /// <summary>
        /// گرفتن کلایم های یک کاربر
        /// </summary>
        /// <param name="userId">آیدی کاربر</param>
        /// <returns></returns>
        public async Task<List<string>> GetUserClaims(long userId)
        {
            return await GetDTOAsync(
                x => x.ClaimValue,
                x => x.UserId == userId);
        } 
        #endregion
    }
}
