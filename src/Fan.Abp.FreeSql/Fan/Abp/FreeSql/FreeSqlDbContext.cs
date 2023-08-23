using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.FreeSql.Infrastructure;
using FreeSql;
using Volo.Abp.DependencyInjection;

namespace Fan.Abp.FreeSql
{
    public abstract class FreeSqlDbContext: IFreeSqlDbContext
    {
        private DatabaseFacade? _database;

        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        protected virtual DbContext Context => LazyServiceProvider.LazyGetRequiredService<IFreeSql>().CreateDbContext();

        public virtual DatabaseFacade Database
        {
            get { return this._database ??= new DatabaseFacade(Context); }
        }


        public virtual int SaveChanges()
        {
           return Context.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }

        public void Initialize(FreeSqlDbContextInitializationContext initializationContext)
        {
            //if (initializationContext.UnitOfWork.Options.Timeout.HasValue &&
            //    Database.IsRelational() &&
            //    !Database.GetCommandTimeout().HasValue)
            //{
            //    Database.SetCommandTimeout(TimeSpan.FromMilliseconds(initializationContext.UnitOfWork.Options.Timeout.Value));
            //}

            //ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;

            //ChangeTracker.Tracked += ChangeTracker_Tracked;
            //ChangeTracker.StateChanged += ChangeTracker_StateChanged;

            //if (UnitOfWorkManager is AlwaysDisableTransactionsUnitOfWorkManager)
            //{
            //    Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
            //}
        }
    }
}
