using System.Threading.Tasks;
using System.Threading;
using Fan.Abp.FreeSql.Infrastructure;

namespace Fan.Abp.FreeSql
{
    public interface IFreeSqlDbContext
    {
        DatabaseFacade Database { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void Initialize(FreeSqlDbContextInitializationContext initializationContext);
    }
}
