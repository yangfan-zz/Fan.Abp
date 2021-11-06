using Fan.Abp.Cqrs;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Fan.Abp.Ddd.Application.Queries
{
    [DependsOn(typeof(FanCqrsModule), typeof(AbpDddApplicationContractsModule))]
    public class FanDddApplicationQueryModule : AbpModule
    {

    }
}
