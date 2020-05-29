using OnMonitor.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace OnMonitor
{
    public abstract class OnMonitorController : AbpController
    {
        protected OnMonitorController()
        {
            LocalizationResource = typeof(OnMonitorResource);
        }
    }
}
