using DomainCqrs.Localization;
using Volo.Abp.AspNetCore.Components;

namespace DomainCqrs.Blazor.Server.Host
{
    public abstract class DomainCqrsComponentBase : AbpComponentBase
    {
        protected DomainCqrsComponentBase()
        {
            LocalizationResource = typeof(DomainCqrsResource);
        }
    }
}
