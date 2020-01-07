//using Quartz;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace TimedTaskWeb.Common
//{
   
//        public class MyJob : IJob
//        {
//            public Task Execute(IJobExecutionContext context)
//            {
//                return Task.Run(() =>
//                {
//                    LogUtil.Debug("执行MyJob");
//                });
//            }
//        }
//    public class QuartzUtil
//    {
//        private static ISchedulerFactory _schedulerFactory;
//        private static IScheduler _scheduler;

//        /// <summary>
//        /// 添加任务
//        /// </summary>
//        /// <param name="type">类</param>
//        /// <param name="jobKey">键</param>
//        /// <param name="trigger">触发器</param>
//        public static async Task Add(Type type, JobKey jobKey, ITrigger trigger = null)
//        {
//            Init();
//            _scheduler = await _schedulerFactory.GetScheduler();

//            await _scheduler.Start();

//            if (trigger == null)
//            {
//                trigger = TriggerBuilder.Create()
//                    .WithIdentity("april.trigger")
//                    .WithDescription("default")
//                    .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow().WithRepeatCount(-1))
//                    .Build();
//            }
//            var job = JobBuilder.Create(type)
//                .WithIdentity(jobKey)
//                .Build();

//            await _scheduler.ScheduleJob(job, trigger);
//        }
//        /// <summary>
//        /// 恢复任务
//        /// </summary>
//        /// <param name="jobKey">键</param>
//        public static async Task Resume(JobKey jobKey)
//        {
//            Init();
//            _scheduler = await _schedulerFactory.GetScheduler();
//            LogUtil.Debug($"恢复任务{jobKey.Group},{jobKey.Name}");
//            await _scheduler.ResumeJob(jobKey);
//        }
//        /// <summary>
//        /// 停止任务
//        /// </summary>
//        /// <param name="jobKey">键</param>
//        public static async Task Stop(JobKey jobKey)
//        {
//            Init();
//            _scheduler = await _schedulerFactory.GetScheduler();
//            LogUtil.Debug($"暂停任务{jobKey.Group},{jobKey.Name}");
//            await _scheduler.PauseJob(jobKey);
//        }
//        /// <summary>
//        /// 初始化
//        /// </summary>
//        private static void Init()
//        {
//            if (_schedulerFactory == null)
//            {
//                _schedulerFactory = AprilConfig.ServiceProvider.GetService<ISchedulerFactory>();
//            }
//        }
//    }
//}