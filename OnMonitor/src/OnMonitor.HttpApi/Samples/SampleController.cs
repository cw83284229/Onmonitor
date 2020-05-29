using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace OnMonitor.Samples
{
    [RemoteService]
    [Route("api/OnMonitor/test")]
    public class testController : OnMonitorController
    {
        private readonly ISampleAppService _sampleAppService;

        public testController(ISampleAppService sampleAppService)
        {
            _sampleAppService = sampleAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok("测试成功");
        }

        [HttpGet]
        [Route("authorized")]
        [Authorize]
        public async Task<SampleDto> GetAuthorizedAsync()
        {
            return await _sampleAppService.GetAsync();
        }
    }
}
