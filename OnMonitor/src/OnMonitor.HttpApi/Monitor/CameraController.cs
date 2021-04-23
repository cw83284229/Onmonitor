using Common;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using OnMonitor.Common.Excel;
using OnMonitor.Excel;
using OnMonitor.Models;
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

    // [Authorize(Roles = "admin")]
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
        /// 获取导入模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GenerateCameraImportTemplate")]
        public IActionResult GetGenerateCameraImportTemplate()
        {
            IImporter Importer = new ExcelImporter();
            var pathname = $"{System.AppDomain.CurrentDomain.BaseDirectory}Basics\\InExcel.xlsx";

            var requst = Importer.GenerateTemplate<ImportCameraDto>(pathname);

            var stream = System.IO.File.OpenRead(pathname);
            string fileExt = Path.GetExtension(pathname);
            var provider = new FileExtensionContentTypeProvider();
            var meni = provider.Mappings[fileExt];
            var returnFile = File(stream, meni, Path.GetFileName(pathname));
            return returnFile;


        }



        /// <summary>
        /// Excel数据库导入(单次按监控室导入)
        /// </summary>
        /// <param name="files">传入文件流</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ExcelToCameraInfo")]
        public async Task<PagedResultDto<CameraDto>> PostExcelToCameraInfoAsync(IFormFile files)
        {

            if (files.Length == 0)
            {
                return null;
            }
            IImporter Importer = new ExcelImporter();

            var import9 = await Importer.Import<UpdateCameraDto>(files.OpenReadStream());

            var data = import9.Data.ToList();
            // var data = ExcelHelper.ExcelToDataTable(files.OpenReadStream(), Path.GetExtension(files.FileName), "Sheet", true);
           // PagedSortedRequestDto resultRequestDto = new PagedSortedRequestDto() { MaxResultCount = 200000, SkipCount = 0, Sorting = "Id" };
            var cameraData =await _cameraAppService.GetListAllAsync();
            var Monitoring_room = data.FirstOrDefault().Monitoring_room;
            var datade = cameraData.Where(u => u.Monitoring_room == Monitoring_room);

            foreach (var item in datade)
            {
               await _cameraAppService.DeleteAsync(item.Id);
            }

            var requst= await _cameraAppService.PostInsertList(data);


            return requst;


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
  

            PagedSortedRequestDto resultRequestDto = new PagedSortedRequestDto() { MaxResultCount = 200000, SkipCount = 0};
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
