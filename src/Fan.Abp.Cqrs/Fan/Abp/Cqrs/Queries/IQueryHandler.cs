using System.Threading;
using System.Threading.Tasks;

namespace Fan.Abp.Cqrs.Queries
{
    public interface IQueryHandler
    {

    }

    public interface IQueryHandler<in TQuery, TResult> : IQueryHandler
        where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}
