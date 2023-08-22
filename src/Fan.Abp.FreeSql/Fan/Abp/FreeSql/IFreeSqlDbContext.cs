using System.Threading.Tasks;
using System.Threading;

namespace Fan.Abp.FreeSql
{
    public interface IFreeSqlDbContext
    {
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void Initialize(FreeSqlDbContextInitializationContext initializationContext);
    }
}
