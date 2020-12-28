namespace EquipmentStatus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AppDVRCheckInfos
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
        public string DVR_ID { get; set; }

        [StringLength(255)]
        public string DVR_SN { get; set; }

        [StringLength(255)]
        public string DVR_type { get; set; }

        public int? DVR_Channel { get; set; }

        public string DVRDISK { get; set; }

        public string DVRChannelInfo { get; set; }

        public bool? DVR_Online { get; set; }

        public int? DiskTotal { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public string DVRTime { get; set; }

        public bool? DiskChenk { get; set; }

        public bool? SNChenk { get; set; }

        public bool? TimeInfoChenk { get; set; }

        public bool? VideoCheck90Day { get; set; }
    }
}
