using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace OnMonitor.MongoDB
{
    public static class OnMonitorMongoDbContextExtensions
    {
        public static void ConfigureOnMonitor(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new OnMonitorMongoModelBuilderConfigurationOptions(
                OnMonitorDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}