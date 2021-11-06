using Volo.Abp.Bundling;

namespace DomainCqrs.Blazor.Host
{
    public class DomainCqrsBlazorHostBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {

        }

        public void AddStyles(BundleContext context)
        {
            context.Add("main.css", true);
        }
    }
}
