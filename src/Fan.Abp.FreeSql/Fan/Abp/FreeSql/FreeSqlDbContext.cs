using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.FreeSql.Infrastructure;
using FreeSql;

namespace Fan.Abp.FreeSql
{
    public abstract class FreeSqlDbContext: DbContext, IFreeSqlDbContext
    {
        private bool _abpUnitOfWorkTransactional;

        protected FreeSqlDbContext() : this(null, null)
        {

        }

        protected FreeSqlDbContext(IFreeSql fsql, DbContextOptions options) : base(fsql, options)
        {

        }

        #region Database

        private DatabaseFacade? _database;

        public virtual DatabaseFacade Database
        {
            get { return this._database ??= new DatabaseFacade(this); }
        }

        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // 是否使用了abp 工作单元并开启事务
            if (_abpUnitOfWorkTransactional)
            {
                // TODO 使用了工作单元事务 则需要修改 DbContext 逻辑 只向数据库提交数据，不提交事务，提交事务交给工作单元实现
                return Task.FromResult(0);
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public void Initialize(FreeSqlDbContextInitializationContext initializationContext)
        {
            _abpUnitOfWorkTransactional = initializationContext.UnitOfWork.Options.IsTransactional;

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
