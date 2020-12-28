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

        public string ExtraProperties { get; set; }

        [StringLength(40)]
        public string ConcurrencyStamp { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastModificationTime { get; set; }

        public Guid? LastModifierId { get; set; }

        [StringLength(255)]
        public string Alarm_ID { get; set; }

        [StringLength(255)]
        public string AlarmTime { get; set; }

        [StringLength(255)]
        public string WithdrawTime { get; set; }

        [StringLength(255)]
        public string WithdrawMan { get; set; }

        [StringLength(255)]
        public string WithdrawRemark { get; set; }

        [StringLength(255)]
        public string DefenceTime { get; set; }

        public string TreatmentTime { get; set; }

        public string TreatmentTimeState { get; set; }

        public string TreatmentReply { get; set; }

        [StringLength(255)]
        public string AlarmHost_IP { get; set; }

        public int? Channel_ID { get; set; }

        public string Remark { get; set; }
    }
}
