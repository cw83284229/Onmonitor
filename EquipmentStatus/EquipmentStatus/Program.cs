using System;
using System.Collections.Generic;
using Alarm_GetStatus_AlarmDevice;
using EquipmentStatus.Models;
using Ruanmou.Redis.Exchange.Service;

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

            //   AlarmGetStatus.TaskLoginStartListen();//开启门磁检测

            // DVRInfoCheck.GetDVRInfoCheck();//开启主机轮询
            RedisStringService redisStringService = new RedisStringService();
            redisStringService.StringSet("bookstr02","我是一个测试");


            Console.WriteLine("请等待");
            Console.ReadLine();
         
        }
    }
}
