using Volo.Abp;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Testing;

namespace Fan.Abp.EventBus.MediatR
{
    public abstract class EventBusTestBase : AbpIntegratedTest<MediatREventBusTestModule>
    {
        protected ILocalEventBus LocalEventBus;

        protected EventBusTestBase()
        {
            LocalEventBus = GetRequiredService<ILocalEventBus>();
        }

        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
