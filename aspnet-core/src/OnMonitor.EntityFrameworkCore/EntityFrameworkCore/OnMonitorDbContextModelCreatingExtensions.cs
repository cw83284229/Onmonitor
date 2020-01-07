using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnMonitor;
using OnMonitor.Monitor;
using OnMonitor.MonitorRepair;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Users;

namespace OnMonitor.EntityFrameworkCore
{
    public static class OnMonitorDbContextModelCreatingExtensions
    {
        public static void ConfigureOnMonitor(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */
            //配置实体映射
            builder.Entity<Camera>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "Cameras", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Camera_ID).IsRequired().HasMaxLength(128);
                //...
            });
            builder.Entity<DVR>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "DVRs", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.DVR_ID).IsRequired().HasMaxLength(128);
                //...
            });
            builder.Entity<CameraRepair>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "CameraRepairs", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Id).IsRequired().HasMaxLength(128);
                //...
            });
            builder.Entity<ProjectManages>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "ProjectManages", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Id).IsRequired().HasMaxLength(128);
            });
            builder.Entity<DVRCheckInfo>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "DVRCheckInfos", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Id).IsRequired().HasMaxLength(128);
            });
            builder.Entity<Alarm>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "Alarms", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Id).IsRequired().HasMaxLength(128);
            });
        }

        public static void ConfigureCustomUserProperties<TUser>(this EntityTypeBuilder<TUser> b)
            where TUser: class, IUser
        {
            //b.Property<string>(nameof(AppUser.MyProperty))...
        }
    }
}