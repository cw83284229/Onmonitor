using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using OnMonitor.Localization;
using OnMonitor.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using OnMonitor.Permissions;

namespace OnMonitor.Web
{
    [DependsOn(
        typeof(OnMonitorHttpApiModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpAutoMapperModule)
        )]
    public class OnMonitorWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(OnMonitorResource), typeof(OnMonitorWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(OnMonitorWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new OnMonitorMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<OnMonitorWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<OnMonitorWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<OnMonitorWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                //Configure authorization.
            });
        }
    }
}
