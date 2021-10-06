using System.Threading;
using System.Threading.Tasks;

namespace Fan.Abp.Cqrs.Commands
{
    public interface ICommandsExecutor
    {
        Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);

        Task ExecuteAsync(ICommand command, CancellationToken cancellationToken = default);
    }
}
