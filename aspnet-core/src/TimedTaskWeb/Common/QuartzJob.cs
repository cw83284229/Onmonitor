//using Quartz;
//using Quartz.Impl;
//using System;

//public class QuartzJob
//{
//    public static async void CreateJob(string name, string group, string cron)
//    {
//        ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
//        IScheduler scheduler = await schedulerFactory.GetScheduler();

//        DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(DateTime.Now, 1);
//        DateTimeOffset endTime = DateBuilder.NextGivenMinuteDate(DateTime.Now, 10);

//        IJobDetail job = JobBuilder.Create<QuartzTest>()
//                         .WithIdentity(name, group)
//                         .Build();

//        ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
//                                   .StartAt(startTime)
//                                   .EndAt(endTime)
//                                   .WithIdentity(name, group)
//                                   .WithCronSchedule(cron)
//                                   .Build();

//        await scheduler.ScheduleJob(job, cronTrigger);
//        await scheduler.Start();
//    }
//}
