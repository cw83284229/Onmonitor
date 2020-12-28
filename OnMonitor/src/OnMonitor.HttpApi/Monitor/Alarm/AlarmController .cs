using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using OnMonitor.Common.Excel;
using OnMonitor.Excel;
using OnMonitor.Monitor;
using OnMonitor.Monitor.Alarm;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.Controllers
{
    [Route("api/Alarm")]
    [Authorize(Roles = "admin")]
    public class AlarmController : OnMonitorController
    {
        public IAlarmAppService _alarmAppService;
        public AlarmController(IAlarmAppService alarmAppService)
        {
            _alarmAppService = alarmAppService;
        }
        #region 批量导入DVR数据
        /// <summary>
        /// /Excel表格导入数据库
        /// </summary>
        /// <param name="files">传入文件流</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ExcelToAalarmInfo")]
        public async Task<List<AlarmDto>> PostExcelToDVRInfoAsync(IFormFile files)
        {

            if (files.Length == 0 || Path.GetExtension(files.FileName) != ".xlsx")
            {
                throw new Exception("上传文件格式错误");
                //return null;
            }

            DataTable dt = new DataTable();



            PagedAndSortedResultRequestDto resultRequestDto = new PagedAndSortedResultRequestDto() { MaxResultCount = 200000, SkipCount = 0, Sorting = "Id" };

            var data = ExcelHelper.ExcelToDataTable(files.OpenReadStream(), Path.GetExtension(files.FileName), "Sheet", true);
            var list = ListToDataTable.tolist<UpdateAlarmDto>(data);
            List<AlarmDto> listdvr = new List<AlarmDto>();
            var alarmdata =await _alarmAppService.GetListAsync(resultRequestDto);
            foreach (var item in list)
            {
                var dvr = alarmdata.Items.Where(u=>u.Alarm_ID==item.Alarm_ID);
                if (dvr.Count() == 0)
                {
                    var dvrdata = await _alarmAppService.CreateAsync(item);
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
            var data = _alarmAppService.GetListAsync(resultRequestDto);
            var list = data.Result.Items.ToList();

            DataTable dataTable = ListToDataTable.toDataTable<AlarmDto>(list);
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