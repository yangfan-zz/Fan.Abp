using DomainCqrs.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace DomainCqrs.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class DomainCqrsPageModel : AbpPageModel
    {
        protected DomainCqrsPageModel()
        {
            LocalizationResourceType = typeof(DomainCqrsResource);
            ObjectMapperContext = typeof(DomainCqrsWebModule);
        }
    }
}