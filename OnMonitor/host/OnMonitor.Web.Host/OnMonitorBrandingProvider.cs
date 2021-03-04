using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace OnMonitor
{
    [Dependency(ReplaceServices = true)]
    public class OnMonitorBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "OnMonitor";
    }
}
