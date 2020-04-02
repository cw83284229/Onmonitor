using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class UpdateProductInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaterialManufacturer",
                table: "AppProductInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialSpecification",
                table: "AppProductInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialManufacturer",
                table: "AppProductInfos");

            migrationBuilder.DropColumn(
                name: "MaterialSpecification",
                table: "AppProductInfos");
        }
    }
}
