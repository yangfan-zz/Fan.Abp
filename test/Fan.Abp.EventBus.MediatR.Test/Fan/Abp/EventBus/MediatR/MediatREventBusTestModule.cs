using Volo.Abp.Modularity;

namespace Fan.Abp.EventBus.MediatR
{
    [DependsOn(typeof(FanEventBusMediatRModule))]
    public class MediatREventBusTestModule : AbpModule
    {

    }
}
