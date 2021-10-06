using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;

namespace Fan.Abp.Cqrs.Queries
{
    public class NullQueryExecutor : IQueryExecutor, ISingletonDependency
    {
        public ILogger<NullQueryExecutor> Logger { get; set; }

        public NullQueryExecutor()
        {
            Logger = NullLogger<NullQueryExecutor>.Instance;
        }

        public Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            Logger.LogWarning($"QueryExecutor ExecuteAsync was not implemented! Using {nameof(NullQueryExecutor)}:");
            Logger.LogWarning("Query : " + query);

            return Task.FromResult((TResult) default);
        }
    }
}
