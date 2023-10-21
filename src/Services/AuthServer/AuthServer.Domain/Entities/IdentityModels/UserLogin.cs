using Microsoft.AspNetCore.Identity;

namespace AuthServer.Domain.Entities
{
    public class UserLogin : IdentityUserLogin<long>
    {
        public virtual User User { get; set; }
    }
}
