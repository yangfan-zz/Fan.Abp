using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreeSql
{
    public static class FreeSqlDbSetExtensions
    {
        public static Task<List<TEntity>> ToListAsync<TEntity>(this DbSet<TEntity> dbSet,
            CancellationToken cancellationToken = default) where TEntity : class
        {
            return dbSet.Select.ToListAsync(cancellationToken);
        }

        public static Task<long> LongCountAsync<TEntity>(
            this DbSet<TEntity> dbSet,
            CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class
        {
            return dbSet.Select.CountAsync(cancellationToken);
        }

        public static IQueryable<TEntity> AsQueryable<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class
        {
            return dbSet.Select.AsQueryable();
        }
    }
}
