using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Fan.Abp.Ddd.Application
{
    [DependsOn(typeof(FanDddApplicationCqrsContractsModule), typeof(AbpDddApplicationModule))]
    public class FanDddApplicationCqrsModule : AbpModule
    {

    }
}
