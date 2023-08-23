using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FreeSql;
using FreeSql.Extensions.Linq;

namespace System.Linq
{
    public static class FreeSqlQueryableExtensions
    {
        public static ISelect<TEntity> AsSelect<TEntity>(this IQueryable<TEntity> queryable) where TEntity : class
        {
            if (queryable is QueryableProvider<TEntity, TEntity> selectQueryable)
            {
                return selectQueryable.AsSelect();
            }

            throw new Exception("IQueryable 不是 FreeSql 对象！");
        }


        public static Task<List<TEntity>> ToListAsync<TEntity>(this IQueryable<TEntity> queryable, CancellationToken cancellationToken = default) where TEntity : class
        {
            return queryable.AsSelect().ToListAsync(cancellationToken);
        }

        public static IQueryable<TEntity> Include<TEntity, TNavigate>(this IQueryable<TEntity> queryable,
            Expression<Func<TEntity, TNavigate>> navigateSelector)
            where TEntity : class
            where TNavigate : class
        {
            return queryable.AsSelect().Include(navigateSelector).AsQueryable();
        }
    }
}
