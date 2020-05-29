using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace OnMonitor.Migrations
{
    public partial class CreateOrderMaterials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppMaterialRepertories");

            migrationBuilder.DropTable(
                name: "AppProcurementContents");

            migrationBuilder.DropTable(
                name: "AppProcurementDeltails");

            migrationBuilder.DropTable(
                name: "AppProductInfos");

            migrationBuilder.DropTable(
                name: "AppSaleContents");

            migrationBuilder.DropTable(
                name: "AppSaleDeltails");
        }
    }
}
