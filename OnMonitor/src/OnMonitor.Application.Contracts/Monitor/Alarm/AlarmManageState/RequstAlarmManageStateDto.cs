using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace OnMonitor.Monitor.Alarm
{
   public class RequstAlarmManageStateDto : AuditedEntityDto<Int32>
    {
        /// <summary>
        /// 报警主机IP
        /// </summary>
        [StringLength(255)]
        public string AlarmHost_IP { get; set; }
        /// <summary>
        /// 报警编号
        /// </summary>
        [StringLength(255)]
        public string Alarm_ID { get; set; }
        /// <summary>
        /// 通道编号
        /// </summary>
        public int? Channel_ID { get; set; }
        /// <summary>
        /// 报警时间
        /// </summary>
        [StringLength(255)]
        public string AlarmTime { get; set; }
        /// <summary>
        /// 撤防时间
        /// </summary>
        [StringLength(255)]
        public string WithdrawTime { get; set; }
        /// <summary>
        /// 撤防人员
        /// </summary>
        [StringLength(255)]
        public string WithdrawMan { get; set; }
        /// <summary>
        /// 撤防原因
        /// </summary>
        [StringLength(255)]
        public string WithdrawRemark { get; set; }
        /// <summary>
        /// 布防时间
        /// </summary>
        [StringLength(255)]
        public string DefenceTime { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public string TreatmentTime { get; set; }
        /// <summary>
        /// 处理状态时间
        /// </summary>
        public string TreatmentTimeState { get; set; }
        /// <summary>
        /// 处理回复
        /// </summary>
        public string TreatmentReply { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }


        /// <summary>
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


    }
}
