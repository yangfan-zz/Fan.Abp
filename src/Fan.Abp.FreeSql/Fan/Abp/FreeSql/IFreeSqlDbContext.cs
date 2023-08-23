using System.Threading.Tasks;
using System.Threading;
using Fan.Abp.FreeSql.Infrastructure;
using FreeSql;

namespace Fan.Abp.FreeSql
{
    public interface IFreeSqlDbContext
    {
        DatabaseFacade Database { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void Initialize(FreeSqlDbContextInitializationContext initializationContext);
    }
}
