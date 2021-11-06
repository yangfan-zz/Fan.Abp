using DomainCqrs.Localization;
using Volo.Abp.Application.Services;

namespace DomainCqrs
{
    public abstract class DomainCqrsAppService : ApplicationService
    {
        protected DomainCqrsAppService()
        {
            LocalizationResource = typeof(DomainCqrsResource);
            ObjectMapperContext = typeof(DomainCqrsApplicationModule);
        }
    }
}
