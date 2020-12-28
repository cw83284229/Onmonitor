using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using OnMonitor.Monitor;
using OnMonitor.Monitor.Alarm;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.Controllers
{

    // [Authorize(Roles = "admin")]
    [Route("api/Alarm")]
    public class AlarmInfoController : OnMonitorController
    {
        public IAlarmHostAppService _alarmHostAppService;
        public IAlarmAppService _alarmAppService;
        public IAlarmManageStateAppService _alarmManageStateAppService;
        public IAlarmStatusAppService _alarmStatusAppService;
        static public HttpClient _httpClient;
        public IConfiguration _configuration;
        string dvrurl;
        public AlarmInfoController(IAlarmHostAppService alarmHostAppService, IAlarmAppService alarmAppService, IAlarmManageStateAppService alarmManageStateAppService, IAlarmStatusAppService alarmStatusAppService, IConfiguration configuration)
        {
            _alarmHostAppService = alarmHostAppService;
            _alarmAppService = alarmAppService;
            _alarmManageStateAppService = alarmManageStateAppService;
            _alarmStatusAppService = alarmStatusAppService;
            _configuration = configuration;

            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
            dvrurl = _configuration.GetSection("DVRInfourl:url").Value;
        }

        //  #region DVR通道操作

        /// <summary>
        /// 获取报警状态
        /// </summary>
        /// <returns></returns>
        //  [Authorize(Roles = "videoCheck")]
        [HttpGet]
        [Route("GetAlarmInfoStatus")]
        public PagedResultDto<AlarmStatusDto> GetAlarmInfoStatus(PagedAndSortedResultRequestDto input01)
        {
            PagedAndSortedResultRequestDto input = new PagedAndSortedResultRequestDto() { MaxResultCount = 1000, SkipCount = 0, Sorting = "id" };
            var AlarmData = _alarmHostAppService.GetListAsync(input).Result.Items;
            var AlarmStatusData = _alarmStatusAppService.GetListAsync(input).Result.Items;
            List<AlarmStatusDto> listalarmStatus = new List<AlarmStatusDto>();
            foreach (var item in AlarmData)
            {
                string url = $"{dvrurl}/api/AlarmInfo/GetAlarmInfo?DVR_IP={item.AlarmHostIP}&DVR_Name={item.User}&DVR_PassWord={item.Password}";

                
                var handler = new HttpClientHandler();
                var response = _httpClient.GetAsync(url).Result;
                var refStatus= response.Content.ReadAsStringAsync().Result;
                var refStatusData= Newtonsoft.Json.JsonConvert.DeserializeObject<List<AlarmStatusDto>>(refStatus);
                var HostAlarmStatusData = AlarmStatusData.Where(u => u.AlarmHostIP == item.AlarmHostIP);

                for (int i = 0; i < item.AlarmChannelCount; i++)
                {
                    AlarmStatusDto refalarmStatusDto = refStatusData.Where(u => u.Channel_ID == i).FirstOrDefault();

                        var alarmdata3 = HostAlarmStatusData.Where(u => u.Channel_ID == i).FirstOrDefault();
                        AlarmStatusDto alarmStatusDto = new AlarmStatusDto();
                        alarmStatusDto.Channel_ID = i;
                        alarmStatusDto.Alarm_ID = item.AlarmHostIP;
                        alarmStatusDto.Alarm_ID = refalarmStatusDto.Alarm_ID;
                        alarmStatusDto.IsAlarm = refalarmStatusDto.IsAlarm;
                        alarmStatusDto.IsAnomaly = refalarmStatusDto.IsAnomaly;
                        alarmStatusDto.IsDefence = refalarmStatusDto.IsDefence;
                        alarmStatusDto.LastModificationTime = refalarmStatusDto.LastModificationTime;
                    if (alarmdata3!=null)
                    {
                        alarmStatusDto.IsOpenDoor = alarmdata3.IsOpenDoor;
                    }
                       
                    listalarmStatus.Add(alarmStatusDto);
                    
                    
                   
                }
               
            }

           // var listrequst = listalarmStatus.PageBy(input01.SkipCount, input01.MaxResultCount).ToList();


            return new PagedResultDto<AlarmStatusDto>() { Items=listalarmStatus,TotalCount=listalarmStatus.Count};

        }

        /// <summary>
        /// 设置布防状态 操作状态 1表示布防，2表示撤防 -1表示失败
        /// </summary>
        /// <param name="Alarm_ID"></param>
        /// <param name="DefenceState"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetDefenceArmMode")]
        public async Task<string> SetDefenceArmModeAsync(string Alarm_ID, int? DefenceState)
        {
            PagedAndSortedResultRequestDto input = new PagedAndSortedResultRequestDto() { MaxResultCount = 1000, SkipCount = 0, Sorting = "id" };
            var alarmhostDto=  _alarmAppService.GetAlarmHostDto(Alarm_ID);
            var alarmdata = await _alarmAppService.GetListAsync(input);
           // var alarmManage =await _alarmManageStateAppService.GetListAsync(input);

            if (alarmdata == null || alarmhostDto == null)
            {
                return "-1";
            }
            var alarmdto=  alarmdata.Items.Where(u => u.Alarm_ID == Alarm_ID).FirstOrDefault();
            string url = $"{dvrurl}/api/AlarmInfo/SetDefenceArmMode?DVR_IP={alarmhostDto.AlarmHostIP}&DVR_Name={alarmhostDto.User}&DVR_PassWord={alarmhostDto.Password}&AlarmChannel={alarmdto.Channel_ID-1}&DefenceState={DefenceState}";
            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            var refStatus = response.Content.ReadAsStringAsync().Result;
         
            return refStatus;

        }

        /// <summary>
        /// 获取布防状态 操作状态 1表示布防，2表示撤防
        /// </summary>
        /// <param name="Alarm_ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDefenceArmMode")]
        public async Task<string> GetDefenceArmModeAsync(string Alarm_ID)
        {
            PagedAndSortedResultRequestDto input = new PagedAndSortedResultRequestDto() { MaxResultCount = 1000, SkipCount = 0, Sorting = "id" };
            var alarmhostDto = _alarmAppService.GetAlarmHostDto(Alarm_ID);
            var alarmdata = await _alarmAppService.GetListAsync(input);
            if (alarmdata==null|| alarmhostDto==null)
            {
                return "-1";
            }
            var alarmdto = alarmdata.Items.Where(u => u.Alarm_ID == Alarm_ID).FirstOrDefault();
            string url = $"{dvrurl}/api/AlarmInfo/GetDefenceArmMode?DVR_IP={alarmhostDto.AlarmHostIP}&DVR_Name={alarmhostDto.User}&DVR_PassWord={alarmhostDto.Password}";
            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            var refStatus = response.Content.ReadAsStringAsync().Result;
            var refStatusData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int,string>>(refStatus);
            var  alarmStatusDto= refStatusData.Where(u => u.Key == alarmdto.Channel_ID - 1).FirstOrDefault();
            if (alarmStatusDto.Value!=null)
            {
                if (alarmStatusDto.Value== "ARMING")
                {
                    return "1";
                }
                else
                {
                    return "2";
                }
            }

            return "-1";

        }

    }
}





