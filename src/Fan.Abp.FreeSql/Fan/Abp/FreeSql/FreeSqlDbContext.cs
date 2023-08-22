using System;
using System.Threading;
using System.Threading.Tasks;
using FreeSql;
using Volo.Abp.DependencyInjection;

namespace Fan.Abp.FreeSql
{
    public abstract class FreeSqlDbContext: IFreeSqlDbContext
    {
        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        protected virtual DbContext Context => LazyServiceProvider.LazyGetRequiredService<IFreeSql>().CreateDbContext(); 

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
        }
    }
}
