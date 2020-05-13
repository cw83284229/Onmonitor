using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.MonitorRepair
{
    public class DVRCameraRepairDto: AuditedEntityDto<Int32>
    {



        /// <summary>
        /// 厂区
        /// </summary>

        public string Factory { get; set; }

        /// <summary>
        /// 监控室
        /// </summary>
        [StringLength(255)]
        public string DVR_Room { get; set; }
        /// <summary>
        /// 主机号
        /// </summary>
        [StringLength(255)]
        public string DVR_ID { get; set; }
        /// <summary>
        /// 通道号
        /// </summary>
        public int channel_ID { get; set; }
        /// <summary>
        /// 楼栋
        /// </summary>
        [StringLength(255)]
        public string Build { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        [StringLength(255)]
        public string floor { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        [StringLength(255)]
        public string Direction { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [StringLength(255)]
        public string Location { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [StringLength(255)]

        public string department { get; set; }

        /// <summary>
        /// 镜头类型
        /// </summary>
        [StringLength(255)]
        public string Camera_Tpye { get; set; }
        /// <summary>
        /// 镜头号
        /// </summary>
        [StringLength(10)]

        public string Camera_ID { get; set; }
        /// <summary>
        /// 安装时间
        /// </summary>
        [StringLength(255)]

        public string install_time { get; set; }
        /// <summary>
        /// 设备厂商
        /// </summary>

        public string manufacturer { get; set; }
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

      

    }

}

