using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace OnMonitor.EntityFrameworkCore
{
    public class OnMonitorHttpApiHostMigrationsDbContext : AbpDbContext<OnMonitorHttpApiHostMigrationsDbContext>
    {

        [ConnectionStringName(OnMonitorDbProperties.ConnectionStringName)]
        public OnMonitorHttpApiHostMigrationsDbContext(DbContextOptions<OnMonitorHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureOnMonitor();
        }
    }
}
