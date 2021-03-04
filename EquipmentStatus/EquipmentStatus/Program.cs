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

            HttpHelper httpHelper = new HttpHelper();

          
            Console.WriteLine("请等待");
            Console.ReadLine();
         
        }
    }
}
