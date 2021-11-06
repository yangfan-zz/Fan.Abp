using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.Cqrs.Queries;
using Volo.Abp.Validation;

namespace Fan.Abp.Ddd.Application.QueryHandlers
{
    public abstract class QueryHandler : IQueryHandler, IValidationEnabled
    {

    }

    public abstract class QueryHandler<TQuery, TResult> : QueryHandler, IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        public virtual Task<TResult> HandleAsync(TQuery request, CancellationToken cancellationToken)=> HandleQueryAsync(request, cancellationToken);


        public abstract Task<TResult> HandleQueryAsync(TQuery query, CancellationToken cancellationToken);
    }
}
