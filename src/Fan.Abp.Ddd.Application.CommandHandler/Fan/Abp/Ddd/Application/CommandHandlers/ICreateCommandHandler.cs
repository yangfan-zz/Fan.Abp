using Fan.Abp.Cqrs.Commands;

namespace Fan.Abp.Ddd.Application.CommandHandlers
{
    public interface ICreateCommandHandler<in TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICreateCommand<TResult>
    {

    }

    public interface ICreateCommandHandler<TKey, in TCommand, TResult> : ICreateCommandHandler<TCommand, TResult>
        where TCommand : ICreateCommand<TKey, TResult>
    {

    }
}
