using Fan.Abp.MediatR;
using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace Fan.Abp.EventBus
{
    [DependsOn(
        typeof(FanMediatRModule), typeof(AbpEventBusModule))]
    public class FanEventBusMediatRModule : AbpModule
    {

    }
}
