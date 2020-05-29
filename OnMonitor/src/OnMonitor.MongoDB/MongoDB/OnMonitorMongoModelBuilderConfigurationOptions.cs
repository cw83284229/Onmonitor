using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace OnMonitor.MongoDB
{
    public class OnMonitorMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public OnMonitorMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}