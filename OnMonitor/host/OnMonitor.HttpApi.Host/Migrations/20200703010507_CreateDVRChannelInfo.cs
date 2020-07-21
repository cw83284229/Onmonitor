using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class CreateDVRChannelInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDVRChannelInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DVR_ID = table.Column<string>(nullable: true),
                    channel_ID = table.Column<int>(nullable: false),
                    Camera_ID = table.Column<string>(nullable: true),
                    DataChannelName = table.Column<string>(nullable: true),
                    DVRChannelName = table.Column<string>(nullable: true),
                    ChannelNameCheck = table.Column<bool>(nullable: true),
                    ImageCheck = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDVRChannelInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDVRChannelInfos");
        }
    }
}
