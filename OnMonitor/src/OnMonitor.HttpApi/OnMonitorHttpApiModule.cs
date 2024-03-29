﻿using Localization.Resources.AbpUi;
using OnMonitor.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.SignalR;

namespace OnMonitor
{
    [DependsOn(
        typeof(OnMonitorApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAspNetCoreSignalRModule))]
    public class OnMonitorHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(OnMonitorHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<OnMonitorResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
           
        }
    }
}
