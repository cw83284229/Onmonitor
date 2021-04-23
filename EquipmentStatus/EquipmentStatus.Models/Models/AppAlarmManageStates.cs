namespace EquipmentStatus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AppAlarmManageStates
    {
        public int Id { get; set; }

        [StringLength(40)]
        public string ConcurrencyStamp { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastModificationTime { get; set; }

        public Guid? LastModifierId { get; set; }

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
        /// 现场处理人
        /// </summary>
        public string TreatmentMan { get; set; }
        /// <summary>
        /// 处理回复
        /// </summary>
        public string TreatmentReply { get; set; }
        /// <summary>
        /// 异常类别
        /// </summary>
        public string AnomalyType { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
