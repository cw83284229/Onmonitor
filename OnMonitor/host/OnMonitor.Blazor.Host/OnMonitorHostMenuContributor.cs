using System.Threading.Tasks;
using OnMonitor.Localization;
using Volo.Abp.UI.Navigation;

namespace OnMonitor.Blazor.Host
{
    public class OnMonitorHostMenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if(context.Menu.DisplayName != StandardMenus.Main)
            {
                return Task.CompletedTask;
            }

            var l = context.GetLocalizer<OnMonitorResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    "OnMonitor.Home",
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home"
                )
            );

            return Task.CompletedTask;
        }
    }
}
