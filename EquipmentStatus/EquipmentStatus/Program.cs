using OnMonitor.Monitor.Alarm;
using System;

namespace EquipmentStatus
{
    class Program
    {
       
        /// <summary>
        ///  Main the Enter of exe
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

              AlarmGetStatus.TaskLoginStartListen();//开启门磁检测

            // DVRInfoCheck.GetDVRInfoCheck();//开启主机轮询

            //HttpHelper httpHelper = new HttpHelper();
            //UpdateAlarmManageStateDto appAlarmManage3 = new UpdateAlarmManageStateDto();
            //appAlarmManage3.AlarmHost_IP = "172.30.116.88";
            //appAlarmManage3.Alarm_ID = "B012";
            //appAlarmManage3.Channel_ID = 12;
            //appAlarmManage3.DefenceTime = "2021-01-01 00:00:00";
            //var dd=   httpHelper.AddAlarmManageState(appAlarmManage3);
          
            Console.WriteLine("请等待");
            Console.ReadLine();
         
        }
    }
}
