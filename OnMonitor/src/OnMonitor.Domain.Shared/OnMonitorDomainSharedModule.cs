using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using OnMonitor.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace OnMonitor
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class OnMonitorDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<OnMonitorDomainSharedModule>("OnMonitor");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<OnMonitorResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/OnMonitor");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("OnMonitor", typeof(OnMonitorResource));
            });
        }
    }
}
