using Microsoft.Extensions.DependencyInjection;
using OnMonitor.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace OnMonitor.Blazor
{
    [DependsOn(
        typeof(OnMonitorHttpApiClientModule),
        typeof(AbpAutoMapperModule)
        )]
    public class OnMonitorBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<OnMonitorBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<OnMonitorBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new OnMonitorMenuContributor());
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(OnMonitorBlazorModule).Assembly);
            });
        }
    }
}