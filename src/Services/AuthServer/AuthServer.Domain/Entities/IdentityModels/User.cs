using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Domain.Entities
{
    public class User : IdentityUser<long>, IBaseEntity, IIsEnabled
    {
        #region Constructors
        public User() { }
        #endregion


        #region Properties
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        #endregion


        #region Navigation Properties
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<UserLogin> Logins { get; set; }
        public virtual ICollection<UserToken> Tokens { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } 
        #endregion
    }
}
