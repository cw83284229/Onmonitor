using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace OnMonitor.Samples
{
    [EnableCors("Default")]
    [RemoteService]
    [Route("api/OnMonitor/test1")]
    public class test1Controller : OnMonitorController
    {
        //private readonly ISampleAppService _sampleAppService;

        //public testController(ISampleAppService sampleAppService)
        //{
        //    _sampleAppService = sampleAppService;
        //}

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok("测试成功");
        }

        [HttpGet]
        [Route("authorized")]
        [Authorize]
        public async Task<IActionResult> GetAuthorizedAsync()
        {
            return Ok("测试成功");
        }
    }
}
