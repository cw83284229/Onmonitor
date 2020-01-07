using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class UpdateCamera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Monitoring_room",
                table: "AppCameras",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "RepairState",
                table: "AppCameraRepairs",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "NoSignal",
                table: "AppCameraRepairs",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Monitoring_room",
                table: "AppCameras");

            migrationBuilder.AlterColumn<bool>(
                name: "RepairState",
                table: "AppCameraRepairs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "NoSignal",
                table: "AppCameraRepairs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
