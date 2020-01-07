using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnMonitor.Data;
using Volo.Abp.DependencyInjection;

namespace OnMonitor.EntityFrameworkCore
{
    [Dependency(ReplaceServices = true)]
    public class EntityFrameworkCoreOnMonitorDbSchemaMigrator 
        : IOnMonitorDbSchemaMigrator, ITransientDependency
    {
        private readonly OnMonitorMigrationsDbContext _dbContext;

        public EntityFrameworkCoreOnMonitorDbSchemaMigrator(OnMonitorMigrationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task MigrateAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}