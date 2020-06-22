using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using OnMonitor.Common.Excel;
using OnMonitor.Excel;
using OnMonitor.MonitorRepair;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.Controllers
{

   // [Authorize(Roles = "admin")]
    [Route("api/CameraRepair")]
    public class CameraRepairController : OnMonitorController
    {
        public ICameraRepairAppService _cameraRepairAppService;
        public CameraRepairController(ICameraRepairAppService cameraRepairAppService)
        {
            _cameraRepairAppService = cameraRepairAppService;
        }

        #region 获取监控维修年份分析数据
        /// <summary>
        /// 获取监控镜头维修年份分析数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("VintageAnalysis")]
        public async Task<Dictionary<string, int>> GetVintageAnalysisAsync()
        {
            int time = DateTime.Now.Year;
            PagedSortedRequestDto resultRequestDto = new PagedSortedRequestDto() { MaxResultCount = 200000, SkipCount = 0, Sorting = "Id" };
            var data = _cameraRepairAppService.GetRepairsList(resultRequestDto);
            Dictionary<string, int> keyValues = new Dictionary<string, int>();
            for (int i = 0; i < 10; i++)
            {
                string year = (time - i).ToString();
                var data1 = data.Items.Where(u => u.install_time.Contains(year));

                keyValues.Add(year, data1.Count());

            }
            return keyValues;
        }
        #endregion

        #region 按条件导出全部数据
        /// <summary>
        /// 按条件导出全部数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [Route("OutExcel")]
        public IActionResult GetOutExcel(QueryCondition condition)
        {

            PagedSortedRequestDto resultRequestDto = new PagedSortedRequestDto() { MaxResultCount = 1000, SkipCount = 0};
            var data = _cameraRepairAppService.GetRepairsListByCondition(condition, resultRequestDto);
            var list = data.Items.ToList();

            DataTable dataTable = ListToDataTable.toDataTable<RequstCameraRepairDto>(list);


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

        #region 获取楼栋监控分布数据
        /// <summary>
        /// 获取楼栋监控分布数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CameraRepairByBuild")]
        public async Task<Dictionary<string, int>> GetCameraRepairByBuildAsync()
        {

            PagedSortedRequestDto resultRequestDto = new PagedSortedRequestDto() { MaxResultCount = 200000, SkipCount = 0, Sorting = null };

            Dictionary<string, int> keyValues = new Dictionary<string, int>();
            var data = _cameraRepairAppService.GetRepairsList(resultRequestDto).Items.ToList();
            var databuild = data.Select(i => new { Build = i.Build }).Distinct();

            foreach (var item in databuild)
            {
                var data2 = data.Where(u => u.Build == item.Build);

                keyValues.Add(item.Build, data2.Count());

            }
            return keyValues;//Newtonsoft.Json.JsonConvert.SerializeObject(keyValues);
        }
        #endregion
    }
}
