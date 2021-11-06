using Fan.Abp.Ddd.Application.Queries;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Fan.Abp.Ddd.Application.QueryHandlers
{
    [DependsOn(typeof(FanDddApplicationQueryModule), typeof(AbpDddApplicationModule))]
    public class FanDddApplicationQueryHandlersModule : AbpModule
    {

    }
}
