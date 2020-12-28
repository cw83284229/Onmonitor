using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlarmServer.Models
{
    public class AlarmTest
    {

        public string IP { get; set; }
        public string Prot { get; set; }
        public string Urse { get; set; }
        public string password { get; set; }


        public static  List<AlarmTest> GetAlarmList() 
        {
            List<AlarmTest> alarmslist = new List<AlarmTest>();

            AlarmTest alarmTest1 = new AlarmTest() {
                IP="172.30.116.63",
                Prot="37777",
                Urse="admin",
                password="admin123"   };
            AlarmTest alarmTest2 = new AlarmTest()
            {
                IP = "172.30.90.61",
                Prot = "37777",
                Urse = "admin",
                password = "admin123"
            };
            AlarmTest alarmTest3 = new AlarmTest()
            {
                IP = "172.30.52.60",
                Prot = "37777",
                Urse = "admin",
                password = "admin123"
            };
            AlarmTest alarmTest4 = new AlarmTest()
            {
                IP = "172.30.31.60",
                Prot = "37777",
                Urse = "admin",
                password = "admin123"
            };
            alarmslist.Add(alarmTest1);
            alarmslist.Add(alarmTest2);
            alarmslist.Add(alarmTest3);
            alarmslist.Add(alarmTest4);

            return alarmslist;
        }

    }

   

}
