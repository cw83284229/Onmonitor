using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace Quartz
{
    [DependsOn(
     //...other dependencies
     typeof(AbpBackgroundJobsQuartzModule) //Add the new module dependency
     )]
    public class QuartzApplicationModule : AbpModule
    {
    }

}
