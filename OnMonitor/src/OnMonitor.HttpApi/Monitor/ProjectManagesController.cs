using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using OnMonitor.Common.Excel;
using OnMonitor.Excel;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utility.Common.Files;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.Controllers
{
    [Route("api/ProjectManages")]
   // [Authorize(Roles = "admin")]
    public class ProjectManagesController : OnMonitorController
    {
        public IProjectManagesAppService _projectManagesAppService;

        public ICameraAppService _cameraAppService;

       // public IBOLBService _bOLBService;
        public ProjectManagesController(IProjectManagesAppService projectManagesAppService, ICameraAppService cameraAppService)
        {
            _cameraAppService = cameraAppService;
            _projectManagesAppService = projectManagesAppService;
          //  _bOLBService = bOLBService;
        }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="layoutfile">图纸文件</param>
    /// <param name="cameraDatafile">点位数据</param>
    /// <param name="projectName">工程名称</param>
    /// <returns></returns>
        [HttpPost]
        [Route("Uplocad")]
        public async Task<string> UploadFiles(IFormFile layoutfile,IFormFile cameraDatafile,string projectName )
        {
            string directorypath = AppContext.BaseDirectory + "ProjectFiles" + "\\" + projectName;
            try
            {
                var directory = FilesHelper.IsExistDirectory(directorypath);

                if (false == directory)
                {
                    FilesHelper.CreateDir(directorypath);
                }
                FilesHelper.CreateFile(directorypath +"\\"+ layoutfile.FileName,  layoutfile.GetAllBytes());
                FilesHelper.CreateFile(directorypath +"\\"+ cameraDatafile.FileName, cameraDatafile.GetAllBytes());
            }
            catch (Exception)
            {

               return "上传失败";
            }
           

            return "ok";

        }



        //[HttpPost]
        //[Route("Uplocad2")]
        //public async Task<string> UploadFiles2(IFormFile layoutfile, IFormFile cameraDatafile, string projectName)
        //{
            
        //    try
        //    {
        //        await _bOLBService.SaveBytesAsync(layoutfile.FileName, layoutfile.GetAllBytes());
        //        await _bOLBService.SaveBytesAsync(cameraDatafile.FileName, cameraDatafile.GetAllBytes());
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

           


        //    return "ok";

        //}



        /// <summary>
        /// 新增工程
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateProjectManages")]
        public async Task<ResponseResultDto> CreateProjectManages(UpdateProjectManagesDto input)
        {
            try
            {
              //  var data = Newtonsoft.Json.JsonConvert.SerializeObject(input.Camera_ID);
                string data2 = ChineseConverter.Convert(input.Camera_ID, ChineseConversionDirection.SimplifiedToTraditional);

                List<UpdateCameraDto> input2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UpdateCameraDto>>(data2);
                var camerarequst =await _cameraAppService.PostInsertList(input2);
            }
            catch (Exception)
            {

                return new ResponseResultDto() { 
                
                Messages="添加摄像机数据失败"};
            }

            var requst = await _projectManagesAppService.CreateAsync(input);

            return new ResponseResultDto() { ResultData=requst};
        }

        /// <summary>
        /// 解析Excel，输出UpdateCameradto集合
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ExcelToUpdateCameraDto")]
        public async Task<List<UpdateCameraDto>> ExcelToUpdateCameraDtoAsync(IFormFile files)
        {

            if (files.Length == 0)
            {
                return null;
            }
            try
            {
                IImporter Importer = new ExcelImporter();
                var import9 = await Importer.Import<UpdateCameraDto>(files.OpenReadStream());
                var data = import9.Data.ToList();
                return data;
            }
            catch (Exception)
            {

               return null;
            }
           
           

        }
        /// <summary>
        /// 获取指定文件夹名称
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProjectFiles")]
        public string[] GetProjectFiles(string projectName)
        {

            string directorypath = AppContext.BaseDirectory + "ProjectFiles" + "\\" + projectName;


            var reuqst=  FilesHelper.GetFileNames(directorypath);

            return reuqst;
        }

        /// <summary>
        /// 传入文件绝对路径下载(GetProjectFiles返回值)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
       [HttpGet]
       [Route("DownloadByFileName")]

        public IActionResult DownloadByFileName(string fileName)
        {
            var stream = System.IO.File.OpenRead(fileName);
            string fileExt = Path.GetExtension(fileName);
            var provider = new FileExtensionContentTypeProvider();
            var meni = provider.Mappings[fileExt];
            var returnFile = File(stream, meni, Path.GetFileName(fileName));
            return returnFile;

        }

    }
}