using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using FileService.Application.Interface;

namespace FileService.Infrastructure.Repositories
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(DbContext context) : base(context)
        {
        }


        #region Repositories
        #region User 
        private IMediaFileRepository _mediaFiles;
        public IMediaFileRepository MediaFiles
        {
            get
            {
                if (_mediaFiles == null)
                    _mediaFiles = new MediaFileRepository(_context);
                return _mediaFiles;
            }
        }
        #endregion 
        #endregion




    }
}
