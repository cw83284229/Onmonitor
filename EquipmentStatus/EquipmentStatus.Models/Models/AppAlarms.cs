namespace EquipmentStatus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AppAlarms
    {
        public int Id { get; set; }

        public string ExtraProperties { get; set; }

        public string ConcurrencyStamp { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastModificationTime { get; set; }

        public Guid? LastModifierId { get; set; }

        [StringLength(255)]
        public string Monitoring_room { get; set; }

        [StringLength(255)]
        public string AlarmHost_ID { get; set; }

        [StringLength(255)]
        public string Alarm_ID { get; set; }

        [StringLength(255)]
        public string Build { get; set; }

        [StringLength(255)]
        public string floor { get; set; }

        [StringLength(255)]
        public string Location { get; set; }

        [StringLength(255)]
        public string GeteType { get; set; }

        [StringLength(255)]
        public string SensorType { get; set; }

        [StringLength(255)]
        public string department { get; set; }

        [StringLength(255)]
        public string Cost_code { get; set; }

        [StringLength(255)]
        public string install_time { get; set; }

        [StringLength(255)]
        public string category { get; set; }

        [StringLength(255)]
        public string Camera_ID { get; set; }

        public bool? IsAlertor { get; set; }

        public bool? IsOpenOrClosed { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public int? Channel_ID { get; set; }
    }
}
