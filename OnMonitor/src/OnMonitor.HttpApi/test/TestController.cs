using System;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimilarImages;
using Utility.Common.Files;
using Utility.Common.ImageHelper;
using Volo.Abp;

namespace OnMonitor.Samples
{
    [RemoteService]
    [Route("api/OnMonitor/TEST")]
    public class TESTController : OnMonitorController
    {
        private readonly ISampleAppService _sampleAppService;

        public TESTController(ISampleAppService sampleAppService)
        {
            _sampleAppService = sampleAppService;
        }

        [HttpPost]
        [Route("Uplocad")]
        public async Task<string> Uplocad (IFormFile files)
        {

            var filepath = AppContext.BaseDirectory + "test123"+"\\" + files.FileName;

            byte[] bytedata =await files.GetAllBytesAsync();

          var dd=  FilesHelper.IsExistDirectory(AppContext.BaseDirectory + "test123");

            if (false==dd)
            {
                FilesHelper.CreateDir("test123");
            }


            FilesHelper.CreateFile(filepath, bytedata);

            return "ok";

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
