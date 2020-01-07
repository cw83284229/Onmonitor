using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class CreateAlarmCheckDVRinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppAlarms",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 128, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Monitoring_room = table.Column<string>(maxLength: 255, nullable: true),
                    AlarmHost_ID = table.Column<string>(maxLength: 255, nullable: true),
                    Alarm_ID = table.Column<string>(maxLength: 255, nullable: true),
                    Build = table.Column<string>(maxLength: 255, nullable: true),
                    floor = table.Column<string>(maxLength: 255, nullable: true),
                    Location = table.Column<string>(maxLength: 255, nullable: true),
                    GeteType = table.Column<string>(maxLength: 255, nullable: true),
                    SensorType = table.Column<string>(maxLength: 255, nullable: true),
                    department = table.Column<string>(maxLength: 255, nullable: true),
                    Cost_code = table.Column<string>(maxLength: 255, nullable: true),
                    install_time = table.Column<string>(maxLength: 255, nullable: true),
                    category = table.Column<string>(maxLength: 255, nullable: true),
                    Camera_ID = table.Column<string>(maxLength: 255, nullable: true),
                    IsAlertor = table.Column<bool>(nullable: true),
                    IsOpenOrClosed = table.Column<bool>(nullable: true),
                    Remark = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAlarms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppDVRCheckInfos",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 128, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    DVR_ID = table.Column<string>(maxLength: 255, nullable: true),
                    DVR_SN = table.Column<string>(maxLength: 255, nullable: true),
                    DVR_type = table.Column<string>(maxLength: 255, nullable: true),
                    DVR_Channel = table.Column<int>(nullable: true),
                    DVRDISK = table.Column<string>(nullable: true),
                    DVRChannelInfo = table.Column<string>(nullable: true),
                    LibraryChannelInfo = table.Column<string>(nullable: true),
                    DVR_Online = table.Column<bool>(nullable: true),
                    InfoChenk = table.Column<bool>(nullable: true),
                    DiskTotal = table.Column<int>(nullable: true),
                    ChannelChenk = table.Column<bool>(nullable: true),
                    Remark = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDVRCheckInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppAlarms");

            migrationBuilder.DropTable(
                name: "AppDVRCheckInfos");
        }
    }
}
