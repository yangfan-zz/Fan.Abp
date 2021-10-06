using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp.DependencyInjection;

namespace Fan.Abp.Cqrs.Commands
{
    public class CommandsExecutor : ICommandsExecutor, ITransientDependency
    {
        private readonly ServiceFactory _serviceFactory;
        private static readonly ConcurrentDictionary<Type, CommandHandlerBase> CommandHandlers = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceFactory"></param>
        public CommandsExecutor(ServiceFactory serviceFactory) => _serviceFactory = serviceFactory;

        public Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command,
            CancellationToken cancellationToken = default)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return GetCommandHandlerWrapper<TResult>(command.GetType())
                .HandleAsync(command, cancellationToken, _serviceFactory);
        }

        public Task ExecuteAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var handler = GetCommandHandlerWrapper<Void>(command.GetType());

            return handler.HandleAsync(command, cancellationToken, _serviceFactory);
        }

        private static CommandHandlerWrapper<TResult> GetCommandHandlerWrapper<TResult>(Type commandType)
        {

            return (CommandHandlerWrapper<TResult>) CommandHandlers.GetOrAdd(commandType,
                static t => (CommandHandlerBase) (Activator.CreateInstance(
                                                      typeof(CommandHandlerWrapperImpl<,>).MakeGenericType(t,
                                                          typeof(TResult)))
                                                  ?? throw new InvalidOperationException(
                                                      $"Could not create wrapper type for {t}")));
        }
    }
}
