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
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                 //Autofac反射设定
                options.AddDefaultRepositories(includeAllEntities: true);
            });
        }
    }
}