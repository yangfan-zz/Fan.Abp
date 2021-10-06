using Fan.Abp.Cqrs;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Fan.Abp.Ddd.Application
{
    [DependsOn(typeof(FanCqrsModule), typeof(AbpDddApplicationContractsModule))]
    public class FanDddApplicationCqrsContractsModule : AbpModule
    {

    }
}
