using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace OnMonitor.MongoDB
{
    [ConnectionStringName(OnMonitorDbProperties.ConnectionStringName)]
    public class OnMonitorMongoDbContext : AbpMongoDbContext, IOnMonitorMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureOnMonitor();
        }
    }
}