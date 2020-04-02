using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class CreateMaterialmenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppMaterialRepertories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderMaterialId = table.Column<Guid>(nullable: false),
                    BillMaterialId = table.Column<Guid>(nullable: false),
                    Count = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMaterialRepertories", x => x.Id);
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
                name: "AppMaterialRepertories");

            migrationBuilder.DropTable(
                name: "AppSystemMenus");
        }
    }
}
