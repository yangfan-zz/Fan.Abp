using System;

namespace Fan.Abp.Cqrs.Commands
{
    public abstract class Command<TResult> : ICommand<TResult>
    {
        protected Command()
        {
            CommandId = Guid.NewGuid();
        }

        protected Command(Guid commandId)
        {
            CommandId = commandId;
        }

        public Guid CommandId { get; }

        public override string ToString()
        {
            return $"[Command: {GetType().Name}] CommandId = {CommandId}";
        }
    }

    public abstract class Command : ICommand
    {
        protected Command()
        {
            CommandId = Guid.NewGuid();
        }

        protected Command(Guid id)
        {
            CommandId = id;
        }

        public Guid CommandId { get; }

        public override string ToString()
        {
            return $"[Command: {GetType().Name}] CommandId = {CommandId}";
        }
    }
}
