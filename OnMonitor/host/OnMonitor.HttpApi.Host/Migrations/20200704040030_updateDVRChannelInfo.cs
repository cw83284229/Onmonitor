using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class updateDVRChannelInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CameraType",
                table: "AppDVRChannelInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DVRChannelEncoding",
                table: "AppDVRChannelInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdateTime",
                table: "AppDVRChannelInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "AppDVRChannelInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CameraType",
                table: "AppDVRChannelInfos");

            migrationBuilder.DropColumn(
                name: "DVRChannelEncoding",
                table: "AppDVRChannelInfos");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "AppDVRChannelInfos");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "AppDVRChannelInfos");
        }
    }
}
