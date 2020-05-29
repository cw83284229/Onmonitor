using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace OnMonitor.EntityFrameworkCore
{
    public class OnMonitorModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public OnMonitorModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}