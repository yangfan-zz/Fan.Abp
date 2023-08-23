using System;
using System.Threading.Tasks;
using Fan.Abp.Domain.Repositories.FreeSql;
using FreeSql;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Fan.Abp.Domain.Repositories
{
    public static class FreeSqlRepositoryExtensions
    {
        public static Task<DbContext> GetDbContextAsync<TEntity>(this IReadOnlyBasicRepository<TEntity> repository)
            where TEntity : class, IEntity
        {
            return repository.ToFreeSqlRepository().GetDbContextAsync();
        }

        public static Task<DbSet<TEntity>> GetDbSetAsync<TEntity>(this IReadOnlyBasicRepository<TEntity> repository)
            where TEntity : class, IEntity
        {
            return repository.ToFreeSqlRepository().GetDbSetAsync();
        }

        public static IFreeSqlRepository<TEntity> ToFreeSqlRepository<TEntity>(
            this IReadOnlyBasicRepository<TEntity> repository)
            where TEntity : class, IEntity
        {
            if (repository is IFreeSqlRepository<TEntity> efCoreRepository)
            {
                return efCoreRepository;
            }

            throw new ArgumentException(
                "Given repository does not implement " + typeof(IFreeSqlRepository<TEntity>).AssemblyQualifiedName,
                nameof(repository));
        }
    }
}
