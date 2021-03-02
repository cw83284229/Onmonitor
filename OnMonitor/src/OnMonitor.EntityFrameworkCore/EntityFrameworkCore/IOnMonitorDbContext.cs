using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace OnMonitor.EntityFrameworkCore
{
    [ConnectionStringName(OnMonitorDbProperties.ConnectionStringName)]
    public interface IOnMonitorDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}