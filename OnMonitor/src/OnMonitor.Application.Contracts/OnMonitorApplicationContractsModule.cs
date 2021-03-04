using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Volo.Abp.VirtualFileSystem;

namespace OnMonitor
{
    [DependsOn(
        typeof(OnMonitorDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class OnMonitorApplicationContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<OnMonitorApplicationContractsModule>("OnMonitor");
            });
        }
    }
}
