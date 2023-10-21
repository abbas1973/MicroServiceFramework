using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Interface
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public interface IRepository<TEntity> : IBaseRepository<TEntity>, IReadOnlyRepository<TEntity> where TEntity : class
    {
        #region توابع مستقیم برای اعمال روی دیتابیس بدون نیاز به کامیت

        #region آپدیت مستقیم روی دیتابیس بدون نیاز به کامیت
        int ExecuteUpdate(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression);


        Task<int> ExecuteUpdateAsync(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression);
        #endregion




        #region حذف بدون نیاز به کامیت
        int ExecuteDelete(Expression<Func<TEntity, bool>> filter);

        
        Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> filter);
        #endregion
        #endregion

    }


}
