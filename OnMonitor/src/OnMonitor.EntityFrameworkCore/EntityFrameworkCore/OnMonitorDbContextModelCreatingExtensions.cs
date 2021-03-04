using System;
using Microsoft.EntityFrameworkCore;
using OnMonitor.MenusInfos;
using OnMonitor.Monitor;
using OnMonitor.Monitor.Alarm;
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
                b.ToTable(options.TablePrefix + "Cameras", options.Schema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Camera_ID).IsRequired().HasMaxLength(128);
                //...
            });
            builder.Entity<DVR>(b =>
            {
                b.ToTable(options.TablePrefix + "DVRs", options.Schema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.DVR_ID).IsRequired().HasMaxLength(128);
                //...
            });
            builder.Entity<CameraRepair>(b =>
            {
                b.ToTable(options.TablePrefix + "CameraRepairs", options.Schema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Id).IsRequired().HasMaxLength(128);
                //...
            });
            builder.Entity<ProjectManages>(b =>
            {
                b.ToTable(options.TablePrefix + "ProjectManages", options.Schema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Id).IsRequired().HasMaxLength(128);
            });
            builder.Entity<DVRCheckInfo>(b =>
            {
                b.ToTable(options.TablePrefix + "DVRCheckInfos", options.Schema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Id).IsRequired().HasMaxLength(128);
            });
            builder.Entity<Alarm>(b =>
            {
                b.ToTable(options.TablePrefix + "Alarms", options.Schema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Id).IsRequired().HasMaxLength(128);
            });
            builder.Entity<AlarmStatus>(b =>
            {
                b.ToTable(options.TablePrefix + "AlarmStatus", options.Schema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Id).IsRequired().HasMaxLength(128);
            });
            builder.Entity<AlarmManageState>(b =>
            {
                b.ToTable(options.TablePrefix + "AlarmManageStates", options.Schema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Id).IsRequired().HasMaxLength(128);
            });
            builder.Entity<AlarmHost>(b =>
            {
                b.ToTable(options.TablePrefix + "AlarmHosts", options.Schema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Id).IsRequired().HasMaxLength(128);
            });
            builder.Entity<MonitorRoom>(b =>
            {
                b.ToTable(options.TablePrefix + "MonitorRooms", options.Schema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });
            builder.Entity<DVRChannelInfo>(b =>
            {
                b.ToTable(options.TablePrefix + "DVRChannelInfos", options.Schema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });

            builder.Entity<SystemMenu>(b =>
            {
                b.ToTable(options.TablePrefix + "SystemMenus", options.Schema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });
            builder.Entity<MaterialRepertory>(b =>
            {
                b.ToTable(options.TablePrefix + "MaterialRepertories", options.Schema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });

            builder.Entity<ProcurementContent>(b =>
            {
                b.ToTable(options.TablePrefix + "ProcurementContents", options.Schema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });

            builder.Entity<ProcurementDeltail>(b =>
            {
                b.ToTable(options.TablePrefix + "ProcurementDeltails", options.Schema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });

            builder.Entity<ProductInfo>(b =>
            {
                b.ToTable(options.TablePrefix + "ProductInfos", options.Schema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });

            builder.Entity<SaleContent>(b =>
            {
                b.ToTable(options.TablePrefix + "SaleContents", options.Schema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });

            builder.Entity<SaleDeltail>(b =>
            {
                b.ToTable(options.TablePrefix + "SaleDeltails", options.Schema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });
        }
    }
}