using Microsoft.Extensions.Hosting;
using OnMonitor.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TimedTask.Host.Job
{
    public class DVRInfoCheckJob : BackgroundService
    {
        //DVRInfoCheckService _DVRInfoCheckService;
        //public DVRInfoCheckJob(DVRInfoCheckService DVRInfoCheckService) 
        //{
        //    _DVRInfoCheckService = DVRInfoCheckService;
        //}


      
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var msg = $"{DateTime.Now},testok";

                

                //    var data = _DVRInfoCheckService.GetDVRInfoCheck();

                  //  Console.WriteLine(data);

                    await Task.Delay(800000, stoppingToken);

                }
            }
            catch (Exception)
            {

               
            }
            
          

        
        }
    }
}
