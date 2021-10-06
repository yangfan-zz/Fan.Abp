using System.Threading;
using System.Threading.Tasks;

namespace Fan.Abp.Cqrs.Queries
{
    public interface IQueryExecutor
    {
        Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
    }
}
