using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace OnMonitor.MongoDB
{
    [ConnectionStringName(OnMonitorDbProperties.ConnectionStringName)]
    public interface IOnMonitorMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
