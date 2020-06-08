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
            //指定数据库及连接地址
            var builder = new DbContextOptionsBuilder<OnMonitorHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("OnMonitor"));

            return new OnMonitorHttpApiHostMigrationsDbContext(builder.Options);
        }

       //配置获取Appsettings.json
        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
