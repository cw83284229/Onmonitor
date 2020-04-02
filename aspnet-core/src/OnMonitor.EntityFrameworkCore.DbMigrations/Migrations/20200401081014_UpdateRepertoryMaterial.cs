using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class UpdateRepertoryMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillMaterialId",
                table: "AppMaterialRepertories");

            migrationBuilder.DropColumn(
                name: "OrderMaterialId",
                table: "AppMaterialRepertories");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductInfoId",
                table: "AppMaterialRepertories",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "RepertoryLocation",
                table: "AppMaterialRepertories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductInfoId",
                table: "AppMaterialRepertories");

            migrationBuilder.DropColumn(
                name: "RepertoryLocation",
                table: "AppMaterialRepertories");

            migrationBuilder.AddColumn<Guid>(
                name: "BillMaterialId",
                table: "AppMaterialRepertories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderMaterialId",
                table: "AppMaterialRepertories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
