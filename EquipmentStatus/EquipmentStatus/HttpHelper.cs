using EquipmentStatus.Models;
using Newtonsoft.Json;
using OnMonitor.Monitor.Alarm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentStatus
{
    public class HttpHelper
    {
        public static HttpClient _httpClient;
        internal static readonly string serverurl = ConfigurationManager.AppSettings["ServerUrl"];

        public HttpHelper()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
        }
        /// <summary>
        /// 获取资料库全部主机信息
        /// </summary>
        /// <returns></returns>
        public List<AppAlarmHosts> GetAlarmHosts()
        {


            string url = $"{serverurl}api/app/alarm-host/alarm-hosts";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var requst = JsonConvert.DeserializeObject<List<AppAlarmHosts>>(data);

            return requst;



        }

        /// <summary>
        /// 依据主机IP地址查询门磁表
        /// </summary>
        /// <param name="AlarmHostIP"></param>
        /// <returns></returns>
        public List<AppAlarms> GetAlarmsByHostIP(string AlarmHostIP)
        {
            string url = $"{serverurl}api/app/alarm/alarm-list?AlarmHostIP={AlarmHostIP}";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var requst = JsonConvert.DeserializeObject<List<AppAlarms>>(data);
            return requst;

        }
        /// <summary>
        /// 依据Alarm_Id查询Dto
        /// </summary>
        /// <returns></returns>
        public AppAlarms GetAlarmbyAlarmID(string AlarmID)
        {
            string url = $"{serverurl}api/app/alarm/alarm-dto?Alarm_ID={AlarmID}";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var requst = JsonConvert.DeserializeObject<AppAlarms>(data);
            return requst;

        }
        /// <summary>
        /// 依据Alarm_Id查询ManageState
        /// </summary>
        /// <returns></returns>
        public List<AppAlarmManageStates> GetAlarmManageStates(string AlarmID)
        {
            string url = $"{serverurl}api/app/alarm-manage-state/alarm-manage-states?Alarm_ID={AlarmID}";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var requst = JsonConvert.DeserializeObject<List<AppAlarmManageStates>>(data);
            return requst;

        }

        /// <summary>
        /// 新增AlarmManageState
        /// </summary>
        /// <param name="stateDto"></param>
        /// <returns></returns>
        public string AddAlarmManageState(UpdateAlarmManageStateDto stateDto)
        {

            string url = $"{serverurl}api/app/alarm-manage-state";
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                { "alarmHost_IP", stateDto.AlarmHost_IP},
                { "alarm_ID",stateDto.Alarm_ID},
                { "channel_ID",stateDto.Channel_ID.ToString() },
                { "alarmTime",stateDto.AlarmTime},
                { "withdrawTime",stateDto.WithdrawTime},
                { "withdrawMan",stateDto.WithdrawMan},
                {"withdrawRemark", stateDto.WithdrawRemark},
                {"defenceTime", stateDto.DefenceTime},
                {"treatmentTime",stateDto.TreatmentTime},
                {"treatmentTimeState",stateDto.TreatmentTimeState},
                {"treatmentMan",stateDto.WithdrawMan},
                {"treatmentReply",stateDto.TreatmentReply},
                {"anomalyType",stateDto.AnomalyType},
                {"remark", stateDto.Remark}
            };

            var content = new FormUrlEncodedContent(dic);
            var response = _httpClient.PostAsync(url, content).Result;

            return response.Content.ReadAsStringAsync().Result;
        }
        /// <summary>
        /// 修改AlarmManageState
        /// </summary>
        /// <param name="stateDto"></param>
        /// <returns></returns>
        public string UpdateAlarmManageState(AppAlarmManageStates stateDto)
        {

            string url = $"{serverurl}api/app/alarm-manage-state/{stateDto.Id}";
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                { "alarmHost_IP", stateDto.AlarmHost_IP},
                { "alarm_ID",stateDto.Alarm_ID},
                { "channel_ID",stateDto.Channel_ID.ToString() },
                { "alarmTime",stateDto.AlarmTime},
                { "withdrawTime",stateDto.WithdrawTime},
                { "withdrawMan",stateDto.WithdrawMan},
                {"withdrawRemark", stateDto.WithdrawRemark},
                {"defenceTime", stateDto.DefenceTime},
                {"treatmentTime",stateDto.TreatmentTime},
                {"treatmentTimeState",stateDto.TreatmentTimeState},
                {"treatmentMan",stateDto.WithdrawMan},
                {"treatmentReply",stateDto.TreatmentReply},
                {"anomalyType",stateDto.AnomalyType},
                {"remark", stateDto.Remark}
            };

            var content = new FormUrlEncodedContent(dic);
            var response = _httpClient.PutAsync(url, content).Result;

            return response.Content.ReadAsStringAsync().Result;
        }

    }
}
