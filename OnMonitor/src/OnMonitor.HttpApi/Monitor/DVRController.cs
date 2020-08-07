using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using OnMonitor.Common.Excel;
using OnMonitor.Excel;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.Controllers
{
    [Route("api/DVR")]
    [Authorize(Roles = "admin")]
    public class DVRController : OnMonitorController
    {
        public IDVRAppService _dVRAppService;
        public DVRController(IDVRAppService dVRAppService)
        {
            _dVRAppService = dVRAppService;
        }
        #region 批量导入数据
        /// <summary>
        /// /Excel表格导入数据库
        /// </summary>
        /// <param name="files">传入文件流</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ExcelToAlarmInfo")]
        public async Task<List<DVRDto>> PostExcelToDVRInfoAsync(IFormFile files)
        {

            if (files.Length == 0 || Path.GetExtension(files.FileName) != ".xlsx")
            {
                throw new Exception("上传文件格式错误");
                //return null;
            }

            DataTable dt = new DataTable();



            //  var dvr25 = await _dVRAppService.GetListAsync();

            var data = ExcelHelper.ExcelToDataTable(files.OpenReadStream(), Path.GetExtension(files.FileName), "Sheet", true);
            var list = ListToDataTable.tolist<UpdateDVRDto>(data);
            List<DVRDto> listdvr = new List<DVRDto>();

            foreach (var item in list)
            {
                var dvr = await _dVRAppService.GetListByCondition(null, null, null, item.DVR_ID);
                if (dvr.TotalCount == 0)
                {
                    var dvrdata = await _dVRAppService.CreateAsync(item);
                    listdvr.Add(dvrdata);
                }

            }
            return listdvr;

        } 
        #endregion

        #region 按条件导出全部数据
        /// <summary>
        /// 按条件导出全部数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("OutExcel")]
        public IActionResult GetOutExcel()
        {

            PagedAndSortedResultRequestDto resultRequestDto = new PagedAndSortedResultRequestDto() { MaxResultCount = 200000, SkipCount = 0, Sorting = "Id" };
            var data = _dVRAppService.GetListAsync(resultRequestDto);
            var list = data.Result.Items.ToList();

            DataTable dataTable = ListToDataTable.toDataTable<DVRDto>(list);
            var pathname = $"{System.AppDomain.CurrentDomain.BaseDirectory}Basics\\OutExcel.xlsx";
            var requst = ExcelHelper.DataTableToExcel(dataTable, pathname, "Sheet1", true);
            var stream = System.IO.File.OpenRead(pathname);
            string fileExt = Path.GetExtension(pathname);
            var provider = new FileExtensionContentTypeProvider();
            var meni = provider.Mappings[fileExt];
            var returnFile = File(stream, meni, Path.GetFileName(pathname));
            return returnFile;
        }
        #endregion
    }
}