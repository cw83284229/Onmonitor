using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.BlobStoring;

namespace OnMonitor
{
    [DependsOn(
        typeof(OnMonitorDomainModule),
        typeof(OnMonitorApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpBlobStoringFileSystemModule),
        typeof(AbpBlobStoringModule)
        )]
    public class OnMonitorApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<OnMonitorApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<OnMonitorApplicationModule>(validate: true);
                options.AddProfile<OnMonitorApplicationAutoMapperProfile>(validate:true);
            });

            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(container =>
                {
                    container.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = "D:\\BOLB";
                    });
                });
            });

          
        }
    }
}
