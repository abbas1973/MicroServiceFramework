using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using AuthServer.Application.Interface;
using Microsoft.AspNetCore.Identity;
using AuthServer.Domain.Entities;

namespace AuthServer.Infrastructure.Repositories
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        protected UserManager<User> _userManager { get; }
        protected RoleManager<Role> _roleManager { get; }
        public UnitOfWork(DbContext context, UserManager<User> userManager, RoleManager<Role> roleManager) : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        #region Repositories
        #region User 
        private IUserRepository _users;
        public IUserRepository Users
        {
            get
            {
                if (_users == null)
                    _users = new UserRepository(_context, _userManager);
                return _users;
            }
        }
        #endregion 


        #region Role 
        private IRoleRepository _roles;
        public IRoleRepository Roles
        {
            get
            {
                if (_roles == null)
                    _roles = new RoleRepository(_context, _roleManager);
                return _roles;
            }
        }
        #endregion


        #region UserRole 
        private IUserRoleRepository _userRoles;
        public IUserRoleRepository UserRoles
        {
            get
            {
                if (_userRoles == null)
                    _userRoles = new UserRoleRepository(_context);
                return _userRoles;
            }
        }
        #endregion


        #region UserClaim
        private IUserClaimRepository _userClaims;
        public IUserClaimRepository UserClaims
        {
            get
            {
                if (_userClaims == null)
                    _userClaims = new UserClaimRepository(_context);
                return _userClaims;
            }
        }
        #endregion


        #region RoleClaim
        private IRoleClaimRepository _roleClaims;
        public IRoleClaimRepository RoleClaims
        {
            get
            {
                if (_roleClaims == null)
                    _roleClaims = new RoleClaimRepository(_context);
                return _roleClaims;
            }
        }
        #endregion
        #endregion




    }
}
