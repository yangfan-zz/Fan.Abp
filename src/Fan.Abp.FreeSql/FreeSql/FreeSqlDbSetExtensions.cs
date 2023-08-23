using System.Collections.Generic;
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
    }
}
