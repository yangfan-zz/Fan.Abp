using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Local;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Fan.Abp.EventBus.Local
{
    /// <summary>
    /// Implements EventBus as Singleton pattern.
    /// </summary>
    [ExposeServices(typeof(ILocalEventBus), typeof(MediatRLocalEventBus))]
    public class MediatRLocalEventBus: LocalEventBus
    {
        public MediatRLocalEventBus(IOptions<AbpLocalEventBusOptions> options, IServiceScopeFactory serviceScopeFactory,
            ICurrentTenant currentTenant, IUnitOfWorkManager unitOfWorkManager,
            IEventHandlerInvoker eventHandlerInvoker) : base(options, serviceScopeFactory, currentTenant,
            unitOfWorkManager, eventHandlerInvoker)
        {

        }

        public override async Task TriggerHandlersAsync(Type eventType, object eventData)
        {
            var exceptions = new List<Exception>();
            if (typeof(INotification).IsAssignableFrom(eventType))
            {
                await TriggerMediatRHandlersAsync(eventType, eventData, exceptions);
            }
            else
            {
                await TriggerHandlersAsync(eventType, eventData, exceptions);
            }

            if (exceptions.Any())
            {
                ThrowOriginalExceptions(eventType, exceptions);
            }
        }


        protected virtual async Task TriggerMediatRHandlersAsync(Type eventType, object eventData,
            List<Exception> exceptions)
        {
            try
            {
                using var scope = ServiceScopeFactory.CreateScope();
                await scope.ServiceProvider.GetRequiredService<IPublisher>().Publish(eventData)!;
            }
            catch (TargetInvocationException ex)
            {
                exceptions.Add(ex.InnerException);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }
    }
}
