using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OnMonitor.Data
{
    /* This is used if database provider does't define
     * IOnMonitorDbSchemaMigrator implementation.
     */
    public class NullOnMonitorDbSchemaMigrator : IOnMonitorDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}