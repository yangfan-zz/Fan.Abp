using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Local;
using Volo.Abp.MultiTenancy;

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
        public MediatRLocalEventBus(IOptions<AbpLocalEventBusOptions> options, IServiceScopeFactory serviceScopeFactory,
            ICurrentTenant currentTenant) : base(options, serviceScopeFactory, currentTenant)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="eventData"></param>
        /// <param name="exceptions"></param>
        /// <returns></returns>
        protected override async Task TriggerHandlersAsync(Type eventType, object eventData, List<Exception> exceptions)
        {
            if (typeof(INotification).IsAssignableFrom(eventType))
            {
                try
                {
                    using var scope = ServiceScopeFactory.CreateScope();
                    await scope.ServiceProvider.GetService<IPublisher>().Publish(eventData);
                }
                catch (TargetInvocationException ex)
                {
                    exceptions.Add(ex.InnerException);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }

                return;
            }

            await base.TriggerHandlersAsync(eventType, eventData, exceptions);
        }
    }
}
