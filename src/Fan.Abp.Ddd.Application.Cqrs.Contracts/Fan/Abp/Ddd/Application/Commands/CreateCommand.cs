namespace Fan.Abp.Ddd.Application.Commands
{
    public abstract class CreateCommand<TKey, TResult> : ICreateCommand<TKey, TResult>
    {
        public TKey Id { get; set; }

        public override string ToString()
        {
            return $"[CreateCommand: {GetType().Name}] Id = {Id}";
        }
    }

    public abstract class CreateCommand<TResult> : CreateCommand<string, TResult>
    {
        public override string ToString()
        {
            return $"[CreateCommand: {GetType().Name}] Id = {Id}";
        }
    }
}
