using System.Threading;
using System.Threading.Tasks;

namespace Fan.Abp.Cqrs.Commands
{
    public interface ICommandHandler
    {

    }

    public interface ICommandHandler<in TCommand, TResult> : ICommandHandler
        where TCommand : ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Void> where TCommand : ICommand
    {

    }
}
