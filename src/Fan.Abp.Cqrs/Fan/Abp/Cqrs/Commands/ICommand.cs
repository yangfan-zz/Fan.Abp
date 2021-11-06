using System;

namespace Fan.Abp.Cqrs.Commands
{
    public interface ICommand<out TResult>
    {
        Guid CommandId { get; }
    }

    public interface ICommand : ICommand<Void>
    {

    }
}
