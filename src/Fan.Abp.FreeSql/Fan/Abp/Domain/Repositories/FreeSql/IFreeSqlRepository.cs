using FreeSql;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Fan.Abp.Domain.Repositories.FreeSql
{
    public interface IFreeSqlRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        [Obsolete("Use GetDbContextAsync() method.")]
        DbContext DbContext { get; }

        [Obsolete("Use GetDbSetAsync() method.")]
        DbSet<TEntity> DbSet { get; }

        Task<DbContext> GetDbContextAsync();

        Task<DbSet<TEntity>> GetDbSetAsync();
    }


    public interface IFreeSqlRepository<TEntity, TKey> : IFreeSqlRepository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}
