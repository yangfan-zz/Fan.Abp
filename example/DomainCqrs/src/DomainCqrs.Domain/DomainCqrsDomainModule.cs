using Fan.Abp.Cqrs.Commands;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace DomainCqrs
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(DomainCqrsDomainSharedModule),
        typeof(FanCqrsCommandModule)
    )]
    public class DomainCqrsDomainModule : AbpModule
    {

    }
}
