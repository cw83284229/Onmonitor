using cctvsystem.Common.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<List<DVRDto>> PostDVRInfoAsync(IFormFile files)
        {

            if (files.Length==0)
            {
                return null;
            }

            DataTable dt = new DataTable();
         

         var data=   ExcelHelper.ExcelToDataTable(files.OpenReadStream(), Path.GetExtension(files.FileName), "Sheet", true);

           var list = ListToDataTable.tolist<UpdateDVRDto>(data);
            List<DVRDto> listdvr = new List<DVRDto>();
            foreach (var item in list)
            {
              var dvrdata= await _dVRAppService.CreateAsync(item);
                listdvr.Add(dvrdata);
            }


            return listdvr;


        }
    }
}
