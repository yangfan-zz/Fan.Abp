using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace DomainCqrs.Blazor.Server.Host
{
    [Dependency(ReplaceServices = true)]
    public class DomainCqrsBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "DomainCqrs";
    }
}
