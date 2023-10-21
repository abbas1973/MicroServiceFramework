using Microsoft.AspNetCore.Identity;

namespace AuthServer.Domain.Entities
{
    public class UserClaim : IdentityUserClaim<long>
    {
        public virtual User User { get; set; }
    }
}
