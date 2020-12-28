using CCTV_Client.DaHua;
using EquipmentStatus.Models;
using EquipmentStatus.Models.Models;
using NetSDKCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm_GetStatus_AlarmDevice
{
  public  class DVRInfoCheck
    {
        private static NET_DEVICEINFO_Ex device = new NET_DEVICEINFO_Ex();
        private static IntPtr m_LoginID = IntPtr.Zero;


        /// <summary>
        /// 定时任务，自动对比主机数据，每天2:00启动一次
        /// </summary>

        public static void  GetDVRInfoCheck()
        {
            EFDBHelp<AppDVRs> dvrDB = new EFDBHelp<AppDVRs>();
         
            var dvrdata = dvrDB.FindList(u=>u.Id>0);

            List<AppDVRCheckInfos> listdVRCheckInfo = new List<AppDVRCheckInfos>();

            foreach (var item in dvrdata)
            {
                DVRInfoCheckService(item);
               
            }
        }

        public static void  DVRInfoCheckService(AppDVRs appDVRs )
        {
            EFDBHelp<AppDVRCheckInfos> dvrCheckInfoDB = new EFDBHelp<AppDVRCheckInfos>();
            DaHuaSDKHelper daHuaSDK = new DaHuaSDKHelper();
            AppDVRCheckInfos dVRCheckInfo = new AppDVRCheckInfos();
                daHuaSDK.DeviceInititalize();
                m_LoginID = daHuaSDK.LoginClick(appDVRs.DVR_IP,appDVRs.DVR_port,appDVRs.DVR_usre,appDVRs.DVR_possword, ref device);

                if (m_LoginID == IntPtr.Zero)//在线检查
                {
                    dVRCheckInfo.DVR_Online = false;
                }
                else
                {
                    dVRCheckInfo.DVR_Online = true;
                    dVRCheckInfo.DVR_SN = device.sSerialNumber;
                    if (appDVRs.DVR_SN == device.sSerialNumber)
                    {
                        dVRCheckInfo.SNChenk = true;
                    }
                    else
                    {
                        dVRCheckInfo.SNChenk = false;
                    }

                }

                DateTime dvrtime = daHuaSDK.GetDVRTime(m_LoginID);
                var diskinfo = daHuaSDK.GetDiskInfo(m_LoginID);


                dVRCheckInfo.DVR_ID = appDVRs.DVR_ID;
                dVRCheckInfo.LastModificationTime = DateTime.Now;
                dVRCheckInfo.CreationTime = DateTime.Now;

                //时间检查验证
                var servertime = DateTime.Now;

                dVRCheckInfo.DVRTime = dvrtime.ToString(); ;
                if (DateTime.Compare(servertime.AddSeconds(-5), dvrtime) < 0 && DateTime.Compare(servertime.AddSeconds(5), dvrtime) > 0)
                {
                    dVRCheckInfo.TimeInfoChenk = true;
                }
                else
                {
                    dVRCheckInfo.TimeInfoChenk = false;
                }

            //硬盘检查

            if (appDVRs.Hard_drive==null)
            {
                appDVRs.Hard_drive = 0;
            }
            int dvrhard = (int)(appDVRs.Hard_drive * 0.91 );
            var disksum = diskinfo.Sum(u => int.Parse(u.TotalSpace)) / 1024 / 1024;
               
                if (dvrhard == disksum)
                {
                    dVRCheckInfo.DiskTotal = disksum;
                    dVRCheckInfo.DiskChenk = true;
                }
                else
                {
                    dVRCheckInfo.DiskTotal = disksum;
                    dVRCheckInfo.DiskChenk = false;
                }


                //90天存储检查

                //String startTime = DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd hh:mm:ss");
                //String endTime = DateTime.Now.AddDays(-90).AddHours(1).ToString("yyyy-MM-dd hh:mm:ss"); ;
                //string url2 = $"{dvrurl}/api/DVRInfo/QueryVideoFileByTime?IP={item.DVR_IP}&name={item.DVR_usre}&password={item.DVR_possword}&startTimestr={startTime}&endTimestr={endTime}";
                //var handler2 = new HttpClientHandler();
                //var response2 = _httpClient.GetAsync(url2).Result;
                //var dt2 = response2.Content.ReadAsStringAsync().Result;
                //var data2 = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(dt2);

                //if (data2==-1)
                //{
                //    response2 = _httpClient.GetAsync(url2).Result;
                //    dt2 = response2.Content.ReadAsStringAsync().Result;
                //   data2 = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(dt2);
                //}


                //if (data2>0)
                //{
                //    dVRCheckInfo.VideoCheck90Day = true;
                //}
                //if (data2==0)
                //{
                //    dVRCheckInfo.VideoCheck90Day = false;
                //}


                var res = dvrCheckInfoDB.Add(dVRCheckInfo);
                if (res > 0)
                {
                    Console.WriteLine($"{appDVRs.DVR_ID}+{DateTime.Now}+写入成功");
                }
                else
                {
                    Console.WriteLine($"{appDVRs.DVR_ID}+{DateTime.Now}+写入失败");
                }

        }
    }
}
