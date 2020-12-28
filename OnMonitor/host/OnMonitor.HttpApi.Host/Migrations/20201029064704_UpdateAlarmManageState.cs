using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class UpdateAlarmManageState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlarmHost_IP",
                table: "AppAlarmManageStates",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Channel_ID",
                table: "AppAlarmManageStates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "AppAlarmManageStates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlarmHost_IP",
                table: "AppAlarmManageStates");

            migrationBuilder.DropColumn(
                name: "Channel_ID",
                table: "AppAlarmManageStates");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "AppAlarmManageStates");
        }
    }
}
