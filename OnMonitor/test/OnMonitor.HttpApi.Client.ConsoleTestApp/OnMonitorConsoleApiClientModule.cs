using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace OnMonitor
{
    [DependsOn(
        typeof(OnMonitorHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class OnMonitorConsoleApiClientModule : AbpModule
    {
        
    }
}
