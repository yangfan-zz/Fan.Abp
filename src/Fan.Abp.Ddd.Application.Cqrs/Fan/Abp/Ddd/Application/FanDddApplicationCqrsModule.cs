using Fan.Abp.Ddd.Application.CommandHandlers;
using Fan.Abp.Ddd.Application.QueryHandlers;
using Volo.Abp.Modularity;

namespace Fan.Abp.Ddd.Application
{
    [DependsOn(typeof(FanDddApplicationCqrsContractsModule), typeof(FanDddApplicationCommandHandlerModule),
        typeof(FanDddApplicationQueryHandlersModule))]
    public class FanDddApplicationCqrsModule : AbpModule
    {

    }
}
