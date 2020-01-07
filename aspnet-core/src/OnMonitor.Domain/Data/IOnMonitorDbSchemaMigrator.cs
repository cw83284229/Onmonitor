using System.Threading.Tasks;

namespace OnMonitor.Data
{
    public interface IOnMonitorDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
