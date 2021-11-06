using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace DomainCqrs.Blazor.Menus
{
    public class DomainCqrsMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            //Add main menu items.
            context.Menu.AddItem(new ApplicationMenuItem(DomainCqrsMenus.Prefix, displayName: "DomainCqrs", "/DomainCqrs", icon: "fa fa-globe"));
            
            return Task.CompletedTask;
        }
    }
}