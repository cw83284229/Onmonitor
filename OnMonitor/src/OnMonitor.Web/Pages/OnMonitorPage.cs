using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using OnMonitor.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace OnMonitor.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits OnMonitor.Web.Pages.OnMonitorPage
     */
    public abstract class OnMonitorPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<OnMonitorResource> L { get; set; }
    }
}
