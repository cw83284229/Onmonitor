﻿using AlarmServer.Models;
using CCTV_Client.DaHua;
using EquipmentStatus.Models;
using NetSDKCS;
using OnMonitor.Monitor.Alarm;
using Ruanmou.Redis.Exchange.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace EquipmentStatus
{
    public class AlarmGetStatus
    {
     
        private static IntPtr lLoginID = IntPtr.Zero;
        private static IntPtr lAttachHandle = IntPtr.Zero;
        private static NET_DEVICEINFO_Ex device = new NET_DEVICEINFO_Ex();
        public static object locker = new object();
        private static fDisConnectCallBack disConnectCallBack = new fDisConnectCallBack(DisConnectCallBack);
        private static fHaveReConnectCallBack haveReConnectCallBack = new fHaveReConnectCallBack(HaveReConnectCallBack);
        private static fMessCallBackEx messCallBackEx = new fMessCallBackEx(AlarmCallBackEx);
        private static RedisHashService  service = new RedisHashService();
        private static HttpHelper httpHelper = new HttpHelper();


        public static void TaskLoginStartListen()
        {
           
            Console.WriteLine("开启报警主机自动监听");
          
            var listAlarmHost =httpHelper.GetAlarmHosts();
            foreach (var item in listAlarmHost)
            {

                 Task task = Task.Run(() => LoginStartListenAsync(item.AlarmHostIP, "37777", item.User, item.Password));
                 task.Wait();
            }

        }

        //启动监听
        public static async Task LoginStartListenAsync(string ip, string prot, string user, string pwd)
        {
            Console.WriteLine($"使用线程ID：{Thread.CurrentThread.ManagedThreadId}");
            loghelper.loginfo.Info($"使用线程ID：{Thread.CurrentThread.ManagedThreadId}");
            NETClient.Init(disConnectCallBack, IntPtr.Zero, null);//初始化设置断线回掉

            NETClient.SetAutoReconnect(haveReConnectCallBack, IntPtr.Zero);//设定自动重连

            lLoginID = NETClient.Login(ip, Convert.ToUInt16(prot), user, pwd, EM_LOGIN_SPAC_CAP_TYPE.TCP, IntPtr.Zero, ref device);
           

                DaHuaSDKHelper daHuaSDK = new DaHuaSDKHelper();

                var alarmdata = httpHelper.GetAlarmsByHostIP(ip);
                var alarminfo = daHuaSDK.GetAlarmStatus(lLoginID);
                var onlineinfo = daHuaSDK.GetConnectionStatus(lLoginID);
                var defenceinfo = daHuaSDK.GetDefenceArmMode(lLoginID);

                lock (locker)
                {
                    foreach (var item in alarmdata)
                    {
                        AppAlarmStatus appAlarmStatus = new AppAlarmStatus();

                        var der = alarminfo.Where(u => u.Key == item.Channel_ID - 1).FirstOrDefault().Value;
                        if (!string.IsNullOrEmpty(der))//报警数据导入
                        {
                            appAlarmStatus.IsAlarm = 1;
                        }
                        else
                        {
                            appAlarmStatus.IsAlarm = 0;
                        }
                        //
                        if (onlineinfo.Where(u => u.Key == item.Channel_ID - 1).FirstOrDefault().Key > 0)//异常数据
                        {
                            appAlarmStatus.IsAnomaly = int.Parse(onlineinfo.Where(u => u.Key == item.Channel_ID - 1).FirstOrDefault().Value);
                        }

                        var defn = defenceinfo.Where(u => u.Key == item.Channel_ID - 1).FirstOrDefault().Value;

                        if (!string.IsNullOrEmpty(defn))
                        {
                            if (defn == EM_DEFENCEMODE.ARMING.ToString())
                            {
                                appAlarmStatus.IsDefence = 1;//布防
                            }
                            else if (defn == EM_DEFENCEMODE.DISARMING.ToString())
                            {
                                appAlarmStatus.IsDefence = 2;//撤防
                            }
                            else
                            {
                                appAlarmStatus.IsDefence = -1;//未知
                            }

                        }//布防数据
                    lock (locker)//访问http加锁
                    {
                       // var defg = httpHelper.GetAlarmManageStates(item.Alarm_ID).FirstOrDefault();
                   
                        //if (defg != null)
                        //{
                        //    if (defg.TreatmentTimeState != null)//处理状态
                        //    {
                        //        appAlarmStatus.TreatmentState = 0;
                        //    }
                        //    else
                        //    {
                                appAlarmStatus.TreatmentState = 1;
                         //   }
                       // }
                        appAlarmStatus.Channel_ID = item.Channel_ID;
                        appAlarmStatus.IsOpenDoor = item.IsOpenOrClosed;//开岗数据
                        appAlarmStatus.LastModificationTime = DateTime.Now.ToString();
                        appAlarmStatus.Alarm_ID = item.Alarm_ID;
                        appAlarmStatus.AlarmHostIP = ip;
                    }
                    service.HashSet<AppAlarmStatus>("AlarmStatus_" + item.Alarm_ID, "data", appAlarmStatus);//把数据存到Redis


                        Console.WriteLine($"ip:{ip}，通道：{item.Channel_ID},初始化成功！，时间：{appAlarmStatus.LastModificationTime}");
                        loghelper.WriteLog($"ip:{ip}，通道：{item.Channel_ID},初始化成功");
                    }

                }
            
            NETClient.SetDVRMessCallBack(messCallBackEx, IntPtr.Zero);//设置报警回掉
            if (IntPtr.Zero != lLoginID)
            {
                bool result = NETClient.StartListen(lLoginID);
                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"IP：{ip}开启监听模式");
                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"IP：{ip}登陆失败");
            }
        }
      
        
        private static void Logout()
        {
            NETClient.Logout(lLoginID);
            NETClient.Cleanup();
        }

        //掉线回掉
        private static void DisConnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            Console.WriteLine($"主机{pchDVRIP}掉线,时间:{DateTime.Now.ToString()}");
            loghelper.WriteLog($"主机{pchDVRIP}掉线,时间:{DateTime.Now.ToString()}");
        }
        //重连回掉
        private static void HaveReConnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            Console.WriteLine($"主机{pchDVRIP}掉线:重新链接Reconnected,时间:{DateTime.Now.ToString()}");
            loghelper.WriteLog($"主机{pchDVRIP}掉线:重新链接Reconnected,时间:{DateTime.Now.ToString()}");

        }

        //报警事件回掉
        private static bool AlarmCallBackEx(int lCommand, IntPtr lLoginID, IntPtr pBuf, uint dwBufLen, IntPtr pchDVRIP, int nDVRPort, bool bAlarmAckFlag, int nEventID, IntPtr dwUser)
        {
            EM_ALARM_TYPE type = (EM_ALARM_TYPE)lCommand;
            string hostIp = Marshal.PtrToStringAnsi(pchDVRIP);
            
            var alarmdata =httpHelper.GetAlarmsByHostIP(hostIp);
            switch (type)
            {
                case EM_ALARM_TYPE.ALARM_ARMMODE_CHANGE_EVENT:
                    {
                        NET_ALARM_ARMMODE_CHANGE_INFO info = (NET_ALARM_ARMMODE_CHANGE_INFO)Marshal.PtrToStructure(pBuf, typeof(NET_ALARM_ARMMODE_CHANGE_INFO));
                        Console.WriteLine("布撤防状态变化事件信息：变化后状态:{0},场景模式:{1},触发方式:{2},ID:{3}",
                            GetStatus(info.bArm), GetScene(info.emSceneMode), GetTrigger(info.emTriggerMode), info.dwID);
                    }
                    break;
                case EM_ALARM_TYPE.ALARM_INPUT_SOURCE_SIGNAL://报警状态变化写入
                    {
                        NET_ALARM_INPUT_SOURCE_SIGNAL_INFO info = (NET_ALARM_INPUT_SOURCE_SIGNAL_INFO)Marshal.PtrToStructure(pBuf, typeof(NET_ALARM_INPUT_SOURCE_SIGNAL_INFO));
                        var alarm = alarmdata.Where(u => u.Channel_ID-1 == info.nChannelID).FirstOrDefault();
                        if (alarm==null)
                        {
                            break;
                        }
                        //AppAlarmStatus appAlarmStatus = alarmstatusdto.Where(u => u.Channel_ID-1 == info.nChannelID).FirstOrDefault();
                        AppAlarmStatus appAlarmStatus = service.HashGet<AppAlarmStatus>("AlarmStatus_" + alarm.Alarm_ID, "data");
                        if (info.nAction == 0)  // 0:开始 1:停止(设备内0表示报警)
                        {
                            if (appAlarmStatus != null)
                            {
                                appAlarmStatus.IsOpenDoor = alarm.IsOpenOrClosed;
                                appAlarmStatus.IsAlarm = 1;
                                appAlarmStatus.LastModificationTime = DateTime.Now.ToString();
                                appAlarmStatus.TreatmentState = 1;
                                appAlarmStatus.AlarmHostIP = hostIp;
                                service.HashSet<AppAlarmStatus>("AlarmStatus_" + alarm.Alarm_ID, "data", appAlarmStatus);
                                if (appAlarmStatus.IsDefence == 1)//报警
                                {
                                    UpdateAlarmManageStateDto AppAlarmManage = new UpdateAlarmManageStateDto();
                                    AppAlarmManage.AlarmHost_IP = hostIp;
                                    AppAlarmManage.Alarm_ID = alarm.Alarm_ID;
                                    AppAlarmManage.Channel_ID = info.nChannelID+1;
                                    AppAlarmManage.AlarmTime = DateTime.Now.ToString();
                                    httpHelper.AddAlarmManageState(AppAlarmManage);
                                    service.HashSet<AppAlarmStatus>("AlarmStatus_" + alarm.Alarm_ID, "data", appAlarmStatus);
                                    Console.WriteLine("设备处于布防状态");
                                }
                            }
                            else
                            {

                                UpdateAlarmManageStateDto AppAlarmManageStates2 = new  UpdateAlarmManageStateDto();
                                AppAlarmManageStates2.AlarmHost_IP = hostIp;
                                AppAlarmManageStates2.AlarmTime =info.stuTime.ToString();
  
                                AppAlarmManageStates2.Channel_ID= info.nChannelID;
                                httpHelper.AddAlarmManageState(AppAlarmManageStates2);
                               
                                Console.WriteLine("此模块未添加进资料库");
                            }
 
                        }
                        else//消除报警状态
                        {
      
                            if (appAlarmStatus!=null)
                            {
                                appAlarmStatus.IsAlarm = 0;
                                appAlarmStatus.LastModificationTime = DateTime.Now.ToString();
                           
                                service.HashSet<AppAlarmStatus>("AlarmStatus_" + alarm.Alarm_ID, "data", appAlarmStatus);
                            }
                            else
                            {
                                //AppAlarmStatus appAlarmStatus4 = new AppAlarmStatus();
                                //appAlarmStatus4.IsAlarm = 0;
                                //appAlarmStatus4.Channel_ID = info.nChannelID;
                                //appAlarmStatus4.Alarm_ID = info.nChannelID.ToString();
                                //appAlarmStatus4.AlarmHostIP = hostIp;
                                //appAlarmStatus4.LastModificationTime = info.stuTime.ToString();
                                //StatusDB.Add(appAlarmStatus4);
                            }
                        }

                        Console.WriteLine($"报警输入源事件信息：IP:{hostIp},通道:{info.nChannelID+1},动作:{ GetAction(info.nAction)},报警时间:{DateTime.Now.ToString()}");
                        loghelper.WriteLog($"报警输入：IP:{hostIp},通道:{info.nChannelID+1},动作:{ GetAction(info.nAction)}");
                    }
                    break;
                case EM_ALARM_TYPE.ALARM_DEFENCE_ARMMODE_CHANGE://撤布防状态变化写入
                    {
                        NET_ALARM_DEFENCE_ARMMODECHANGE_INFO info = (NET_ALARM_DEFENCE_ARMMODECHANGE_INFO)Marshal.PtrToStructure(pBuf, typeof(NET_ALARM_DEFENCE_ARMMODECHANGE_INFO));
                      var alarmstatuslist=  httpHelper.GetAlarmsByHostIP(hostIp);
                      var appAlarm = alarmstatuslist.Where(u => u.Channel_ID == info.nDefenceID+1).FirstOrDefault();
                        if (appAlarm==null)
                        {
                            break;
                        }
                        if (!service.HashExists(appAlarm.Alarm_ID,"data"))
                        {
                            var appAlarmStatus = service.HashGet<AppAlarmStatus>(appAlarm.Alarm_ID, "data");
                            if (appAlarmStatus!=null)
                            {
                                if (info.emDefenceStatus == EM_DEFENCEMODE.ARMING)
                                {
                                    appAlarmStatus.IsDefence = 1;//布防
                                }
                                else if (info.emDefenceStatus == EM_DEFENCEMODE.DISARMING)
                                {
                                    appAlarmStatus.IsDefence = 2;//撤防
                                }
                                else
                                {
                                    appAlarmStatus.IsDefence = -1;//未知
                                }
                                appAlarmStatus.LastModificationTime = DateTime.Now.ToString();

                                service.HashSet("AlarmStatus_" + appAlarmStatus.Alarm_ID, "data", appAlarmStatus);
                            }
                           
                        }
                        //更新AppAlarmManageStates数据
                        AppAlarmManageStates appAlarmManage = httpHelper.GetAlarmManageStates(appAlarm.Alarm_ID).FirstOrDefault();                       
                        if (appAlarmManage != null&&appAlarmManage.AlarmTime!=null)
                        {
                               
                           if (string.IsNullOrEmpty(appAlarmManage.WithdrawTime))
                              {   //判断异常
                                    if (info.emDefenceStatus == EM_DEFENCEMODE.ARMING)
                                    {
                                        appAlarmManage.DefenceTime = DateTime.Now.ToString();
                                        appAlarmManage.WithdrawRemark = "用户布防";
                                    }
                                    else
                                    {
                                        appAlarmManage.WithdrawTime =DateTime.Now.ToString();
                                        appAlarmManage.WithdrawRemark = "报警撤防";
                                        appAlarmManage.WithdrawMan = "系统默认";
                                    }
                               }
                         else
                              {
                                 if (info.emDefenceStatus == EM_DEFENCEMODE.ARMING)
                                     {
                                      appAlarmManage.DefenceTime = DateTime.Now.ToString();
                                       //  appAlarmManage.WithdrawRemark = "用户布防";
                                     }
                                else
                                    {
                                    appAlarmManage.WithdrawTime =DateTime.Now.ToString();
                                    appAlarmManage.WithdrawRemark = "重复撤防";
                                    appAlarmManage.WithdrawMan = "系统默认";
                                    }
                               }
                                appAlarmManage.LastModificationTime = DateTime.Now;
                               httpHelper.UpdateAlarmManageState(appAlarmManage);
 
                        }
                        else
                          {
                            UpdateAlarmManageStateDto appAlarmManage3 = new UpdateAlarmManageStateDto();
                            appAlarmManage3.AlarmHost_IP = hostIp;
                            appAlarmManage3.Channel_ID = info.nDefenceID + 1;
                            if (alarmdata.Where(u => u.Channel_ID - 1 == info.nDefenceID).FirstOrDefault() != null)
                            {
                                appAlarmManage3.Alarm_ID = alarmdata.Where(u => u.Channel_ID - 1 == info.nDefenceID).FirstOrDefault().Alarm_ID;
                            }
                           
                            if (info.emDefenceStatus == EM_DEFENCEMODE.ARMING)
                            {
                                appAlarmManage3.DefenceTime = DateTime.Now.ToString();
                            }
                            else
                            {
                                appAlarmManage3.WithdrawTime =DateTime.Now.ToString();
                                appAlarmManage3.WithdrawRemark = "用户撤防";
                                appAlarmManage3.WithdrawMan = "系统默认";

                            }
                           httpHelper.AddAlarmManageState(appAlarmManage3);


                        }


                        Console.WriteLine($"防区布撤防状态改变事件信息：IP:{hostIp},状态:{GetDefence(info.emDefenceStatus)}, 防区号:{info.nDefenceID+1},时间：{DateTime.Now.ToString()}");
                        loghelper.WriteLog($"防区布撤防信息：IP:{hostIp}, 防区号:{info.nDefenceID+1},状态:{GetDefence(info.emDefenceStatus)}");
                    }
                    break;
                default:
                    Console.WriteLine(lCommand.ToString("X"));
                    break;
            }
            return true;
        }

        private static string GetStatus(EM_ALARM_MODE mode)
        {
            switch (mode)
            {
                case EM_ALARM_MODE.UNKNOWN:
                    return "未知";
                case EM_ALARM_MODE.DISARMING:
                    return "撤防";
                case EM_ALARM_MODE.ARMING:
                    return "布防";
                case EM_ALARM_MODE.FORCEON:
                    return "强制布防";
                case EM_ALARM_MODE.PARTARMING:
                    return "部分布防";
                default:
                    return "未知";
            }
        }
        private static string GetScene(EM_SCENE_MODE mode)
        {
            switch (mode)
            {
                case EM_SCENE_MODE.UNKNOWN:
                    return "未知";
                case EM_SCENE_MODE.OUTDOOR:
                    return "外出模式";
                case EM_SCENE_MODE.INDOOR:
                    return "室内模式";
                case EM_SCENE_MODE.WHOLE:
                    return "全局模式";
                case EM_SCENE_MODE.RIGHTNOW:
                    return "立即模式";
                case EM_SCENE_MODE.SLEEPING:
                    return "就寝模式";
                case EM_SCENE_MODE.CUSTOM:
                    return "自定义模式";
                default:
                    return "未知";
            }
        }
        private static string GetTrigger(EM_TRIGGER_MODE mode)
        {
            switch (mode)
            {
                case EM_TRIGGER_MODE.UNKNOWN:
                    return "未知";
                case EM_TRIGGER_MODE.NET:
                    return "网络用户";
                case EM_TRIGGER_MODE.KEYBOARD:
                    return "键盘";
                case EM_TRIGGER_MODE.REMOTECONTROL:
                    return "遥控器";
                default:
                    return "未知";
            }
        }
        private static string GetAction(int action)
        {
            if (action == 0)
            {
                return "开始";
            }
            else if (action == 1)
            {
                return "停止";
            }
            else
            {
                return "";
            }
        }
        private static string GetDefence(EM_DEFENCEMODE mode)
        {
            switch (mode)
            {
                case EM_DEFENCEMODE.UNKNOWN:
                    return "未知";
                case EM_DEFENCEMODE.ARMING:
                    return "布防";
                case EM_DEFENCEMODE.DISARMING:
                    return "撤防";
                default:
                    return "未知";
            }
        }



    }
}
