using Fan.Abp.Cqrs.Commands;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Fan.Abp.Ddd.Application.CommandHandlers
{
    [DependsOn(typeof(FanCqrsCommandModule), typeof(AbpDddApplicationModule))]
    public class FanDddApplicationCommandHandlerModule : AbpModule
    {

    }
}
