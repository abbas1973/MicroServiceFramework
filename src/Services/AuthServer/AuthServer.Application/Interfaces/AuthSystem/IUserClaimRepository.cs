using Application.Interface;
using AuthServer.Domain.Entities;

namespace AuthServer.Application.Interface
{
    public interface IUserClaimRepository : IBaseRepository<UserClaim>
    {
        /// <summary>
        /// گرفتن کلایم های یک کاربر
        /// </summary>
        /// <param name="userId">آیدی کاربر</param>
        /// <returns></returns>
        Task<List<string>> GetUserClaims(long userId);
       
    }
}
