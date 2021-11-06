using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace DomainCqrs.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(DomainCqrsBlazorModule)
        )]
    public class DomainCqrsBlazorServerModule : AbpModule
    {
        
    }
}