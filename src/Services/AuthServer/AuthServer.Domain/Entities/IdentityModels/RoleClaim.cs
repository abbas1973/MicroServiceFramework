using Microsoft.AspNetCore.Identity;

namespace AuthServer.Domain.Entities
{
    public class RoleClaim : IdentityRoleClaim<long>
    {
        public virtual Role Role { get; set; }
    }
}
