using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace OnMonitor.Monitor
{
    
    public partial class AlarmStatus : Entity<Int32>
    {

        /// <summary>
        /// 报警编号
        /// </summary>
        [StringLength(255)]
        public string Alarm_ID { get; set; }
        /// <summary>
        /// 报警状态
        /// </summary>
        public bool?  IsAlarm { get; set; }
        /// <summary>
        /// 布防状态
        /// </summary>
        public bool? IsDefence { get; set; }
        /// <summary>
        /// 异常状态
        /// </summary>
        public bool  IsAnomaly { get; set; }
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
        /// 坠毁修改时间
        /// </summary>
        public string LastModificationTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(255)]
        public string Remark { get; set; }

    }
}
