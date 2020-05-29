using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class UpdateDVRInfoCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelChenk",
                table: "AppDVRCheckInfos");

            migrationBuilder.DropColumn(
                name: "InfoChenk",
                table: "AppDVRCheckInfos");

            migrationBuilder.DropColumn(
                name: "LibraryChannelInfo",
                table: "AppDVRCheckInfos");

            migrationBuilder.AddColumn<string>(
                name: "DVRTime",
                table: "AppDVRCheckInfos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DiskChenk",
                table: "AppDVRCheckInfos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SNChenk",
                table: "AppDVRCheckInfos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TimeInfoChenk",
                table: "AppDVRCheckInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DVRTime",
                table: "AppDVRCheckInfos");

            migrationBuilder.DropColumn(
                name: "DiskChenk",
                table: "AppDVRCheckInfos");

            migrationBuilder.DropColumn(
                name: "SNChenk",
                table: "AppDVRCheckInfos");

            migrationBuilder.DropColumn(
                name: "TimeInfoChenk",
                table: "AppDVRCheckInfos");

            migrationBuilder.AddColumn<bool>(
                name: "ChannelChenk",
                table: "AppDVRCheckInfos",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InfoChenk",
                table: "AppDVRCheckInfos",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LibraryChannelInfo",
                table: "AppDVRCheckInfos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
