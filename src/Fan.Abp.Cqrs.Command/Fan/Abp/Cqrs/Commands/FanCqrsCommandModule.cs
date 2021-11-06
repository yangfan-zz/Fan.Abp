using Volo.Abp.Modularity;

namespace Fan.Abp.Cqrs.Commands
{
    [DependsOn(typeof(FanCqrsModule))]
    public class FanCqrsCommandModule : AbpModule
    {

    }
}
