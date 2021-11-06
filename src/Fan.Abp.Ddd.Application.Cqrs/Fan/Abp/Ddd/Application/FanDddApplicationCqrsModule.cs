using Fan.Abp.Ddd.Application.CommandHandlers;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Fan.Abp.Ddd.Application
{
    [DependsOn(typeof(FanDddApplicationCqrsContractsModule), typeof(FanDddApplicationCommandHandlerModule),
        typeof(AbpDddApplicationModule))]
    public class FanDddApplicationCqrsModule : AbpModule
    {

    }
}
