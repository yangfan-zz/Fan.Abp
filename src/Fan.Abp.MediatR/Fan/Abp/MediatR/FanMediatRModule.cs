using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Modularity;

namespace Fan.Abp.MediatR
{
    public class FanMediatRModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            AddRequiredServices(context.Services, ServiceLifetime.Transient);
        }

        private void AddRequiredServices(IServiceCollection services, ServiceLifetime serviceLifetime)
        {
            // Use TryAdd, so any existing ServiceFactory/IMediator registration doesn't get overriden
            services.TryAddTransient<ServiceFactory>(p => p.GetService);
            services.TryAdd(new ServiceDescriptor(typeof(IMediator), typeof(Mediator), serviceLifetime));
            services.TryAdd(new ServiceDescriptor(typeof(ISender), sp => sp.GetService<IMediator>(), serviceLifetime));
            services.TryAdd(
                new ServiceDescriptor(typeof(IPublisher), sp => sp.GetService<IMediator>(), serviceLifetime));

            // Use TryAddTransientExact (see below), we dó want to register our Pre/Post processor behavior, even if (a more concrete)
            // registration for IPipelineBehavior<,> already exists. But only once.
            services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
            services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionActionProcessorBehavior<,>));
            services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));
        }
    }
}
