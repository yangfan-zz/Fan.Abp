using DomainCqrs.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DomainCqrs
{
    public abstract class DomainCqrsController : AbpController
    {
        protected DomainCqrsController()
        {
            LocalizationResource = typeof(DomainCqrsResource);
        }
    }
}
