using System.Data;
using FreeSql;
using System.Data.Common;
using System.Threading.Tasks;
using System.Threading;

namespace Fan.Abp.FreeSql.Infrastructure
{
    public class DatabaseFacade
    {
        private readonly DbContext _context;

        public DatabaseFacade(DbContext context)
        {
            _context = context;
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
