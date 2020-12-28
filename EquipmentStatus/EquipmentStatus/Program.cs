using System;
using System.Collections.Generic;
using System.Text;
using NetSDKCS;
using System.Runtime.InteropServices;
using System.Threading;
using Alarm_GetStatus_AlarmDevice;
using EquipmentStatus.Models;
using System.Linq;
using System.Data.Entity.Migrations;

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

              AlarmGetStatus.TaskLoginStartListen();
            EFDBHelp<AppAlarms> eFDBHelp = new EFDBHelp<AppAlarms>();

           // DVRInfoCheck.GetDVRInfoCheck();
            
            //var data = eFDBHelp.FindList(u => u.Id > 0).ToList();

            //foreach (var item in data)
            //{
            //    item.CreationTime = DateTime.Now;

            //    eFDBHelp.Update(item);
            //}

           

            Console.WriteLine("请等待");
            Console.ReadLine();
         
        }
    }
}
