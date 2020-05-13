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

    [Authorize(Roles = "admin")]
    [Route("api/Camera")]
    public class CameraController : OnMonitorController
    {
        public ICameraAppService _cameraAppService;
        public CameraController(ICameraAppService cameraAppService)
        {
            _cameraAppService = cameraAppService;
        }
        #region Excel数据导入
        /// <summary>
        /// Excel数据库导入
        /// </summary>
        /// <param name="files">传入文件流</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ExcelToCameraInfo")]
        public async Task<List<CameraDto>> PostExcelToCameraInfoAsync(IFormFile files)
        {

            if (files.Length == 0)
            {
                return null;
            }

            DataTable dt = new DataTable();


            var data = ExcelHelper.ExcelToDataTable(files.OpenReadStream(), Path.GetExtension(files.FileName), "Sheet", true);

            var list = ListToDataTable.tolist<UpdateCameraDto>(data);
            List<CameraDto> listcamera = new List<CameraDto>();
            foreach (var item in list)
            {
              
                var camera = await _cameraAppService.GetListByCondition(new CameraCondition() {Camera_ID= item.Camera_ID },null);



                if (camera.TotalCount == 0)
                {
                    var Cameradata = await _cameraAppService.CreateAsync(item);
                    listcamera.Add(Cameradata);
                }

            }


            return listcamera;


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
        public IActionResult GetOutExcel(CameraCondition condition)
        {

            PagedAndSortedResultRequestDto resultRequestDto = new PagedAndSortedResultRequestDto() { MaxResultCount = 200000, SkipCount = 0};
            var data = _cameraAppService.GetListByCondition(condition, resultRequestDto);
            var list = data.Result.Items.ToList();

            DataTable dataTable = ListToDataTable.toDataTable<CameraDto>(list);
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

        #region 获取镜头年份分析
        /// <summary>
        /// 获取监控镜头年份分析数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("VintageAnalysis")]
        public async Task<Dictionary<string, int>> GetVintageAnalysisAsync()
        {
            int time = DateTime.Now.Year;
            PagedAndSortedResultRequestDto resultRequestDto = new PagedAndSortedResultRequestDto() { MaxResultCount = 20000, SkipCount = 0, Sorting =null };
          
            Dictionary<string, int> keyValues = new Dictionary<string, int>();
            var data =_cameraAppService.GetListAsync(resultRequestDto).Result.Items.ToList();
            for (int i = 0; i < 10; i++)
            {
                string year = (time - i).ToString();
                CameraCondition cameraCondition = new CameraCondition() { install_time = year };
               
                var data2 = data.Where(u=>u.install_time.Contains(year));
               
                keyValues.Add(year, data2.Count());
                
            }
            return keyValues;//Newtonsoft.Json.JsonConvert.SerializeObject(keyValues);
        }
        #endregion

        #region 获取楼栋监控分布数据
        /// <summary>
        /// 获取楼栋监控分布数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CameraByBuild")]
        public async Task<Dictionary<string, int>> GetCameraByBuildAsync()
        {
           
            PagedAndSortedResultRequestDto resultRequestDto = new PagedAndSortedResultRequestDto() { MaxResultCount = 20000, SkipCount = 0, Sorting = null };

            Dictionary<string, int> keyValues = new Dictionary<string, int>();
            var data = _cameraAppService.GetListAsync(resultRequestDto).Result.Items.ToList();
            var databuild=data.Select(i => new { Build = i.Build}).Distinct();

            foreach (var item in databuild)
            {
                var data2 = data.Where(u => u.Build==item.Build);

                keyValues.Add(item.Build, data2.Count());

            }
            return keyValues;//Newtonsoft.Json.JsonConvert.SerializeObject(keyValues);
        }
        #endregion




    }
}
