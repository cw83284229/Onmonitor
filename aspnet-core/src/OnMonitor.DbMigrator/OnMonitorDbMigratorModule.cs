using OnMonitor.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace OnMonitor.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(OnMonitorEntityFrameworkCoreDbMigrationsModule),
        typeof(OnMonitorApplicationContractsModule)
        )]
    public class OnMonitorDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
