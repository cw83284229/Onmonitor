using System;
using System.Collections.Generic;
using System.Text;
using OnMonitor.Localization;
using Volo.Abp.Application.Services;

namespace OnMonitor
{
    /* Inherit your application services from this class.
     */
    public abstract class OnMonitorAppService : ApplicationService
    {
        protected OnMonitorAppService()
        {
            LocalizationResource = typeof(OnMonitorResource);
        }
    }
}
