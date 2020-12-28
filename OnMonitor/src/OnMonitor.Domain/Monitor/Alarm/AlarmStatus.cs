using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace OnMonitor.Monitor.Alarm
{
    
    public partial class AlarmStatus : Entity<Int32>
    {

        /// <summary>
        /// 主机IP
        /// </summary>
        [StringLength(255)]
        public string AlarmHostIP { get; set; }
        /// <summary>
        /// 报警编号
        /// </summary>
        public string Alarm_ID { get; set; }
        /// <summary>
        /// 通道编号
        /// </summary>
        public int? Channel_ID { get; set; }
        /// <summary>
        /// 报警状态
        /// </summary>
        public int? IsAlarm { get; set; }
        /// <summary>
        /// 布防状态 0表示未知 1：表示布防 2：表示撤防
        /// </summary>
        public int? IsDefence { get; set; }
        /// <summary>
        /// 异常状态 0:未分配1:离线2:在线"
        /// </summary>
        public int? IsAnomaly { get; set; }
        /// <summary>
        /// 是否开岗
        /// </summary>
        public bool? IsOpenDoor { get; set; }
        /// <summary>
        /// 处理状态 1.表示处理中，0表示已处理，2表示异常上报，3，异常超时
        /// </summary>
        public int TreatmentState { get; set; }
        /// <summary>
        /// 旁路状态 1.正常，2。旁路，3.隔离
        /// </summary>
        public int BypassState { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public string LastModificationTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>

        public string Remark { get; set; }

    }
}
