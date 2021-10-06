namespace Fan.Abp.Cqrs.Commands
{
    public interface ICommand<out TResult>
    {

    }

    public interface ICommand : ICommand<Void>
    {

    }
}
