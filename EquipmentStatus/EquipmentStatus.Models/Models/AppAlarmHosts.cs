namespace EquipmentStatus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AppAlarmHosts
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Monitoring_room { get; set; }

        [StringLength(255)]
        public string AlarmHost_ID { get; set; }

        [StringLength(255)]
        public string AlarmHostType { get; set; }

        [StringLength(255)]
        public string AlarmHostIP { get; set; }

        public int? AlarmChannelCount { get; set; }

        [StringLength(255)]
        public string install_time { get; set; }

        [StringLength(255)]
        public string category { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string User { get; set; }
    }
}
