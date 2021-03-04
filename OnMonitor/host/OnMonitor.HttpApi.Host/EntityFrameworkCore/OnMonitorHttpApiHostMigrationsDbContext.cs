using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMonitor.EntityFrameworkCore
{
    public class OnMonitorHttpApiHostMigrationsDbContext : AbpDbContext<OnMonitorHttpApiHostMigrationsDbContext>
    {
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
