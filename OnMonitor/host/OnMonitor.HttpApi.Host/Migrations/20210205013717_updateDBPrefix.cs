using Microsoft.EntityFrameworkCore.Migrations;

namespace OnMonitor.Migrations
{
    public partial class updateDBPrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorSystemMenus",
                table: "OnMonitorSystemMenus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorSaleDeltails",
                table: "OnMonitorSaleDeltails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorSaleContents",
                table: "OnMonitorSaleContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorProjectManages",
                table: "OnMonitorProjectManages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorProductInfos",
                table: "OnMonitorProductInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorProcurementDeltails",
                table: "OnMonitorProcurementDeltails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorProcurementContents",
                table: "OnMonitorProcurementContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorMonitorRooms",
                table: "OnMonitorMonitorRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorMaterialRepertories",
                table: "OnMonitorMaterialRepertories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorDVRs",
                table: "OnMonitorDVRs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorDVRCheckInfos",
                table: "OnMonitorDVRCheckInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorDVRChannelInfos",
                table: "OnMonitorDVRChannelInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorCameras",
                table: "OnMonitorCameras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorCameraRepairs",
                table: "OnMonitorCameraRepairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorAlarmStatus",
                table: "OnMonitorAlarmStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorAlarms",
                table: "OnMonitorAlarms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorAlarmManageStates",
                table: "OnMonitorAlarmManageStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnMonitorAlarmHosts",
                table: "OnMonitorAlarmHosts");

            migrationBuilder.RenameTable(
                name: "OnMonitorSystemMenus",
                newName: "AppSystemMenus");

            migrationBuilder.RenameTable(
                name: "OnMonitorSaleDeltails",
                newName: "AppSaleDeltails");

            migrationBuilder.RenameTable(
                name: "OnMonitorSaleContents",
                newName: "AppSaleContents");

            migrationBuilder.RenameTable(
                name: "OnMonitorProjectManages",
                newName: "AppProjectManages");

            migrationBuilder.RenameTable(
                name: "OnMonitorProductInfos",
                newName: "AppProductInfos");

            migrationBuilder.RenameTable(
                name: "OnMonitorProcurementDeltails",
                newName: "AppProcurementDeltails");

            migrationBuilder.RenameTable(
                name: "OnMonitorProcurementContents",
                newName: "AppProcurementContents");

            migrationBuilder.RenameTable(
                name: "OnMonitorMonitorRooms",
                newName: "AppMonitorRooms");

            migrationBuilder.RenameTable(
                name: "OnMonitorMaterialRepertories",
                newName: "AppMaterialRepertories");

            migrationBuilder.RenameTable(
                name: "OnMonitorDVRs",
                newName: "AppDVRs");

            migrationBuilder.RenameTable(
                name: "OnMonitorDVRCheckInfos",
                newName: "AppDVRCheckInfos");

            migrationBuilder.RenameTable(
                name: "OnMonitorDVRChannelInfos",
                newName: "AppDVRChannelInfos");

            migrationBuilder.RenameTable(
                name: "OnMonitorCameras",
                newName: "AppCameras");

            migrationBuilder.RenameTable(
                name: "OnMonitorCameraRepairs",
                newName: "AppCameraRepairs");

            migrationBuilder.RenameTable(
                name: "OnMonitorAlarmStatus",
                newName: "AppAlarmStatus");

            migrationBuilder.RenameTable(
                name: "OnMonitorAlarms",
                newName: "AppAlarms");

            migrationBuilder.RenameTable(
                name: "OnMonitorAlarmManageStates",
                newName: "AppAlarmManageStates");

            migrationBuilder.RenameTable(
                name: "OnMonitorAlarmHosts",
                newName: "AppAlarmHosts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppSystemMenus",
                table: "AppSystemMenus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppSaleDeltails",
                table: "AppSaleDeltails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppSaleContents",
                table: "AppSaleContents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppProjectManages",
                table: "AppProjectManages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppProductInfos",
                table: "AppProductInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppProcurementDeltails",
                table: "AppProcurementDeltails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppProcurementContents",
                table: "AppProcurementContents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppMonitorRooms",
                table: "AppMonitorRooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppMaterialRepertories",
                table: "AppMaterialRepertories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppDVRs",
                table: "AppDVRs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppDVRCheckInfos",
                table: "AppDVRCheckInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppDVRChannelInfos",
                table: "AppDVRChannelInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppCameras",
                table: "AppCameras",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppCameraRepairs",
                table: "AppCameraRepairs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppAlarmStatus",
                table: "AppAlarmStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppAlarms",
                table: "AppAlarms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppAlarmManageStates",
                table: "AppAlarmManageStates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppAlarmHosts",
                table: "AppAlarmHosts",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppSystemMenus",
                table: "AppSystemMenus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppSaleDeltails",
                table: "AppSaleDeltails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppSaleContents",
                table: "AppSaleContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppProjectManages",
                table: "AppProjectManages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppProductInfos",
                table: "AppProductInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppProcurementDeltails",
                table: "AppProcurementDeltails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppProcurementContents",
                table: "AppProcurementContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppMonitorRooms",
                table: "AppMonitorRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppMaterialRepertories",
                table: "AppMaterialRepertories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppDVRs",
                table: "AppDVRs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppDVRCheckInfos",
                table: "AppDVRCheckInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppDVRChannelInfos",
                table: "AppDVRChannelInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppCameras",
                table: "AppCameras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppCameraRepairs",
                table: "AppCameraRepairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppAlarmStatus",
                table: "AppAlarmStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppAlarms",
                table: "AppAlarms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppAlarmManageStates",
                table: "AppAlarmManageStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppAlarmHosts",
                table: "AppAlarmHosts");

            migrationBuilder.RenameTable(
                name: "AppSystemMenus",
                newName: "OnMonitorSystemMenus");

            migrationBuilder.RenameTable(
                name: "AppSaleDeltails",
                newName: "OnMonitorSaleDeltails");

            migrationBuilder.RenameTable(
                name: "AppSaleContents",
                newName: "OnMonitorSaleContents");

            migrationBuilder.RenameTable(
                name: "AppProjectManages",
                newName: "OnMonitorProjectManages");

            migrationBuilder.RenameTable(
                name: "AppProductInfos",
                newName: "OnMonitorProductInfos");

            migrationBuilder.RenameTable(
                name: "AppProcurementDeltails",
                newName: "OnMonitorProcurementDeltails");

            migrationBuilder.RenameTable(
                name: "AppProcurementContents",
                newName: "OnMonitorProcurementContents");

            migrationBuilder.RenameTable(
                name: "AppMonitorRooms",
                newName: "OnMonitorMonitorRooms");

            migrationBuilder.RenameTable(
                name: "AppMaterialRepertories",
                newName: "OnMonitorMaterialRepertories");

            migrationBuilder.RenameTable(
                name: "AppDVRs",
                newName: "OnMonitorDVRs");

            migrationBuilder.RenameTable(
                name: "AppDVRCheckInfos",
                newName: "OnMonitorDVRCheckInfos");

            migrationBuilder.RenameTable(
                name: "AppDVRChannelInfos",
                newName: "OnMonitorDVRChannelInfos");

            migrationBuilder.RenameTable(
                name: "AppCameras",
                newName: "OnMonitorCameras");

            migrationBuilder.RenameTable(
                name: "AppCameraRepairs",
                newName: "OnMonitorCameraRepairs");

            migrationBuilder.RenameTable(
                name: "AppAlarmStatus",
                newName: "OnMonitorAlarmStatus");

            migrationBuilder.RenameTable(
                name: "AppAlarms",
                newName: "OnMonitorAlarms");

            migrationBuilder.RenameTable(
                name: "AppAlarmManageStates",
                newName: "OnMonitorAlarmManageStates");

            migrationBuilder.RenameTable(
                name: "AppAlarmHosts",
                newName: "OnMonitorAlarmHosts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorSystemMenus",
                table: "OnMonitorSystemMenus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorSaleDeltails",
                table: "OnMonitorSaleDeltails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorSaleContents",
                table: "OnMonitorSaleContents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorProjectManages",
                table: "OnMonitorProjectManages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorProductInfos",
                table: "OnMonitorProductInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorProcurementDeltails",
                table: "OnMonitorProcurementDeltails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorProcurementContents",
                table: "OnMonitorProcurementContents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorMonitorRooms",
                table: "OnMonitorMonitorRooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorMaterialRepertories",
                table: "OnMonitorMaterialRepertories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorDVRs",
                table: "OnMonitorDVRs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorDVRCheckInfos",
                table: "OnMonitorDVRCheckInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorDVRChannelInfos",
                table: "OnMonitorDVRChannelInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorCameras",
                table: "OnMonitorCameras",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorCameraRepairs",
                table: "OnMonitorCameraRepairs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorAlarmStatus",
                table: "OnMonitorAlarmStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorAlarms",
                table: "OnMonitorAlarms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorAlarmManageStates",
                table: "OnMonitorAlarmManageStates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnMonitorAlarmHosts",
                table: "OnMonitorAlarmHosts",
                column: "Id");
        }
    }
}
