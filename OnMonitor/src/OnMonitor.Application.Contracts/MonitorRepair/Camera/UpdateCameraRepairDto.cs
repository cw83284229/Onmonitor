using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnMonitor.MonitorRepair
{
    public class UpdateCameraRepairDto
    {
        /// <summary>
        /// 镜头号
        /// </summary>
        [StringLength(10)]
        public string Camera_ID { get; set; }
        /// <summary>
        /// 异常时间
        /// </summary>
        [StringLength(55)]
        public string AnomalyTime { get; set; }
        /// <summary>
        /// 统计时间
        /// </summary>
        [StringLength(55)]
        public string CollectTime { get; set; }
        /// <summary>
        /// 异常类别
        /// </summary>
        [StringLength(10)]
        public string AnomalyType { get; set; }

        /// <summary>
        /// 异常等级
        /// </summary>
        [StringLength(10)]
        public string AnomalyGrade { get; set; }
        /// <summary>
        /// 统计人员
        /// </summary>
        [StringLength(10)]
        public string Registrar { get; set; }
        /// <summary>
        /// 修复状态
        /// </summary>

        public bool? RepairState { get; set; }
        /// <summary>
        /// 修复时间
        /// </summary>
        [StringLength(55)]
        public string RepairedTime { get; set; }
        /// <summary>
        /// 维修人员
        /// </summary>
        [StringLength(55)]
        public string Accendant { get; set; }
        /// <summary>
        /// 维修内容
        /// </summary>
        [StringLength(55)]
        public string RepairDetails { get; set; }
        /// <summary>
        /// 厂商维修
        /// </summary>
        [StringLength(55)]
        public string RepairFirm { get; set; }

        /// <summary>
        /// 监工确认
        /// </summary>
        [StringLength(55)]
        public string Supervisor { get; set; }
        /// <summary>
        /// 更换配件
        /// </summary>
        [StringLength(55)]
        public string ReplacePart { get; set; }
        /// <summary>
        /// 工程异常
        /// </summary>
        [StringLength(55)]
        public string ProjectAnomaly { get; set; }

        /// <summary>
        /// 灰屏确认
        /// </summary>
        public bool? NoSignal { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(255)]
        public string Remark { get; set; }
    }


    }

