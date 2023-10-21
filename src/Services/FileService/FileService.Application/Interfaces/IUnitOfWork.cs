using Application.Interface;

namespace FileService.Application.Interface
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public interface IUnitOfWork : IUnitOfWorkBase, IDisposable
    {
        IMediaFileRepository MediaFiles { get; }

    }
}
