using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnMonitor.Monitor
{
    [Route("api/ReportForms")]
    public class ReportFormsController : OnMonitorController
    {
        IReportFormsAppService _reportFormsAppService;
        public ReportFormsController(IReportFormsAppService reportFormsAppService)
        {

            _reportFormsAppService = reportFormsAppService;

        }
       

        #region 获取监控室分类
        /// <summary>
        /// 获取监控异常比例，按监控室分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AnomalyProportionByMonitorRoom")]
        public Dictionary<string, string> GetAnomalyProportionByMonitorRoom()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            var data = _reportFormsAppService.GetReportFormsByMonitorRoom();

            foreach (var item in data)
            {
                string anomalyProportion = item.AnomalyProportion.ToString("P2");
                dic.Add(item.DVRRoom, anomalyProportion);
            }
            return dic;
        }

        /// <summary>
        /// 获取监控镜头异常数量，按监控室分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AnomalyCountByMonitorRoom")]
        public Dictionary<string,int[]> GetAnomalyCountByMonitorRoom()
        {
            Dictionary<string, int[]> dic = new Dictionary<string, int[]>();

            var data = _reportFormsAppService.GetReportFormsByMonitorRoom();

            foreach (var item in data)
            {
                int[] vs = { item.CameraAnomaly, item.CameraTotal };
                dic.Add(item.DVRRoom, vs);
            }
            return dic;
        }
     
        #endregion

        #region 按楼栋分类

        /// <summary>
        /// 获取监控异常比例，按楼栋分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AnomalyProportionByBuild")]
        public Dictionary<string, string> GetAnomalyProportionByBuild()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            var data = _reportFormsAppService.GetReportFormsByBuild();

            foreach (var item in data)
            {
                if (item.CameraTotal!=0)
                {
                    string anomalyProportion = ((float)item.CameraAnomaly / item.CameraTotal).ToString("P2");
                    dic.Add(item.Camera_build, anomalyProportion);
                }
              
            }
            return dic;
        }

        /// <summary>
        /// 获取监控镜头异常数量，按楼栋分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AnomalyCountByBuild")]
        public Dictionary<string, int[]> GetAnomalyCountByBuild()
        {
            Dictionary<string, int[]> dic = new Dictionary<string, int[]>();

            var data = _reportFormsAppService.GetReportFormsByBuild();

            foreach (var item in data)
            {
                int[] vs = { item.CameraAnomaly, item.CameraTotal };
                dic.Add(item.Camera_build, vs);
            }
            return dic;
        }
      


        #endregion

        #region 按年份分类

        /// <summary>
        /// 获取监控异常比例，按年份分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AnomalyProportionByYearly")]
        public Dictionary<string, string> GetAnomalyProportionByYearly()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            var data = _reportFormsAppService.GetReportFormsByYear();

            foreach (var item in data)
            {
                string anomalyProportion = item.AnomalyProportion.ToString("P2");
                dic.Add(item.install_time, anomalyProportion);
            }
            return dic;
        }

        /// <summary>
        /// 获取监控镜头异常数量，按年份分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AnomalyCountByYearly")]
        public Dictionary<string, int[]> GetAnomalyCountByYearly()
        {
            Dictionary<string, int[]> dic = new Dictionary<string,int[]>();

            var data = _reportFormsAppService.GetReportFormsByYear();

            foreach (var item in data)
            {
                int[] vs = { item.CameraAnomaly, item.CameraTotal };
                dic.Add(item.install_time, vs);
            }
            return dic;
        }
    


        #endregion

        #region 按部门分类

        /// <summary>
        /// 获取监控异常比例，按部门分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AnomalyProportionByDepartment")]
        public Dictionary<string, string> GetAnomalyProportionByDepartment()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            var data = _reportFormsAppService.GetReportFormsBydepartment();

            foreach (var item in data)
            {
                if (item.department != null)
                {
                    string anomalyProportion = item.AnomalyProportion.ToString("P2");
                    dic.Add(item.department, anomalyProportion);
                }
            }
            return dic;
        }

        /// <summary>
        /// 获取监控镜头异常数量，按部门分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AnomalyCountByDepartment")]
        public Dictionary<string, int[]> GetAnomalyCountByDepartment()
        {
            Dictionary<string,int[]> dic = new Dictionary<string,int[]>();

            var data = _reportFormsAppService.GetReportFormsBydepartment();

            foreach (var item in data)
            {
                if (item.department!=null)
                {
                    int[] vs = { item.CameraAnomaly, item.CameraTotal };
                    dic.Add(item.department, vs);
                }
               
            }
            return dic;
        }
       
        #endregion

        #region 1级异常按监控室时间分类

        /// <summary>
        /// 获取监控1级异常比例，按监控室分类年度异常
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("1AnomalyProportionByMonitorRoomAndYearly")]
        public Dictionary<string, string> Get1AnomalyProportionByMonitorRoomAndYearly()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string StartTime = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss");
            string EndTime= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var data = _reportFormsAppService.GetReportFormsByMonitorRoomorAnomalyCondition(StartTime,EndTime,"1",null);

            foreach (var item in data)
            {
                string anomalyProportion = item.AnomalyProportion.ToString("P2");
                dic.Add(item.DVRRoom, anomalyProportion);
            }
            return dic;
        }
        /// <summary>
        /// 获取监控1级异常比例，按监控室分类月度异常
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("1AnomalyProportionByMonitorRoomAndMonths")]
        public Dictionary<string, string> Get1AnomalyProportionByMonitorRoomAndMonths()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string StartTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss");
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var data = _reportFormsAppService.GetReportFormsByMonitorRoomorAnomalyCondition(StartTime, EndTime, "1", null);

            foreach (var item in data)
            {
                string anomalyProportion = item.AnomalyProportion.ToString("P2");
                dic.Add(item.DVRRoom, anomalyProportion);
            }
            return dic;
        }
        /// <summary>
        /// 获取监控1级异常比例，按监控室分类每周异常
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("1AnomalyProportionByMonitorRoomAndWeeks")]
        public Dictionary<string, string> Get1AnomalyProportionByMonitorRoomAndWeeks()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string StartTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var data = _reportFormsAppService.GetReportFormsByMonitorRoomorAnomalyCondition(StartTime, EndTime, "1", null);

            foreach (var item in data)
            {
                string anomalyProportion = item.AnomalyProportion.ToString("P2");
                dic.Add(item.DVRRoom, anomalyProportion);
            }
            return dic;
        }

        #endregion

        #region 每日新增异常按时间分类


        #region 每周异常报表
        /// <summary>
        /// 获取监控异常比例，按每周异常
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AnomalyProportionByWeek")]
        public Dictionary<string, string> GetAnomalyProportionByWeek()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string StartTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var data = _reportFormsAppService.GetReportFormsByTime(StartTime, EndTime, null, null, null);

            foreach (var item in data)
            {

                if (item.CameraAnomaly != 0)
                {
                    float anomalyProportion = (float)item.CameraAnomaly / item.CameraTotal;
                    dic.Add(item.install_time, anomalyProportion.ToString("P2"));
                }

            }
            return dic;
        }
        /// <summary>
        /// 获取维修比例，按每周异常
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RepairProportionByWeek")]
        public Dictionary<string, string> GetRepairProportionByWeek()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string StartTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var data = _reportFormsAppService.GetReportFormsByTime(StartTime, EndTime, null, null, null);

            foreach (var item in data)
            {

                if (item.CameraAnomaly != 0)
                {
                    float RepairProportion = (float)item.RepairTotal / item.CameraAnomaly;
                    dic.Add(item.install_time, RepairProportion.ToString("P2"));
                }

            }
            return dic;
        }

        /// <summary>
        /// 获取监控数量，按每周异常
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AnomalyCountByWeek")]
        public Dictionary<string, int[]> GetAnomalyCountByWeek()
        {
            Dictionary<string, int[]> dic = new Dictionary<string, int[]>();
            string StartTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var data = _reportFormsAppService.GetReportFormsByTime(StartTime, EndTime, null, null, null);

            foreach (var item in data)
            {
                int[] vs = { item.CameraAnomalyRepair, item.CameraAnomaly };

                dic.Add(item.install_time, vs);

            }
            return dic;
        }

        #endregion

        #region 制定时间及监控室异常报表
        /// <summary>
        /// 获取监控异常比例，指定时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AnomalyProportionByTimes")]
        public Dictionary<string, string> GetAnomalyProportionByTimes(string StartTime, string EndTime, string AnomalyType, string MonitorRoom)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
          

            var data = _reportFormsAppService.GetReportFormsByTime(StartTime, EndTime, null, AnomalyType, MonitorRoom);

            foreach (var item in data)
            {

                if (item.CameraAnomaly != 0)
                {
                    float anomalyProportion = (float)item.CameraAnomaly / item.CameraTotal;
                    dic.Add(item.install_time, anomalyProportion.ToString("P2"));
                }

            }
            return dic;
        }
        /// <summary>
        /// 获取监控维修比例，指定时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RepairProportionByTimes")]
        public Dictionary<string, string> GetRepairProportionByTimes(string StartTime, string EndTime, string AnomalyType, string MonitorRoom)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();


            var data = _reportFormsAppService.GetReportFormsByTime(StartTime, EndTime, null, AnomalyType, MonitorRoom);

            foreach (var item in data)
            {

                if (item.CameraAnomaly != 0)
                {
                    float anomalyProportion = (float) item.RepairTotal/ item.CameraAnomaly;
                    dic.Add(item.install_time, anomalyProportion.ToString("P2"));
                }

            }
            return dic;
        }

        /// <summary>
        /// 获取监控数量，按自定义
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AnomalyCountByTimes")]
        public Dictionary<string, int[]> GetAnomalyCountByTimes(string StartTime, string EndTime, string AnomalyType, string MonitorRoom)
        {
            Dictionary<string, int[]> dic = new Dictionary<string, int[]>();
           
            var data = _reportFormsAppService.GetReportFormsByTime(StartTime, EndTime, null, AnomalyType, MonitorRoom);

            foreach (var item in data)
            {
                int[] vs = { item.CameraAnomalyRepair, item.CameraAnomaly };

                dic.Add(item.install_time, vs);

            }
            return dic;
        }
        #endregion

        #endregion



        #region DVR异常/在线状态信息

        /// <summary>
        /// 获取DVR异常比例，按监控室分类,返回DVR异常比例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DVRAnomalyProportionByMonitorRoom")]
        public Dictionary<string, string> GetDVRAnomalyProportionByMonitorRoom()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            var data = _reportFormsAppService.GetDVROnlineTotal();

            foreach (var item in data)
            {
                if (item.DVRTotal != 0)
                {
                    string anomalyProportion = ((float)item.DVRAnomaly / item.DVRTotal).ToString("P2");
                    dic.Add(item.DVRRoom, anomalyProportion);
                }

            }
            return dic;
        }

        /// <summary>
        /// 获取主机在线数量，按监控室分析 返回<监控室-在线数量-主机总数>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DVROnlineCountBycMonitorRoom")]
        public Dictionary<string, int[]> GetDVROnlineCountByMonitorRoom()
        {
            Dictionary<string, int[]> dic = new Dictionary<string, int[]>();

            var data = _reportFormsAppService.GetDVROnlineTotal();

            foreach (var item in data)
            {
                int[] vs = { item.DVROnLine, item.DVRTotal};
                dic.Add(item.DVRRoom, vs);
            }
            return dic;
        }



        #endregion





    }
}
