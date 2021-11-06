using Volo.Abp.Modularity;

namespace DomainCqrs
{
    [DependsOn(
        typeof(DomainCqrsApplicationModule),
        typeof(DomainCqrsDomainTestModule)
        )]
    public class DomainCqrsApplicationTestModule : AbpModule
    {

    }
}
