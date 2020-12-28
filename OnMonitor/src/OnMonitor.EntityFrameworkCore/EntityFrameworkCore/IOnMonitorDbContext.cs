using Microsoft.EntityFrameworkCore;
using OnMonitor.MenusInfos;
using OnMonitor.Monitor;
using OnMonitor.Monitor.Alarm;
using OnMonitor.MonitorRepair;
using OnMonitor.OrderMaterials;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace OnMonitor.EntityFrameworkCore
{
    [ConnectionStringName(OnMonitorDbProperties.ConnectionStringName)]
    public interface IOnMonitorDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<DVR> DVRs { get; set; }
        public DbSet<CameraRepair> CameraRepairs { get; set; }
        public DbSet<ProjectManages> ProjectManages { get; set; }
        public DbSet<DVRCheckInfo> DVRCheckInfos { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<MonitorRoom> MonitorRooms { get; set; }
        public DbSet<DVRChannelInfo> DVRChannelInfos { get; set; }
        public DbSet<SystemMenu> SystemMenus { get; set; }
        public DbSet<MaterialRepertory> MaterialRepertories { get; set; }
        public DbSet<ProcurementContent> ProcurementContents { get; set; }
        public DbSet<ProcurementDeltail> ProcurementDeltails { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<SaleContent> SaleContents { get; set; }
        public DbSet<SaleDeltail> SaleDeltails { get; set; }
    }
}