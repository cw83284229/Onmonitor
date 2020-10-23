using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.Monitor
{
   public  class AlarmDto :AuditedEntityDto<Int32>
    { /// <summary>
      /// 监控室
      /// </summary>
        [StringLength(255)]
        public string Monitoring_room { get; set; }
        /// <summary>
        /// 报警主机号
        /// </summary>
        [StringLength(255)]
        public string AlarmHost_ID { get; set; }

        /// <summary>
        /// 报警主机编号
        /// </summary>
        [StringLength(255)]
        public string Alarm_ID { get; set; }
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
        /// 位置
        /// </summary>
        [StringLength(255)]
        public string Location { get; set; }
        /// <summary>
        /// 门岗类型
        /// </summary>
        [StringLength(255)]
        public string GeteType { get; set; }
        /// <summary>
        /// 传感器类型
        /// </summary>
        [StringLength(255)]
        public string SensorType { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        [StringLength(255)]
        public string department { get; set; }
        /// <summary>
        /// 费用代码
        /// </summary>
        [StringLength(255)]
        public string Cost_code { get; set; }
        /// <summary>
        /// 安装时间
        /// </summary>
        [StringLength(255)]
        public string install_time { get; set; }
        /// <summary>
        /// 安装厂商
        /// </summary>
        [StringLength(255)]
        public string category { get; set; }

        /// <summary>
        /// 镜头号
        /// </summary>
        [StringLength(255)]
        public string Camera_ID { get; set; }

        /// <summary>
        /// 有无报警器
        /// </summary>
        public bool? IsAlertor { get; set; }

        /// <summary>
        /// 是否开岗
        /// </summary>
        public bool? IsOpenOrClosed { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(255)]
        public string Remark { get; set; }
    }
}
