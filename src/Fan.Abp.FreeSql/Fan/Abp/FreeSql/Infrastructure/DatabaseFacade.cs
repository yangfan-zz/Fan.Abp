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

        [CanBeNull]
        public virtual IDbContextTransaction CurrentTransaction =>
            new DbContextTransaction(_context.UnitOfWork.GetOrBeginTransaction());

        public DatabaseFacade(DbContext context)
        {
            _context = context;
        }

        public virtual DbConnection GetDbConnection()
        {
            // TODO 调研 FreeSql 如何获取当前链接
            return _context.UnitOfWork.GetOrBeginTransaction().Connection;
        }

        #region BeginTransactionAsync

        public Task<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel,
            CancellationToken cancellationToken = default)
        {
            _context.UnitOfWork.IsolationLevel = isolationLevel;
            return Task.FromResult(_context.UnitOfWork.GetOrBeginTransaction());
        }

        public Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_context.UnitOfWork.GetOrBeginTransaction());
        }

        #endregion
    }
}
