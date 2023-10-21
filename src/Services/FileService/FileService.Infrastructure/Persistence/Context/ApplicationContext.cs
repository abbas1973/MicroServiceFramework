using Microsoft.EntityFrameworkCore;
using FileService.Domain.Entities;
using Application.Interface;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data.Entity;

namespace FileService.Infrastructure.Persistence
{
    public class ApplicationContext : BaseContext
    {
        #region Constructors
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IJwtManager jwtManager)
            : base(options, jwtManager, typeof(MediaFile).Assembly, typeof(ApplicationContext).Assembly) { }
        #endregion


        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }

}
