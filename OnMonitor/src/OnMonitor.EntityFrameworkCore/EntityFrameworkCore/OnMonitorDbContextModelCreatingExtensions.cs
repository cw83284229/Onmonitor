using System;
using Microsoft.EntityFrameworkCore;
using OnMonitor.MenusInfos;
using OnMonitor.Monitor;
using OnMonitor.MonitorRepair;
using OnMonitor.OrderMaterials;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace OnMonitor.EntityFrameworkCore
{
    public static class OnMonitorDbContextModelCreatingExtensions
    {
        public static void ConfigureOnMonitor(
            this ModelBuilder builder,
            Action<OnMonitorModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new OnMonitorModelBuilderConfigurationOptions(
                OnMonitorDbProperties.DbTablePrefix,
                OnMonitorDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */
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
            builder.Entity<MonitorRoom>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "MonitorRooms", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });

            builder.Entity<SystemMenu>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "SystemMenus", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });
            builder.Entity<MaterialRepertory>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "MaterialRepertories", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });

            builder.Entity<ProcurementContent>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "ProcurementContents", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });

            builder.Entity<ProcurementDeltail>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "ProcurementDeltails", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });

            builder.Entity<ProductInfo>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "ProductInfos", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });

            builder.Entity<SaleContent>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "SaleContents", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });

            builder.Entity<SaleDeltail>(b =>
            {
                b.ToTable(OnMonitorConsts.DbTablePrefix + "SaleDeltails", OnMonitorConsts.DbSchema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });


        }
    }
}