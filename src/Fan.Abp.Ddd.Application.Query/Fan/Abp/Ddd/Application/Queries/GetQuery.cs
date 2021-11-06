using Fan.Abp.Cqrs.Queries;

namespace Fan.Abp.Ddd.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class GetQuery<TKey, TResult> : Query<TResult>
    {
        public TKey Id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class GetQuery<TResult> : GetQuery<string, TResult>
    {

    }
}
