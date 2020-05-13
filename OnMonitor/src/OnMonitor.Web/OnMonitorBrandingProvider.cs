using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace OnMonitor.Web
{
    [Dependency(ReplaceServices = true)]
    public class OnMonitorBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "OnMonitor";
    }
}
