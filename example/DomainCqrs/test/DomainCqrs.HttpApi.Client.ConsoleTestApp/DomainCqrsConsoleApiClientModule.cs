using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace DomainCqrs
{
    [DependsOn(
        typeof(DomainCqrsHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class DomainCqrsConsoleApiClientModule : AbpModule
    {
        
    }
}
