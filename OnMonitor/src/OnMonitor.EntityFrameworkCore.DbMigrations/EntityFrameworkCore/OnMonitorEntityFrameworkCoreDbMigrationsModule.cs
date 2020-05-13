using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace OnMonitor.EntityFrameworkCore
{
    [DependsOn(
        typeof(OnMonitorEntityFrameworkCoreModule)
        )]
    public class OnMonitorEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<OnMonitorMigrationsDbContext>();
        }
    }
}
