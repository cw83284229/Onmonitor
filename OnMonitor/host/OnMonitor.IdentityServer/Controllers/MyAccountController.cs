using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace OnMonitor.Controllers
{

    [Route("MyAccount")]
    public class MyAccountController : AbpController
    {
       
        public string Gettest()
        {
            return "测试文件";
        }
    }
}
