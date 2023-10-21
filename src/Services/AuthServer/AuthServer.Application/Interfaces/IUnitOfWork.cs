using Application.Interface;

namespace AuthServer.Application.Interface
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public interface IUnitOfWork : IUnitOfWorkBase, IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IUserRoleRepository UserRoles { get; }
        IRoleClaimRepository RoleClaims { get; }
        IUserClaimRepository UserClaims { get; }

    }
}
