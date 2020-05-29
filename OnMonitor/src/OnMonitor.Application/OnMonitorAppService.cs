using OnMonitor.Localization;
using Volo.Abp.Application.Services;

namespace OnMonitor
{
    public abstract class OnMonitorAppService : ApplicationService
    {
        protected OnMonitorAppService()
        {
            LocalizationResource = typeof(OnMonitorResource);
            ObjectMapperContext = typeof(OnMonitorApplicationModule);
        }
    }
}
