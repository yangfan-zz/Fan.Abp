using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.Cqrs;
using Fan.Abp.Cqrs.Commands;
using Volo.Abp.Uow;
using Volo.Abp.Validation;

namespace Fan.Abp.Ddd.Application.CommandHandlers
{
    public abstract class CommandHandler : ICommandHandler, IValidationEnabled,
        IUnitOfWorkEnabled
    {

    }

    public abstract class CommandHandler<TCommand, TResult> : CommandHandler, ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        public virtual Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken) =>
            HandleCommandAsync(command, cancellationToken);

        public abstract Task<TResult> HandleCommandAsync(TCommand command, CancellationToken cancellationToken);
    }

    public abstract class CommandHandler<TCommand> : CommandHandler, ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public virtual async Task<Void> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            await HandleCommandAsync(command, cancellationToken).ConfigureAwait(false);
            return Void.Value;
        }

        public abstract Task HandleCommandAsync(TCommand command, CancellationToken cancellationToken);
    }
}
