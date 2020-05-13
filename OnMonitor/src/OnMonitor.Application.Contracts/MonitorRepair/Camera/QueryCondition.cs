using System;
using System.Collections.Generic;
using System.Text;

namespace OnMonitor.MonitorRepair
{
  public  class QueryCondition
    {

        /// <summary>
        /// 监控室
        /// </summary>
        public string DVR_Room { get; set; }
        /// <summary>
        /// 主机号
        /// </summary>

        public string DVR_ID { get; set; }
    
        /// <summary>
        /// 楼栋
        /// </summary>
     
        public string Build { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
 
        public string floor { get; set; }
       
    
        /// <summary>
        /// 镜头号
        /// </summary>
   
        public string Camera_ID { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
      
        public string Location { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
   

        public string department { get; set; }
        /// <summary>
        /// 异常时间
        /// </summary>

        public string AnomalyTime { get; set; }
        /// <summary>
        /// 统计时间
        /// </summary>
        public string CollectTime { get; set; }
        /// <summary>
        /// 异常类别
        /// </summary>
      
        public string AnomalyType { get; set; }

        /// <summary>
        /// 异常等级
        /// </summary>
    
        public string AnomalyGrade { get; set; }
        /// <summary>
        /// 统计人员
        /// </summary>
      
        public string Registrar { get; set; }
        /// <summary>
        /// 修复状态
        /// </summary>

        public bool? RepairState { get; set; }
        /// <summary>
        /// 修复时间
        /// </summary>
    
        public string RepairedTime { get; set; }
        /// <summary>
        /// 维修人员
        /// </summary>
       
        public string Accendant { get; set; }
        /// <summary>
        /// 维修内容
        /// </summary>
     
        public string RepairDetails { get; set; }
        /// <summary>
        /// 厂商维修
        /// </summary>
       
        public string RepairFirm { get; set; }

        /// <summary>
        /// 监工确认
        /// </summary>
       
        public string Supervisor { get; set; }
        /// <summary>
        /// 更换配件
        /// </summary>
       
        public string ReplacePart { get; set; }
        /// <summary>
        /// 工程异常
        /// </summary>
        
        public string ProjectAnomaly { get; set; }

        /// <summary>
        /// 灰屏确认
        /// </summary>
        public bool? NoSignal { get; set; }
      



    }
}
