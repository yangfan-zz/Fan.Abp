using Fan.Abp.Ddd.Application.Commands;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Fan.Abp.Ddd.Application.CommandHandlers
{
    public abstract class CreateCommandHandler<TCommand, TResult> : CommandHandler<TCommand, TResult>,
        ICreateCommandHandler<TCommand, TResult>
        where TCommand : ICreateCommand<TResult>
    {

    }

    public abstract class CreateCommandHandler<TEntity, TCommand, TResult> : CreateCommandHandler<TCommand, TResult>
        where TCommand : ICreateCommand<TResult>
        where TEntity : class, IEntity
    {
        protected IRepository<TEntity> Repository => LazyServiceProvider.LazyGetRequiredService<IRepository<TEntity>>();
    }


    public abstract class CreateCommandHandler<TEntity, TKey, TCommand, TResult> :
        CreateCommandHandler<TCommand, TResult>,
        ICreateCommandHandler<TKey, TCommand, TResult>
        where TCommand : ICreateCommand<TKey, TResult>
        where TEntity : class, IEntity<TKey>
    {

        protected IRepository<TEntity, TKey> Repository =>
            LazyServiceProvider.LazyGetRequiredService<IRepository<TEntity, TKey>>();
    }
}
