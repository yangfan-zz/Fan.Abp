namespace Fan.Abp.Cqrs.Commands
{
    public abstract class CreateCommand<TKey, TResult> : Command<TResult>, ICreateCommand<TKey, TResult>
    {
        public TKey Id { get; set; }
    }

    public abstract class CreateCommand<TResult> : CreateCommand<string, TResult>
    {

    }
}
