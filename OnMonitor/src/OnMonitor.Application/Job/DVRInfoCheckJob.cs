using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TimedTask.Host.Job
{
    public class DVRInfoCheckJob : BackgroundService
    {


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var msg = $"{DateTime.Now},testok";

                Console.WriteLine(msg);

                await Task.Delay(100000, stoppingToken);

            }

        
        }
    }
}
