using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentStatus.Models
{
    class Program
    {
        static void Main(string[] args)
        {

            EFContext context = new EFContext();

        var data=    context.AppAlarmHosts.Where(u=>u.Id>0).ToList();



        }
    }
}
