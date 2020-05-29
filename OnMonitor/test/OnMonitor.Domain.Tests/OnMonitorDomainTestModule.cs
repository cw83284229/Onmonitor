using OnMonitor.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace OnMonitor
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(OnMonitorEntityFrameworkCoreTestModule)
        )]
    public class OnMonitorDomainTestModule : AbpModule
    {
        
    }
}
