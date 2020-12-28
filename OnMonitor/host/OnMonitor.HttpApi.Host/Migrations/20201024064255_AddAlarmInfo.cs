using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class AddAlarmInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppSaleContents",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppProjectManages",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppProductInfos",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppProcurementContents",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppDVRs",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppDVRCheckInfos",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppCameras",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppCameraRepairs",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                name: "AppAlarmHosts",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 128, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monitoring_room = table.Column<string>(maxLength: 255, nullable: true),
                    AlarmHost_ID = table.Column<string>(maxLength: 255, nullable: true),
                    AlarmHostType = table.Column<string>(maxLength: 255, nullable: true),
                    AlarmHostIP = table.Column<string>(maxLength: 255, nullable: true),
                    AlarmChannelCount = table.Column<int>(maxLength: 255, nullable: true),
                    install_time = table.Column<string>(maxLength: 255, nullable: true),
                    category = table.Column<string>(maxLength: 255, nullable: true),
                    Remark = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAlarmHosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppAlarmManageStates",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 128, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Alarm_ID = table.Column<string>(maxLength: 255, nullable: true),
                    AlarmTime = table.Column<string>(maxLength: 255, nullable: true),
                    WithdrawTime = table.Column<string>(maxLength: 255, nullable: true),
                    WithdrawMan = table.Column<string>(maxLength: 255, nullable: true),
                    WithdrawRemark = table.Column<string>(maxLength: 255, nullable: true),
                    DefenceTime = table.Column<string>(maxLength: 255, nullable: true),
                    TreatmentTime = table.Column<string>(nullable: true),
                    TreatmentTimeState = table.Column<string>(nullable: true),
                    TreatmentReply = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAlarmManageStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppAlarmStatus",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 128, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alarm_ID = table.Column<string>(maxLength: 255, nullable: true),
                    IsAlarm = table.Column<bool>(nullable: true),
                    IsDefence = table.Column<bool>(nullable: true),
                    IsAnomaly = table.Column<bool>(nullable: false),
                    IsOpenDoor = table.Column<bool>(nullable: true),
                    TreatmentState = table.Column<int>(nullable: false),
                    BypassState = table.Column<int>(nullable: false),
                    LastModificationTime = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAlarmStatus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppAlarmHosts");

            migrationBuilder.DropTable(
                name: "AppAlarmManageStates");

            migrationBuilder.DropTable(
                name: "AppAlarmStatus");

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppSaleContents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppProjectManages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppProductInfos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppProcurementContents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppDVRs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppDVRCheckInfos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppCameras",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppCameraRepairs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppAlarms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);
        }
    }
}
