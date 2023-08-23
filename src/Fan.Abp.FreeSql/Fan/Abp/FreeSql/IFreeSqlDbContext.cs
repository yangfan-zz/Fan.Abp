using System.Data;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Common;

namespace Fan.Abp.FreeSql
{
    public interface IFreeSqlDbContext
    {
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);
        Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        void Initialize(FreeSqlDbContextInitializationContext initializationContext);
    }
}
