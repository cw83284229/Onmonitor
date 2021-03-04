using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace OnMonitor.EntityFrameworkCore
{
    [DependsOn(
        typeof(OnMonitorDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class OnMonitorEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<OnMonitorDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);//自定义默认仓储
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}