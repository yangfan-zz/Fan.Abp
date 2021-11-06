using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace DomainCqrs
{
    [Dependency(ReplaceServices = true)]
    public class DomainCqrsBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "DomainCqrs";
    }
}
