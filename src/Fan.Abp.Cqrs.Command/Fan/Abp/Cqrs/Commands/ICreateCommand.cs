namespace Fan.Abp.Cqrs.Commands
{
    public interface ICreateCommand<out TResult> : ICommand<TResult>
    {

    }

    public interface ICreateCommand<TKey, out TResult> : ICreateCommand<TResult>
    {
        public TKey Id { get; set; }
    }
}
