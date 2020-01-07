using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class UpdateAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCameraRepairs",
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
                    Camera_ID = table.Column<string>(maxLength: 10, nullable: true),
                    AnomalyTime = table.Column<string>(maxLength: 55, nullable: true),
                    CollectTime = table.Column<string>(maxLength: 55, nullable: true),
                    AnomalyType = table.Column<string>(maxLength: 10, nullable: true),
                    AnomalyGrade = table.Column<string>(maxLength: 10, nullable: true),
                    Registrar = table.Column<string>(maxLength: 10, nullable: true),
                    RepairState = table.Column<bool>(nullable: false),
                    RepairedTime = table.Column<string>(maxLength: 55, nullable: true),
                    Accendant = table.Column<string>(maxLength: 55, nullable: true),
                    RepairDetails = table.Column<string>(maxLength: 55, nullable: true),
                    RepairFirm = table.Column<string>(maxLength: 55, nullable: true),
                    Supervisor = table.Column<string>(maxLength: 55, nullable: true),
                    ReplacePart = table.Column<string>(maxLength: 55, nullable: true),
                    ProjectAnomaly = table.Column<string>(maxLength: 55, nullable: true),
                    NoSignal = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCameraRepairs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppCameras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    DVR_ID = table.Column<string>(maxLength: 255, nullable: true),
                    channel_ID = table.Column<int>(nullable: false),
                    Camera_ID = table.Column<string>(maxLength: 128, nullable: false),
                    Build = table.Column<string>(maxLength: 255, nullable: true),
                    floor = table.Column<string>(maxLength: 255, nullable: true),
                    Direction = table.Column<string>(maxLength: 255, nullable: true),
                    Location = table.Column<string>(maxLength: 255, nullable: true),
                    MonitorClassification = table.Column<string>(maxLength: 255, nullable: true),
                    Camera_Tpye = table.Column<string>(maxLength: 255, nullable: true),
                    department = table.Column<string>(maxLength: 255, nullable: true),
                    Cost_code = table.Column<string>(maxLength: 255, nullable: true),
                    install_time = table.Column<string>(maxLength: 255, nullable: true),
                    manufacturer = table.Column<string>(nullable: true),
                    category = table.Column<string>(maxLength: 255, nullable: true),
                    Alarm_ID = table.Column<string>(maxLength: 255, nullable: true),
                    Remark = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCameras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppDVRs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Factory = table.Column<string>(maxLength: 255, nullable: true),
                    Monitoring_room = table.Column<string>(maxLength: 255, nullable: true),
                    Camera_build = table.Column<string>(maxLength: 255, nullable: true),
                    Camera_foor = table.Column<string>(maxLength: 255, nullable: true),
                    DVR_ID = table.Column<string>(maxLength: 128, nullable: false),
                    Home_server = table.Column<string>(maxLength: 255, nullable: true),
                    Hard_drive = table.Column<int>(nullable: true),
                    DVR_IP = table.Column<string>(maxLength: 255, nullable: true),
                    DVR_port = table.Column<string>(maxLength: 255, nullable: true),
                    DVR_usre = table.Column<string>(maxLength: 255, nullable: true),
                    DVR_possword = table.Column<string>(maxLength: 255, nullable: true),
                    install_time = table.Column<string>(maxLength: 255, nullable: true),
                    Manufacturer = table.Column<string>(maxLength: 255, nullable: true),
                    DVR_type = table.Column<string>(maxLength: 255, nullable: true),
                    DVR_SN = table.Column<string>(maxLength: 255, nullable: true),
                    DVR_Channel = table.Column<int>(nullable: true),
                    department = table.Column<string>(maxLength: 255, nullable: true),
                    Cost_code = table.Column<string>(maxLength: 255, nullable: true),
                    Remark = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDVRs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppProjectManages",
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
                    ProjectManageType = table.Column<string>(maxLength: 55, nullable: true),
                    ProjectName = table.Column<string>(maxLength: 55, nullable: true),
                    ProjectOrder = table.Column<string>(maxLength: 55, nullable: true),
                    StartWorkDate = table.Column<string>(maxLength: 55, nullable: true),
                    CompleteDate = table.Column<string>(maxLength: 55, nullable: true),
                    AcceptanceData = table.Column<string>(maxLength: 55, nullable: true),
                    ManufacturerName = table.Column<string>(maxLength: 55, nullable: true),
                    ProjectSpecifications = table.Column<string>(maxLength: 255, nullable: true),
                    Build = table.Column<string>(maxLength: 255, nullable: true),
                    Floor = table.Column<string>(maxLength: 255, nullable: true),
                    Camera_ID = table.Column<string>(maxLength: 255, nullable: true),
                    AcceptanceResult = table.Column<string>(maxLength: 255, nullable: true),
                    Remark = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectManages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCameraRepairs");

            migrationBuilder.DropTable(
                name: "AppCameras");

            migrationBuilder.DropTable(
                name: "AppDVRs");

            migrationBuilder.DropTable(
                name: "AppProjectManages");
        }
    }
}
