using Volo.Abp.Modularity;

namespace OnMonitor
{
    [DependsOn(
        typeof(OnMonitorApplicationModule),
        typeof(OnMonitorDomainTestModule)
        )]
    public class OnMonitorApplicationTestModule : AbpModule
    {

    }
}