using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace OnMonitor.Blazor.Menus
{
    public class OnMonitorMenuContributor : IMenuContributor
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
            context.Menu.AddItem(new ApplicationMenuItem(OnMonitorMenus.Prefix, displayName: "OnMonitor", "~/OnMonitor", icon: "fa fa-globe"));
            
            return Task.CompletedTask;
        }
    }
}