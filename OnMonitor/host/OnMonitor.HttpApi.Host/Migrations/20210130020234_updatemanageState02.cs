using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class updatemanageState02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnomalyType",
                table: "AppAlarmManageStates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TreatmentMan",
                table: "AppAlarmManageStates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnomalyType",
                table: "AppAlarmManageStates");

            migrationBuilder.DropColumn(
                name: "TreatmentMan",
                table: "AppAlarmManageStates");
        }
    }
}
