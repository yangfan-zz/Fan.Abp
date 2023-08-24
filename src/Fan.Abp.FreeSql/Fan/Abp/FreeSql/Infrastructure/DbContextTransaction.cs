using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using FreeSql;
using JetBrains.Annotations;

namespace Fan.Abp.FreeSql.Infrastructure
{
    internal class DbContextTransaction : IDbContextTransaction
    {
        private readonly DbContext _dbContext;

        [CanBeNull] protected DbTransaction DbTransaction { get; private set; }

        public DbContextTransaction(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public void Rollback()
        {
            _dbContext.UnitOfWork.Rollback();
        }

        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            _dbContext.UnitOfWork.Rollback();
            return Task.CompletedTask;
        }

        public DbTransaction GetDbTransaction()
        {
            return DbTransaction ??= _dbContext.UnitOfWork.GetOrBeginTransaction();
        }

        public void Dispose()
        {
            DbTransaction?.Dispose();
        }
    }
}
