using Fan.Abp.FreeSql;
using Fan.Abp.FreeSql.Infrastructure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace Fan.Abp.Uow.FreeSql
{
    public class FreeSqlTransactionApi : ITransactionApi, ISupportsRollback
    {
        public IDbContextTransaction DbContextTransaction { get; }
        public IFreeSqlDbContext StarterDbContext { get; }
        public List<IFreeSqlDbContext> AttendedDbContexts { get; }

        protected ICancellationTokenProvider CancellationTokenProvider { get; }

        public FreeSqlTransactionApi(IDbContextTransaction dbContextTransaction,
            IFreeSqlDbContext starterDbContext,
            ICancellationTokenProvider cancellationTokenProvider)
        {
            StarterDbContext = starterDbContext;
            CancellationTokenProvider = cancellationTokenProvider;
            DbContextTransaction = dbContextTransaction;
            AttendedDbContexts = new List<IFreeSqlDbContext>();
        }


        public async Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var dbContext in AttendedDbContexts)
            {
                //if (dbContext.As<DbContext>().HasRelationalTransactionManager() &&
                //    dbContext.Database.GetDbConnection() == DbContextTransaction.GetDbTransaction().Connection)
                //{
                //    continue; //Relational databases use the shared transaction if they are using the same connection
                //}

                //await dbContext.Database.CommitTransactionAsync(CancellationTokenProvider.FallbackToProvider(cancellationToken));
            }

            await DbContextTransaction.CommitAsync(CancellationTokenProvider.FallbackToProvider(cancellationToken));
        }

        public void Dispose()
        {
            DbContextTransaction.Dispose();
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var dbContext in AttendedDbContexts)
            {
                //if (dbContext.As<DbContext>().HasRelationalTransactionManager() &&
                //    dbContext.Database.GetDbConnection() == DbContextTransaction.GetDbTransaction().Connection)
                //{
                //    continue; //Relational databases use the shared transaction if they are using the same connection
                //}

                //await dbContext.Database.RollbackTransactionAsync(CancellationTokenProvider.FallbackToProvider(cancellationToken));
            }

            await DbContextTransaction.RollbackAsync(CancellationTokenProvider.FallbackToProvider(cancellationToken));
        }
    }
}
