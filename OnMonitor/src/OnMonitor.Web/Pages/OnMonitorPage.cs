using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using OnMonitor.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace OnMonitor.Web.Pages
{
    public abstract class OnMonitorPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<OnMonitorResource> L { get; set; }
    }
}
