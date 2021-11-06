using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.Data;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="currentTenant"></param>
        /// <param name="errorHandler"></param>
        public MediatRLocalEventBus(IOptions<AbpLocalEventBusOptions> options, IServiceScopeFactory serviceScopeFactory,
            ICurrentTenant currentTenant, IEventErrorHandler errorHandler) : base(options, serviceScopeFactory,
            currentTenant, errorHandler)
        {

        }
        public override async Task PublishAsync(LocalEventMessage localEventMessage)
        {
            await TriggerHandlersAsync(localEventMessage.EventType, localEventMessage.EventData, errorContext =>
            {
                errorContext.EventData = localEventMessage.EventData;
                errorContext.SetProperty(nameof(LocalEventMessage.MessageId), localEventMessage.MessageId);
            });
        }


        public override async Task TriggerHandlersAsync(Type eventType, object eventData, Action<EventExecutionErrorContext> onErrorAction = null)
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
                var context = new EventExecutionErrorContext(exceptions, eventType, this);
                onErrorAction?.Invoke(context);
                await ErrorHandler.HandleAsync(context);
            }
        }


        protected virtual async Task TriggerMediatRHandlersAsync(Type eventType, object eventData,
            List<Exception> exceptions)
        {
            try
            {
                using var scope = ServiceScopeFactory.CreateScope();
                await scope.ServiceProvider.GetService<IPublisher>()?.Publish(eventData);
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
