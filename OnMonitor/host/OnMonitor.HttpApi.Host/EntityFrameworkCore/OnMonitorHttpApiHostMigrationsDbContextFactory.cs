using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace OnMonitor.EntityFrameworkCore
{
    public class OnMonitorHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<OnMonitorHttpApiHostMigrationsDbContext>
    {
        public OnMonitorHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<OnMonitorHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("OnMonitor"));

            return new OnMonitorHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
