using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Distributed;
using OnMonitor.Common.Excel;
using OnMonitor.Excel;
using OnMonitor.Monitor;
using OnMonitor.Monitor.Alarm;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Common.Files;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;

namespace OnMonitor.Controllers
{
    [Route("api/AlarmStatus")]
   // [Authorize(Roles = "admin")]
    public class AlarmStatusController : OnMonitorController
    {
        public IAlarmStatusAppService _alarmStatusAppService;

        public Microsoft.Extensions.Caching.Distributed.IDistributedCache _cache;

        public AlarmStatusController(IAlarmStatusAppService alarmStatusAppService, Microsoft.Extensions.Caching.Distributed.IDistributedCache cache)
        {
            _alarmStatusAppService = alarmStatusAppService;
            _cache = cache;
        }
     

        #region 按条件导出全部数据
        /// <summary>
        /// 按条件导出全部数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("OutExcel")]
        public IActionResult GetOutExcel()
        {
            ConditionAlarmStatusDto conditionAlarmStatus = new ConditionAlarmStatusDto();
            PagedSortedRequestDto resultRequestDto = new PagedSortedRequestDto() { MaxResultCount = 200000, SkipCount = 0, Sorting = "Id" };
            var data = _alarmStatusAppService.GetRequstList(conditionAlarmStatus,resultRequestDto);
            var list = data.Items.ToList();

            DataTable dataTable = ListToDataTable.toDataTable<RequstAlarmStatusDto>(list);
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


        #region 按条件获取数据清单
        /// <summary>
        /// 按条件获取数量清单
        /// </summary>
        /// <param name="Factory">厂区</param>
        /// <param name="RoomType">监控室类别</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ConditionCount")]
        public Dictionary<string, int> GetConditionCount(string Factory, string RoomType, string MonitorRoom)
        {
            ConditionAlarmStatusDto conditionAlarmStatus = new ConditionAlarmStatusDto();
            conditionAlarmStatus.Factory = Factory;
            conditionAlarmStatus.RoomType = RoomType;
            conditionAlarmStatus.RoomLocation = MonitorRoom;
            PagedSortedRequestDto resultRequestDto = new PagedSortedRequestDto() { MaxResultCount = 200000, SkipCount = 0, Sorting = "Id" };
            var data = _alarmStatusAppService.GetRequstList(conditionAlarmStatus, resultRequestDto);
            Dictionary<string, int> dic = new Dictionary<string, int>();

            var alarmint = data.Items.Where(u => u.IsDefence == 1).Where(i => i.IsAlarm == 1).Where(k=>k.IsAnomaly==2).Count();//报警数量
            var defenceint = data.Items.Where(u=>u.IsDefence==1).Count();
            var notdefineint = data.Items.Where(u => u.IsDefence == 2).Count();
            var openDoorint = data.Items.Where(u => u.IsOpenDoor == true).Count();
            var closeDoorint = data.Items.Where(u => u.IsOpenDoor == false).Count();
            var  treatmentint  = data.Items.Where(u => u.TreatmentState== 1).Count();
            var  onlineint = data.Items.Where(u => u.IsAnomaly == 2).Count();

            dic.Add("门磁数量", (int)data.TotalCount);
            dic.Add("报警数据",alarmint);
            dic.Add("布防数据", defenceint);
            dic.Add("撤防数据", notdefineint);
            dic.Add("在线数据", onlineint);
            dic.Add("离线数据", data.Items.Where(u => u.IsAnomaly == 1||u.IsAnomaly==0).Count());
            dic.Add("开岗数据", openDoorint);
            dic.Add("封岗数据", closeDoorint);
            dic.Add("未处理数据", treatmentint);

            return dic;


        }
        #endregion

        #region 上传图纸文件
        /// <summary>
        /// 上传文件（已楼层命名规则"C03-3F.JPG" 传入楼栋名称“C03”）
        /// </summary>
        /// <param name="layoutfile">图纸文件</param>
        /// <param name="AlarmcBliudName">楼栋名称</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Uplocad")]
        public string UploadFiles(IFormFile layoutfile, string AlarmcBliudName)
        {
            string directorypath = AppContext.BaseDirectory + "AlarmFiles" + "\\" + AlarmcBliudName;
            try
            {
                var directory = FilesHelper.IsExistDirectory(directorypath);

                if (false == directory)
                {
                    FilesHelper.CreateDir(directorypath);
                }
                FilesHelper.CreateFile(directorypath + "\\" + layoutfile.FileName, layoutfile.GetAllBytes());
            }
            catch (Exception)
            {

                return "上传失败";
            }


            return "ok";

        }

        #endregion

        #region 获取图纸图片
        /// <summary>
        /// 获取图纸文件
        /// </summary>
        /// <param name="name">获取图纸的楼层格式“C03-3F”</param>
        /// <returns></returns>
        [HttpGet]
        [Route("DownloadByFileName")]

        public IActionResult DownloadByFileName(string name)
        {

            string directorypath = AppContext.BaseDirectory + "AlarmFiles" + "\\" + name.Substring(0, 3);

            var reuqst = FilesHelper.GetFileNames(directorypath);
            string fileName = reuqst.Where(u => u.Contains(name)).FirstOrDefault();

            // string fileName = $"{directorypath}//{name}";
            var stream = System.IO.File.OpenRead(fileName);
            string fileExt = Path.GetExtension(fileName);
            var provider = new FileExtensionContentTypeProvider();
            var meni = provider.Mappings[fileExt];
            var returnFile = File(stream, meni, Path.GetFileName(fileName));
            return returnFile;

        }
        #endregion

        #region 获取报警数据
        [HttpGet]
        [Route("InAlarm")]
        public PagedResultDto<RequstAlarmStatusDto> GetInAlarm(string Factory, string RoomType,string RoomLocation)
        {
            ConditionAlarmStatusDto conditionAlarmStatus = new ConditionAlarmStatusDto();
            conditionAlarmStatus.Factory = Factory;
            conditionAlarmStatus.RoomType = RoomType;
            conditionAlarmStatus.RoomLocation = RoomLocation;
            conditionAlarmStatus.IsAlarm = 1;
            conditionAlarmStatus.IsDefence = 1;
            conditionAlarmStatus.IsAnomaly = 2;
            
            PagedSortedRequestDto resultRequestDto = new PagedSortedRequestDto() { MaxResultCount = 200000, SkipCount = 0, Sorting = "Id" };

            var data = _alarmStatusAppService.GetRequstList(conditionAlarmStatus, resultRequestDto);

             data.Items= data.Items.OrderByDescending(u => u.LastModificationTime).ToList();//反序排列


            return data;



        }
        #endregion

        #region 获取未处理数据
        [HttpGet]
        [Route("TreatmentState")]
        public PagedResultDto<RequstAlarmStatusDto> GetTreatmentState(string Factory, string RoomType, string RoomLocation)
        {
            ConditionAlarmStatusDto conditionAlarmStatus = new ConditionAlarmStatusDto();
            conditionAlarmStatus.Factory = Factory;
            conditionAlarmStatus.RoomType = RoomType;
            conditionAlarmStatus.RoomLocation = RoomLocation;

            conditionAlarmStatus.TreatmentState = 1;
            PagedSortedRequestDto resultRequestDto = new PagedSortedRequestDto() { MaxResultCount = 200000, SkipCount = 0, Sorting = "Id" };
            var data = _alarmStatusAppService.GetRequstList(conditionAlarmStatus, resultRequestDto);
            data.Items = data.Items.OrderByDescending(u=>u.LastModificationTime).ToList();

            return data;



        }
        #endregion

        #region 测试缓存
        [HttpGet]
        [Route("testState")]
        public List<AlarmStatusDto> GettestState(PagedAndSortedResultRequestDto input,string condity)
        {
            ConditionAlarmStatusDto conditionAlarmStatus = new ConditionAlarmStatusDto();
           

            conditionAlarmStatus.TreatmentState = 1;
            PagedSortedRequestDto resultRequestDto = new PagedSortedRequestDto() { MaxResultCount = 200000, SkipCount = 0, Sorting = "Id" };
           var data = _alarmStatusAppService.GetListAsync(input).Result.Items;

           var bookstr= Newtonsoft.Json.JsonConvert.SerializeObject(data);
           
            var bt= Encoding.UTF8.GetBytes(bookstr);
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(20));
            _cache.Set("bookstr", bt, options);

            var DD=  _cache.Get(condity);
            var FF= Encoding.UTF8.GetString(DD);
            var DDF2G = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AlarmStatusDto>>(FF);
            return DDF2G;



        }

      
        #endregion
    }


}