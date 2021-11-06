using Localization.Resources.AbpUi;
using DomainCqrs.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace DomainCqrs
{
    [DependsOn(
        typeof(DomainCqrsApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class DomainCqrsHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DomainCqrsHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<DomainCqrsResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
