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
    public abstract class FreeSqlDbContext: DbContext, IFreeSqlDbContext
    {
        private DatabaseFacade? _database;

        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        protected virtual IFreeSql FreeSql => LazyServiceProvider.LazyGetRequiredService<IFreeSql>();

        public virtual DatabaseFacade Database
        {
            get { return this._database ??= new DatabaseFacade(this); }
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
