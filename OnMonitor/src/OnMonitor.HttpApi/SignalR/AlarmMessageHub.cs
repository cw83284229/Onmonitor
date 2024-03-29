﻿using Microsoft.AspNetCore.SignalR;
using OnMonitor.Models.SignalRModel;
using OnMonitor.Monitor.Alarm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.SignalR;

namespace OnMonitor.SignalR
{

    [HubRoute("AlarmSignalR")]
    public  class AlarmMessageHub : AbpHub
    {
        public static object locker = new object();
        public IAlarmStatusAppService _alarmStatusAppService;
        IHubContext<AlarmMessageHub> _hubContext;
        public ConditionAlarmStatusDto conditionAlarmStatus;
        public PagedSortedRequestDto resultRequestDto;
        Dictionary<string, int> NewdicStatusCount;
        List<ResponseSignalRDto> newAlarmlist;
        List<ResponseSignalRDto> newTreatmentlist;
        Dictionary<string, int> dicStatusCount = new Dictionary<string, int>();
        List<ResponseSignalRDto> Alarmlist = new List<ResponseSignalRDto>();
        List<ResponseSignalRDto> Treatmentlist = new List<ResponseSignalRDto>();
        public Microsoft.Extensions.Caching.Distributed.IDistributedCache _cache;
        System.Timers.Timer timer = null;
        List<RequstAlarmStatusDto> dataall;
        public AlarmMessageHub(IAlarmStatusAppService alarmStatusAppService, IHubContext<AlarmMessageHub> hubContext, Microsoft.Extensions.Caching.Distributed.IDistributedCache cache)
        {
            _alarmStatusAppService = alarmStatusAppService;
            _hubContext = hubContext;
            _cache = cache;
       
        }

        public  void AlarmStatusAsync(string Manage)
        {
            GetRequstAlarms();


            if (timer==null)
            {
                timer = new System.Timers.Timer(2000);
            }
            lock (locker)
            {
                timer.AutoReset = true;
                timer.Elapsed += new System.Timers.ElapsedEventHandler(aTimer_Elapsed);
                timer.Enabled = true;
            }

            #region 暂时移除
            //while (true)
            //{
            //    Thread.Sleep(2000);

            //    var dataall = _alarmStatusAppService.GetRequstListAll();

            //    NewdicStatusCount = GetAlarmStatusCount(dataall);
            //    newAlarmlist = GetInAlarm(dataall);
            //    newTreatmentlist = GetTreatmentState(dataall);

            //    if (!dicStatusCount.SequenceEqual(NewdicStatusCount))
            //    {
            //        //调用表头回掉方法
            //        dicStatusCount = NewdicStatusCount;
            //        var dd = newAlarmlist.GetHashCode();

            //        string strStatusCount = Newtonsoft.Json.JsonConvert.SerializeObject(NewdicStatusCount);
            //        Clients.All.SendAsync("ReceiveCount", strStatusCount);

            //    }

            //    if (!Alarmlist.Equals(newAlarmlist))
            //    {  //调用报警回掉方法
            //        Alarmlist = newAlarmlist;
            //        string strAlarmStatus = Newtonsoft.Json.JsonConvert.SerializeObject(Alarmlist);
            //        Clients.All.SendAsync("ReceiveAlarm", strAlarmStatus);
            //    }

            //    if (!Treatmentlist.Equals(newTreatmentlist))
            //    {    //调用未处理回掉
            //        Treatmentlist = newTreatmentlist;
            //        string strAlarmTreatment = Newtonsoft.Json.JsonConvert.SerializeObject(Treatmentlist);
            //        Clients.All.SendAsync("ReceiveTreatment", strAlarmTreatment);
            //    }

            //    newAlarmlist.Clear();
            //    newTreatmentlist.Clear();
            //    NewdicStatusCount.Clear();
            //}   
            #endregion
        }   

        public override Task OnDisconnectedAsync(Exception exception)
        {
           // AlarmStatusAsync("0");
            return base.OnDisconnectedAsync(exception);
        }
        public override Task OnConnectedAsync()
        {
            
               AlarmStatusAsync("1");
           
            Dispose();
            return base.OnConnectedAsync();
           
           
        }

        public  string AlarmDispaly(string Manage)
        {

          //  AlarmStatusAsync("0");
            base.Context.Abort();

            return "ok";
        }


        /// <summary>
        /// 获取标头数据
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetAlarmStatusCount(List<RequstAlarmStatusDto> data)
        {

            var alarmint = data.Where(u => u.IsDefence == 1).Where(i => i.IsAlarm == 1).Where(k => k.IsAnomaly == 2).Count();//报警数量
            var defenceint = data.Where(u => u.IsDefence == 1).Count();
            var notdefineint = data.Where(u => u.IsDefence == 2).Count();
            var openDoorint = data.Where(u => u.IsOpenDoor == true).Count();
            var closeDoorint = data.Where(u => u.IsOpenDoor == false).Count();
            var treatmentint = data.Where(u => u.TreatmentState == 1).Count();
            var onlineint = data.Where(u => u.IsAnomaly == 2).Count();
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("门磁数量", (int)data.Count());
            dic.Add("报警数据", alarmint);
            dic.Add("布防数据", defenceint);
            dic.Add("撤防数据", notdefineint);
            dic.Add("在线数据", onlineint);
            dic.Add("离线数据", data.Where(u => u.IsAnomaly == 1 || u.IsAnomaly == 0).Count());
            dic.Add("开岗数据", openDoorint);
            dic.Add("封岗数据", closeDoorint);
            dic.Add("未处理数据", treatmentint);
           
            return dic;

        }

        #region 获取报警数据
       
        public  List<ResponseSignalRDto> GetInAlarm(List<RequstAlarmStatusDto> data)
        {

            data = data.Where(u => u.IsDefence == 1).Where(i => i.IsAlarm == 1).Where(k => k.IsAnomaly == 2).OrderByDescending(q=>q.LastModificationTime).ToList();
           
            List<ResponseSignalRDto> Responselist = new List<ResponseSignalRDto>();
            foreach (var item in data)
            {
                ResponseSignalRDto responseSignalRDto = new ResponseSignalRDto();
                var timeSpan = (int)(DateTime.Now - DateTime.Parse(item.LastModificationTime)).TotalMinutes;
                string str = $"{item.Alarm_ID} {item.Build}-{item.floor} {item.Location} {item.LastModificationTime} {timeSpan}";
                responseSignalRDto.Color = "red";
                responseSignalRDto.Size = "large";
                responseSignalRDto.Content = str;
                responseSignalRDto.Action = $"<button type=\"button\" onclick=\"gateMapOperation('{item.Alarm_ID} {item.Build}-{item.floor} {item.Location}')\">操作</button>";
                Responselist.Add(responseSignalRDto);

            }
           
            return Responselist;
        }
        #endregion

        #region 获取未处理数据

        public List<ResponseSignalRDto> GetTreatmentState(List<RequstAlarmStatusDto> data)
        {
            data = data.Where(u => u.TreatmentState == 1).OrderByDescending(i=>i.LastModificationTime).ToList();
            List<ResponseSignalRDto> Responselist = new List<ResponseSignalRDto>();

            foreach (var item in data)
            {
                ResponseSignalRDto responseSignalRDto = new ResponseSignalRDto();
                var timeSpan = (int)(DateTime.Now - DateTime.Parse(item.LastModificationTime)).TotalMinutes;
                string str = $"{item.Alarm_ID} {item.Build}-{item.floor} {item.Location} {item.LastModificationTime} {timeSpan}";
                responseSignalRDto.Color = "yellow";
                responseSignalRDto.Size = "large";
                responseSignalRDto.Content = str;
                responseSignalRDto.Action = $"<button type=\"button\" onclick=\"gateNoHandleOperation('{item.Alarm_ID} {item.Build}-{item.floor} {item.Location}')\">操作</button>";
                Responselist.Add(responseSignalRDto);
            }
           
            return Responselist;
        }
        #endregion

        public List<RequstAlarmStatusDto> GetRequstAlarms()
        {
            lock (locker)
            {
                dataall = _alarmStatusAppService.GetRequstListAll().ToList();
            }
            return dataall;
        }

        void aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
          
            foreach (var item in dataall)
            {
                var alarmstatusres = _cache.Get("AlarmStatus_"+item.Alarm_ID);
                var strAlarmStatus = Encoding.UTF8.GetString(alarmstatusres);
                var alarmstatusDto = Newtonsoft.Json.JsonConvert.DeserializeObject<AlarmStatusDto>(strAlarmStatus);
                item.BypassState = alarmstatusDto.BypassState;
                item.IsAlarm = alarmstatusDto.IsAlarm;
                item.IsAnomaly = alarmstatusDto.IsAnomaly;
                item.IsDefence = alarmstatusDto.IsDefence;
                item.IsOpenDoor = alarmstatusDto.IsOpenDoor;
                item.LastModificationTime = alarmstatusDto.LastModificationTime;
                item.TreatmentState = alarmstatusDto.TreatmentState;
            }

            NewdicStatusCount = GetAlarmStatusCount(dataall);
            newAlarmlist = GetInAlarm(dataall);
            newTreatmentlist = GetTreatmentState(dataall);

            if (!dicStatusCount.SequenceEqual(NewdicStatusCount))
            {
                //调用表头回掉方法
                dicStatusCount = NewdicStatusCount;
                var dd = newAlarmlist.GetHashCode();

                string strStatusCount = Newtonsoft.Json.JsonConvert.SerializeObject(NewdicStatusCount);
                _hubContext.Clients.All.SendAsync("ReceiveCount", strStatusCount);

            }

            if (!Alarmlist.Equals(newAlarmlist))
            {  //调用报警回掉方法
                Alarmlist = newAlarmlist;
                string strAlarmStatus = Newtonsoft.Json.JsonConvert.SerializeObject(Alarmlist);
                _hubContext.Clients.All.SendAsync("ReceiveAlarm", strAlarmStatus);
            }

            if (!Treatmentlist.Equals(newTreatmentlist))
            {    //调用未处理回掉
                Treatmentlist = newTreatmentlist;
                string strAlarmTreatment = Newtonsoft.Json.JsonConvert.SerializeObject(Treatmentlist);
                _hubContext.Clients.All.SendAsync("ReceiveTreatment", strAlarmTreatment);
            }

            newAlarmlist.Clear();
            newTreatmentlist.Clear();
            NewdicStatusCount.Clear();

            Dispose();
            timer.Start();
        }

    }
}
