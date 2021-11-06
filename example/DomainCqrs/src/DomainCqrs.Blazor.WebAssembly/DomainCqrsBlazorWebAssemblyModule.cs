using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace DomainCqrs.Blazor.WebAssembly
{
    [DependsOn(
        typeof(DomainCqrsBlazorModule),
        typeof(DomainCqrsHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class DomainCqrsBlazorWebAssemblyModule : AbpModule
    {
        
    }
}