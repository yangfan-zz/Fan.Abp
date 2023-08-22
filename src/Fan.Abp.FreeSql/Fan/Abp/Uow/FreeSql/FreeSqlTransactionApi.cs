using Fan.Abp.FreeSql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace Fan.Abp.Uow.FreeSql
{
    public class FreeSqlTransactionApi : ITransactionApi, ISupportsRollback
    {
        public IFreeSqlDbContext StarterDbContext { get; }
        public List<IFreeSqlDbContext> AttendedDbContexts { get; }

        protected ICancellationTokenProvider CancellationTokenProvider { get; }

        public FreeSqlTransactionApi(IFreeSqlDbContext starterDbContext,
            ICancellationTokenProvider cancellationTokenProvider)
        {
            StarterDbContext = starterDbContext;
            CancellationTokenProvider = cancellationTokenProvider;
            AttendedDbContexts = new List<IFreeSqlDbContext>();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RollbackAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
