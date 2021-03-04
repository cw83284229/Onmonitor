using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace OnMonitor
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(OnMonitorDomainSharedModule)
    )]
    public class OnMonitorDomainModule : AbpModule
    {

    }
}
