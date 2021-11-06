using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace DomainCqrs
{
    [DependsOn(
        typeof(DomainCqrsDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class DomainCqrsApplicationContractsModule : AbpModule
    {

    }
}
