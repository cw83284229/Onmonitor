using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace OnMonitor.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(OnMonitorHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class OnMonitorConsoleApiClientModule : AbpModule
    {
        
    }
}
