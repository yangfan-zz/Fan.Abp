using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.FreeSql;
using Volo.Abp.Uow;

namespace Fan.Abp.Uow.FreeSql
{
    public class FreeSqlDatabaseApi : IDatabaseApi, ISupportsSavingChanges
    {
        public FreeSqlDatabaseApi(IFreeSqlDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IFreeSqlDbContext DbContext { get; }

        public Task SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
