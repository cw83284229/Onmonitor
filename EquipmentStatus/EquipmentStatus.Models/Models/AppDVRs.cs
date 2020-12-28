namespace EquipmentStatus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AppDVRs
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
        public string Factory { get; set; }

        [StringLength(255)]
        public string Monitoring_room { get; set; }

        [StringLength(255)]
        public string Camera_build { get; set; }

        [StringLength(255)]
        public string Camera_foor { get; set; }

        [Required]
        [StringLength(128)]
        public string DVR_ID { get; set; }

        [StringLength(255)]
        public string Home_server { get; set; }

        public int? Hard_drive { get; set; }

        [StringLength(255)]
        public string DVR_IP { get; set; }

        [StringLength(255)]
        public string DVR_port { get; set; }

        [StringLength(255)]
        public string DVR_usre { get; set; }

        [StringLength(255)]
        public string DVR_possword { get; set; }

        [StringLength(255)]
        public string install_time { get; set; }

        [StringLength(255)]
        public string Manufacturer { get; set; }

        [StringLength(255)]
        public string DVR_type { get; set; }

        [StringLength(255)]
        public string DVR_SN { get; set; }

        public int? DVR_Channel { get; set; }

        [StringLength(255)]
        public string department { get; set; }

        [StringLength(255)]
        public string Cost_code { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }
    }
}
