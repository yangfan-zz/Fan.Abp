using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace DomainCqrs
{
    [DependsOn(
        typeof(DomainCqrsApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class DomainCqrsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "DomainCqrs";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(DomainCqrsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
