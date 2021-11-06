using DomainCqrs.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace DomainCqrs.Pages
{
    public abstract class DomainCqrsPageModel : AbpPageModel
    {
        protected DomainCqrsPageModel()
        {
            LocalizationResourceType = typeof(DomainCqrsResource);
        }
    }
}