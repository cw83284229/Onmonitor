using OnMonitor.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace OnMonitor.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class OnMonitorPageModel : AbpPageModel
    {
        protected OnMonitorPageModel()
        {
            LocalizationResourceType = typeof(OnMonitorResource);
            ObjectMapperContext = typeof(OnMonitorWebModule);
        }
    }
}