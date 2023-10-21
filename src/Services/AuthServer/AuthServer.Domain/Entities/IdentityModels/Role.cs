using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Domain.Entities
{
    public class Role : IdentityRole<long>, IBaseEntity, IIsEnabled
    {
        #region Constructors
        public Role() { }
        #endregion


        #region Properties
        public bool IsEnabled { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        #endregion


        #region Navigation Properties
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
        #endregion
    }
}
