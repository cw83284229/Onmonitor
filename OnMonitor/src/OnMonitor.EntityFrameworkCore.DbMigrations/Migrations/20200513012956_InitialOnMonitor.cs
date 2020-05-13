using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class InitialOnMonitor : Migration
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
                    RepairState = table.Column<bool>(nullable: true),
                    RepairedTime = table.Column<string>(maxLength: 55, nullable: true),
                    Accendant = table.Column<string>(maxLength: 55, nullable: true),
                    RepairDetails = table.Column<string>(maxLength: 55, nullable: true),
                    RepairFirm = table.Column<string>(maxLength: 55, nullable: true),
                    Supervisor = table.Column<string>(maxLength: 55, nullable: true),
                    ReplacePart = table.Column<string>(maxLength: 55, nullable: true),
                    ProjectAnomaly = table.Column<string>(maxLength: 55, nullable: true),
                    NoSignal = table.Column<bool>(nullable: true),
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
                    Monitoring_room = table.Column<string>(maxLength: 255, nullable: true),
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
                name: "AppMaterialRepertories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductInfoId = table.Column<Guid>(nullable: false),
                    Count = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    RepertoryLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMaterialRepertories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppProcurementContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    ProcurementTime = table.Column<string>(nullable: true),
                    ProcurementMethod = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    IsShipments = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProcurementContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppProcurementDeltails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcurementContentId = table.Column<Guid>(nullable: false),
                    ProductInfoId = table.Column<Guid>(nullable: false),
                    Count = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProcurementDeltails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppProductInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    MaterialsNumber = table.Column<string>(nullable: true),
                    MaterialsType = table.Column<string>(nullable: true),
                    MaterialsName = table.Column<string>(nullable: true),
                    MaterialSpecification = table.Column<string>(nullable: true),
                    MaterialManufacturer = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    units = table.Column<string>(nullable: true),
                    MateriralsPrice = table.Column<decimal>(nullable: false),
                    MarketPrice = table.Column<decimal>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductInfos", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "AppSaleContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    SaleStore = table.Column<string>(nullable: true),
                    SaleTime = table.Column<string>(nullable: true),
                    ShipmentMethod = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    IsShipments = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSaleContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSaleDeltails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleContentId = table.Column<Guid>(nullable: false),
                    ProductInfoId = table.Column<Guid>(nullable: false),
                    Count = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSaleDeltails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSystemMenus",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pid = table.Column<long>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true),
                    href = table.Column<string>(nullable: true),
                    target = table.Column<string>(nullable: true),
                    sort = table.Column<int>(nullable: false),
                    status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSystemMenus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppAlarms");

            migrationBuilder.DropTable(
                name: "AppCameraRepairs");

            migrationBuilder.DropTable(
                name: "AppCameras");

            migrationBuilder.DropTable(
                name: "AppDVRCheckInfos");

            migrationBuilder.DropTable(
                name: "AppDVRs");

            migrationBuilder.DropTable(
                name: "AppMaterialRepertories");

            migrationBuilder.DropTable(
                name: "AppProcurementContents");

            migrationBuilder.DropTable(
                name: "AppProcurementDeltails");

            migrationBuilder.DropTable(
                name: "AppProductInfos");

            migrationBuilder.DropTable(
                name: "AppProjectManages");

            migrationBuilder.DropTable(
                name: "AppSaleContents");

            migrationBuilder.DropTable(
                name: "AppSaleDeltails");

            migrationBuilder.DropTable(
                name: "AppSystemMenus");
        }
    }
}
