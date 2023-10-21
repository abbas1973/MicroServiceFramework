using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AuthServer.Domain.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Application.Interface;

namespace AuthServer.Infrastructure.Persistence
{
    public class ApplicationContext : IdentityDbContext
        <User, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        protected IJwtManager _jwtManager { get; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IJwtManager jwtManager)
            : base(options)
        {
            _jwtManager = jwtManager;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            #region Fluent API - لود کردن از کلاس های جانبی
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            #endregion


            #region DeleteBehavior - رفتار در هنگام حذف دیتا
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            #endregion
        }



        #region SaveChanges Override
        #region SaveChangesAsync
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetCreatorAndModifier();
            SetIsEnabled();
            SetIsDeleted();

            var res = base.SaveChangesAsync(cancellationToken);
            return res;
        }
        #endregion


        #region SaveChanges
        public override int SaveChanges()
        {
            SetCreatorAndModifier();
            SetIsEnabled();
            SetIsDeleted();

            var res = base.SaveChanges();
            return res;
        }
        #endregion



        #region مقدار دهی اطلاعات ایجاد و ویرایش
        public void SetCreatorAndModifier()
        {
            var user = _jwtManager.GetUser();
            if (user == null)
                return;
            foreach (var entry in ChangeTracker.Entries<IBaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateDate = DateTime.Now;
                    entry.Entity.CreatedBy = user.Id;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifyDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = user.Id;
                }
            }
        }
        #endregion



        #region فعال کردن موجودیت هنگام افزودن به دیتابیس
        /// <summary>
        /// در صورت وجود پروپرتی IsEnabled
        /// مقدار آن هنگام افزودن موجودیت جدید فعال میشود
        /// </summary>
        public void SetIsEnabled()
        {
            var entities = ChangeTracker.Entries<IIsEnabled>()
                        .Where(e => e.State == EntityState.Added);
            foreach (var entity in entities)
                entity.Entity.IsEnabled = true;
        }
        #endregion


        #region حذف نرم افزاری المان های مورد نیاز
        /// <summary>
        /// تبدیل حذف فیزیکی به حذف نرم افزاری
        /// </summary>
        public void SetIsDeleted()
        {
            var entities = ChangeTracker.Entries<IIsDeleted>()
                        .Where(e => e.State == EntityState.Deleted);
            foreach (var entity in entities)
            {
                entity.State = EntityState.Modified;
                entity.Entity.IsDeleted = true;
            }
        }
        #endregion

    }
    #endregion




}
