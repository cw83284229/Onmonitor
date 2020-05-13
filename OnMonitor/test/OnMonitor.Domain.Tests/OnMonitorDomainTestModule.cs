using OnMonitor.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace OnMonitor
{
    [DependsOn(
        typeof(OnMonitorEntityFrameworkCoreTestModule)
        )]
    public class OnMonitorDomainTestModule : AbpModule
    {

    }
}