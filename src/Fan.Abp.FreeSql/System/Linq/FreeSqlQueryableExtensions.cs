using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FreeSql;
using FreeSql.Extensions.Linq;
using JetBrains.Annotations;

namespace System.Linq
{
    internal static class FreeSqlQueryableExtensions
    {
        public static ISelect<TEntity> AsSelect<TEntity>(this IQueryable<TEntity> queryable)
        {
            if (queryable is QueryableProvider<TEntity, TEntity> selectQueryable)
            {
                return selectQueryable.AsSelect();
            }

            throw new Exception("IQueryable 不是 FreeSql 对象！");
        }

        public static Task<List<TEntity>> ToListAsync<TEntity>(this IQueryable<TEntity> queryable, CancellationToken cancellationToken = default)
        {
            return queryable.AsSelect().ToListAsync(cancellationToken);
        }

        [ItemCanBeNull]
        public static async Task<TEntity> SingleOrDefaultAsync<TEntity>(this IQueryable<TEntity> queryable,
            CancellationToken cancellationToken = default) where TEntity : class
        {
            var result = await queryable.AsSelect().Take(2).ToListAsync(cancellationToken);
            return result.SingleOrDefault();
        }

        [ItemCanBeNull]
        public static async Task<TEntity> SingleAsync<TEntity>(this IQueryable<TEntity> queryable,
            CancellationToken cancellationToken = default)
        {
            var result = await queryable.AsSelect().Take(2).ToListAsync(cancellationToken);
            return result.Single();
        }

        [ItemCanBeNull]
        public static async Task<TEntity> SingleAsync<TEntity>(this IQueryable<TEntity> queryable,
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default) // where TEntity : class
        {
            var result = await queryable.AsSelect().Where(predicate).Take(2).ToListAsync(cancellationToken);
            return result.Single();
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
