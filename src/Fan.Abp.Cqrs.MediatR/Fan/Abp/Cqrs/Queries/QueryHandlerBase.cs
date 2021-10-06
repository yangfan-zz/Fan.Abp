using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Fan.Abp.Cqrs.Queries
{
    internal abstract class QueryHandlerBase
    {
        protected static THandler GetHandler<THandler>(ServiceFactory factory)
        {
            THandler handler;

            try
            {
                handler = factory.GetInstance<THandler>();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    $"Error constructing handler for request of type {typeof(THandler)}. Register your handlers with the container. See the samples in GitHub for examples.",
                    e);
            }

            if (handler == null)
            {
                throw new InvalidOperationException(
                    $"Handler was not found for request of type {typeof(THandler)}. Register your handlers with the container. See the samples in GitHub for examples.");
            }

            return handler;
        }
    }

    internal abstract class QueryHandlerWrapper<TResult> : QueryHandlerBase
    {
        public abstract Task<TResult> HandleAsync(IQuery<TResult> query, CancellationToken cancellationToken,
            ServiceFactory serviceFactory);
    }

    internal class QueryHandlerWrapperImpl<TRequest, TResult> : QueryHandlerWrapper<TResult> where TRequest : IQuery<TResult>
    {
        public override Task<TResult> HandleAsync(IQuery<TResult> request, CancellationToken cancellationToken,
            ServiceFactory serviceFactory)
        {
            Task<TResult> Handler() => GetHandler<IQueryHandler<TRequest, TResult>>(serviceFactory)
                .HandleAsync((TRequest) request, cancellationToken);

            return serviceFactory
                .GetInstances<IPipelineBehavior<TRequest, TResult>>()
                .Reverse()
                .Aggregate((RequestHandlerDelegate<TResult>) Handler,
                    (next, pipeline) => () => pipeline.Handle((TRequest) request, cancellationToken, next))();
        }
    }
}
