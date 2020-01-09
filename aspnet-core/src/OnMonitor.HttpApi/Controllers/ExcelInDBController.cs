using cctvsystem.Common.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.Common;
using OnMonitor.Common.Excel;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace OnMonitor.Controllers
{
    [Route("api/ExcelInDB")]
    public class ExcelInDBController : OnMonitorController
    {

        public ICameraAppService _cameraAppService;
        public IDVRAppService _dVRAppService;
      


        public ExcelInDBController(ICameraAppService cameraAppService, IDVRAppService dVRAppService)
        {
            _cameraAppService = cameraAppService;
            _dVRAppService = dVRAppService;
           
        }

        //[HttpGet]
        //public async Task<List<DVRDto>> GetDVRInfoAsync(IFormFile files)
        //{

           


        //}
    }
}
