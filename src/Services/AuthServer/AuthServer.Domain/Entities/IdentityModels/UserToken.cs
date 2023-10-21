using Microsoft.AspNetCore.Identity;

namespace AuthServer.Domain.Entities
{
    public class UserToken : IdentityUserToken<long>
    {
        public virtual User User { get; set; }
    }
}
