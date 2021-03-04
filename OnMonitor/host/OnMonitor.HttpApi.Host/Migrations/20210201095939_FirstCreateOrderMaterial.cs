using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class FirstCreateOrderMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OnMonitorMaterialRepertories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RepertoryLocation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnMonitorMaterialRepertories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OnMonitorProcurementContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcurementTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcurementMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsShipments = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnMonitorProcurementContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OnMonitorProcurementDeltails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcurementContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnMonitorProcurementDeltails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OnMonitorProductInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialsType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialsName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialSpecification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialManufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    units = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MateriralsPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarketPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnMonitorProductInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OnMonitorSaleContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleStore = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    SaleTime = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    ShipmentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsShipments = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnMonitorSaleContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OnMonitorSaleDeltails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnMonitorSaleDeltails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnMonitorMaterialRepertories");

            migrationBuilder.DropTable(
                name: "OnMonitorProcurementContents");

            migrationBuilder.DropTable(
                name: "OnMonitorProcurementDeltails");

            migrationBuilder.DropTable(
                name: "OnMonitorProductInfos");

            migrationBuilder.DropTable(
                name: "OnMonitorSaleContents");

            migrationBuilder.DropTable(
                name: "OnMonitorSaleDeltails");
        }
    }
}
