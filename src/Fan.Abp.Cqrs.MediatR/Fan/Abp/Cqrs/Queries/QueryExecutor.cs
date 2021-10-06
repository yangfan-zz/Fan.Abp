using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp.DependencyInjection;

namespace Fan.Abp.Cqrs.Queries
{
    public class QueryExecutor : IQueryExecutor, ITransientDependency
    {
        private readonly ServiceFactory _serviceFactory;
        private static readonly ConcurrentDictionary<Type, QueryHandlerBase> QueryHandlers = new();


        public QueryExecutor(ServiceFactory serviceFactory) => _serviceFactory = serviceFactory;

        public virtual Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query,
            CancellationToken cancellationToken = default)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            return GetQueryHandlerWrapper<TResult>(query.GetType())
                .HandleAsync(query, cancellationToken, _serviceFactory);
        }


        private static QueryHandlerWrapper<TResult> GetQueryHandlerWrapper<TResult>(Type commandType)
        {

            return (QueryHandlerWrapper<TResult>) QueryHandlers.GetOrAdd(commandType,
                static t => (QueryHandlerBase) (Activator.CreateInstance(
                                                    typeof(QueryHandlerWrapperImpl<,>).MakeGenericType(t,
                                                        typeof(TResult)))
                                                ?? throw new InvalidOperationException(
                                                    $"Could not create wrapper type for {t}")));
        }
    }
}
