using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace OnMonitor
{
    [DependsOn(
        typeof(OnMonitorApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class OnMonitorHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "OnMonitor";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(OnMonitorApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
