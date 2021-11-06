using Fan.Abp.Cqrs.Commands;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Fan.Abp.Ddd.Application
{
    [DependsOn(typeof(FanCqrsCommandModule), typeof(AbpDddApplicationContractsModule))]
    public class FanDddApplicationCqrsContractsModule : AbpModule
    {

    }
}
