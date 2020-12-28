using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class UpdateAlarmHost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AppAlarmHosts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "AppAlarmHosts",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AppAlarmHosts");

            migrationBuilder.DropColumn(
                name: "User",
                table: "AppAlarmHosts");
        }
    }
}
