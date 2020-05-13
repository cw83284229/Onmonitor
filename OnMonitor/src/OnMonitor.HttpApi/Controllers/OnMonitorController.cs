using OnMonitor.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace OnMonitor.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class OnMonitorController : AbpController
    {
        protected OnMonitorController()
        {
            LocalizationResource = typeof(OnMonitorResource);
        }
    }
}