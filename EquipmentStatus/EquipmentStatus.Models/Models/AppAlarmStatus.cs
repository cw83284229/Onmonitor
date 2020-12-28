namespace EquipmentStatus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AppAlarmStatus
    {
        public int Id { get; set; }

        public string Alarm_ID { get; set; }

        public int? IsAlarm { get; set; }

        public int? IsDefence { get; set; }

        public int? IsAnomaly { get; set; }

        public bool? IsOpenDoor { get; set; }

        public int TreatmentState { get; set; }

        public int BypassState { get; set; }

        public string LastModificationTime { get; set; }

        public string Remark { get; set; }

        public int? Channel_ID { get; set; }

        [StringLength(255)]
        public string AlarmHostIP { get; set; }
    }
}
