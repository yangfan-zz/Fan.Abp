using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.FreeSql;
using FreeSql;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Fan.Abp.Domain.Repositories.FreeSql
{
    public class FreeSqlRepository<TDbContext, TEntity> : RepositoryBase<TEntity>, IFreeSqlRepository<TEntity>
        where TDbContext : IFreeSqlDbContext
        where TEntity : class, IEntity
    {
        #region DbContext GetDbContext GetDbContextAsync 

        [Obsolete("Use GetDbContextAsync() method.")]
        protected virtual TDbContext DbContext => GetDbContext();

        [Obsolete("Use GetDbContextAsync() method.")]
        DbContext IFreeSqlRepository<TEntity>.DbContext => GetDbContext() as DbContext;

        async Task<DbContext> IFreeSqlRepository<TEntity>.GetDbContextAsync()
        {
            return await GetDbContextAsync() as DbContext;
        }

        [Obsolete("Use GetDbContextAsync() method.")]
        private TDbContext GetDbContext()
        {
            // Multi-tenancy unaware entities should always use the host connection string
            if (!EntityHelper.IsMultiTenant<TEntity>())
            {
                using (CurrentTenant.Change(null))
                {
                    return _dbContextProvider.GetDbContext();
                }
            }

            return _dbContextProvider.GetDbContext();
        }

        protected virtual Task<TDbContext> GetDbContextAsync()
        {
            // Multi-tenancy unaware entities should always use the host connection string
            if (!EntityHelper.IsMultiTenant<TEntity>())
            {
                using (CurrentTenant.Change(null))
                {
                    return _dbContextProvider.GetDbContextAsync();
                }
            }

            return _dbContextProvider.GetDbContextAsync();
        }

        #endregion

        #region DbSet GetDbSetAsync

        [Obsolete("Use GetDbSetAsync() method.")]
        public virtual DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

        Task<DbSet<TEntity>> IFreeSqlRepository<TEntity>.GetDbSetAsync()
        {
            return GetDbSetAsync();
        }

        protected async Task<DbSet<TEntity>> GetDbSetAsync()
        {
            return (await GetDbContextAsync()).Set<TEntity>();
        }

        #endregion

        #region GetDbConnectionAsync

        protected async Task<IDbConnection> GetDbConnectionAsync()
        {
            return (await GetDbContextAsync()).Database.GetDbConnection();
        }

        protected async Task<IDbTransaction> GetDbTransactionAsync()
        {
            return (await GetDbContextAsync()).Database.CurrentTransaction?.GetDbTransaction();
        }


        #endregion

        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        public virtual IGuidGenerator GuidGenerator => LazyServiceProvider.LazyGetService<IGuidGenerator>(SimpleGuidGenerator.Instance);

        #region 构造函数

        public FreeSqlRepository(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        #endregion


        public override async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            CheckAndSetId(entity);

            var dbContext = await GetDbContextAsync();

            await dbContext.Set<TEntity>().AddAsync(entity, GetCancellationToken(cancellationToken));

            if (autoSave)
            {
                await dbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }

            // TODO 考虑重新查找出实体
            var savedEntity = entity;

            return savedEntity;
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            dbContext.Set<TEntity>().Attach(entity);

            await dbContext.Set<TEntity>().UpdateAsync(entity, cancellationToken);

            if (autoSave)
            {
                await dbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }

            // TODO 考虑重新查找出实体
            var updatedEntity = entity;
            return updatedEntity;
        }

        public override Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override async Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await (await WithDetailsAsync()).ToListAsync(GetCancellationToken(cancellationToken))
                : await (await GetDbSetAsync()).ToListAsync(GetCancellationToken(cancellationToken));
        }

        public override Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task<long> GetCountAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, bool includeDetails = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override async Task<IQueryable<TEntity>> GetQueryableAsync()
        {
           return (await GetDbSetAsync()).Select.AsQueryable();
        }

        public override Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override Task DeleteDirectAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

      

        [Obsolete("Use GetQueryableAsync method.")]
        protected override IQueryable<TEntity> GetQueryable()
        {
            return DbSet.Select.AsQueryable();
        }

        public override async Task<IQueryable<TEntity>> WithDetailsAsync(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return IncludeDetails(
                await GetQueryableAsync(),
                propertySelectors
            );
        }

        private static IQueryable<TEntity> IncludeDetails(
            IQueryable<TEntity> query,
            Expression<Func<TEntity, object>>[] propertySelectors)
        {
            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }


        #region 检测并设置主键唯一标识

        protected virtual void CheckAndSetId(TEntity entity)
        {
            if (entity is IEntity<Guid> entityWithGuidId)
            {
                TrySetGuidId(entityWithGuidId);
            }
        }
        protected virtual void TrySetGuidId(IEntity<Guid> entity)
        {
            if (entity.Id != default)
            {
                return;
            }

            EntityHelper.TrySetId(
                entity,
                () => GuidGenerator.Create(),
                true
            );
        }

        #endregion

    }
}
