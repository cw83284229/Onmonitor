using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class updatealarmtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlarmHostIP",
                table: "AppAlarmStatus",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Channel_ID",
                table: "AppAlarms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlarmHostIP",
                table: "AppAlarmStatus");

            migrationBuilder.DropColumn(
                name: "Channel_ID",
                table: "AppAlarms");
        }
    }
}
