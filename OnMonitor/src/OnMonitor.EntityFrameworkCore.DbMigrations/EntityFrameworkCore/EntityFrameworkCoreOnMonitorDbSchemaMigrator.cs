using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnMonitor.Data;
using Volo.Abp.DependencyInjection;

namespace OnMonitor.EntityFrameworkCore
{
    public class EntityFrameworkCoreOnMonitorDbSchemaMigrator
        : IOnMonitorDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreOnMonitorDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the OnMonitorMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<OnMonitorMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}