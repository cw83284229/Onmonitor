namespace EquipmentStatus.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using EquipmentStatus.Models.Models;

    public partial class EFContext : DbContext
    {
        public EFContext()
            : base("name=EFDBConntext")
        {
        }

        public virtual DbSet<AppAlarmHosts> AppAlarmHosts { get; set; }
        public virtual DbSet<AppAlarmManageStates> AppAlarmManageStates { get; set; }
        public virtual DbSet<AppAlarms> AppAlarms { get; set; }
        public virtual DbSet<AppAlarmStatus> AppAlarmStatus { get; set; }
        public virtual DbSet<AppDVRCheckInfos> AppDVRCheckInfos { get; set; }
        public virtual DbSet<AppDVRs> AppDVRs { get; set; }
        public virtual DbSet<AppCameras> AppCameras { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
