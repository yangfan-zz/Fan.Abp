using System.Data;
using FreeSql;
using System.Data.Common;
using System.Threading.Tasks;
using System.Threading;
using JetBrains.Annotations;

namespace Fan.Abp.FreeSql.Infrastructure
{
    /// <summary>
    /// 仿照EF DbContext 实现
    /// </summary>
    public class DatabaseFacade
    {
        private readonly DbContext _context;

        public virtual IDbContextTransaction CurrentTransaction =>
            new DbContextTransaction(_context);

        public DatabaseFacade(DbContext context)
        {
            _context = context;
        }

        public virtual DbConnection GetDbConnection()
        {
            // TODO 调研 FreeSql 如何获取当前链接
            return CurrentTransaction.GetDbTransaction().Connection;
        }

        #region BeginTransactionAsync

        public Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel,
            CancellationToken cancellationToken = default)
        {
            _context.UnitOfWork.IsolationLevel = isolationLevel;
            return Task.FromResult(CurrentTransaction);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(CurrentTransaction);
        }

        #endregion
    }
}
