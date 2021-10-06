using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.Ddd.Application.Commands;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Fan.Abp.Ddd.Application.CommandHandlers
{
    public abstract class CreateCommandHandler<TCommand, TResult> : CommandHandler,
        ICreateCommandHandler<TCommand, TResult>
        where TCommand : ICreateCommand<TResult>
    {
        public virtual Task<TResult> HandleAsync(TCommand request, CancellationToken cancellationToken) =>
            HandleCommandAsync(request, cancellationToken);
        
        public abstract Task<TResult> HandleCommandAsync(TCommand command, CancellationToken cancellationToken);
    }

    public abstract class CreateCommandHandler<TEntity, TCommand, TResult> : CreateCommandHandler<TCommand, TResult>
        where TCommand : ICreateCommand<TResult>
        where TEntity : class, IEntity
    {
        protected CreateCommandHandler(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        protected IRepository<TEntity> Repository { get; }
    }


    public abstract class CreateCommandHandler<TEntity, TKey, TCommand, TResult> :
        CreateCommandHandler<TCommand, TResult>,
        ICreateCommandHandler<TKey, TCommand, TResult>
        where TCommand : ICreateCommand<TKey, TResult>
        where TEntity : class, IEntity<TKey>
    {
        protected CreateCommandHandler(IRepository<TEntity, TKey> repository)
        {
            Repository = repository;
        }

        protected IRepository<TEntity, TKey> Repository { get; }
    }
}
