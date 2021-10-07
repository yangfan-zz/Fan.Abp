using Fan.Abp.DependencyInjection;
using Fan.Abp.MediatR;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Fan.Abp.Cqrs
{
    [DependsOn(typeof(FanCqrsModule), typeof(FanMediatRModule))]
    public class FanCqrsMediatRModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddConventionalRegistrar(new MediatRCqrsConventionalRegistrar());
        }
    }
}
