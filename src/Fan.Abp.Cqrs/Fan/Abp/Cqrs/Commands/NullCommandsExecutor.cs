using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;

namespace Fan.Abp.Cqrs.Commands
{
    public class NullCommandsExecutor : ICommandsExecutor, ISingletonDependency
    {
        public ILogger<NullCommandsExecutor> Logger { get; set; }

        public NullCommandsExecutor()
        {
            Logger = NullLogger<NullCommandsExecutor>.Instance;
        }

        public Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command,
            CancellationToken cancellationToken = default)
        {
            Logger.LogWarning($"CommandsExecutor ExecuteAsync was not implemented! Using {nameof(NullCommandsExecutor)}:");
            Logger.LogWarning("Command : " + command);

            return Task.FromResult((TResult) default);
        }

        public Task ExecuteAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            Logger.LogWarning($"CommandsExecutor ExecuteAsync was not implemented! Using {nameof(NullCommandsExecutor)}:");
            Logger.LogWarning("Command : " + command);

            return Task.CompletedTask;
        }
    }
}
