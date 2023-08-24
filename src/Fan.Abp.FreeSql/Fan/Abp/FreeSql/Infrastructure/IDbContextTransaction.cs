using System.Data.Common;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Fan.Abp.FreeSql.Infrastructure
{
    public interface IDbContextTransaction : IDisposable
    {
        void Commit();
        Task CommitAsync(CancellationToken cancellationToken = default);

        void Rollback();

        Task RollbackAsync(CancellationToken cancellationToken = default);

        DbTransaction GetDbTransaction();
    }
}
