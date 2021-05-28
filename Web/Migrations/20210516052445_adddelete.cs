using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class adddelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "TaskCenters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "TaskCenters",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SysUserLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "SysUserLogs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SysMessageReceiveds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "SysMessageReceiveds",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SysMessageCenters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "SysMessageCenters",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SysHelps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "SysHelps",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SysHelpClasses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "SysHelpClasses",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SysDepartments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "SysDepartments",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "JsonDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "JsonDatas",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "JsonDataHistorys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "JsonDataHistorys",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "TaskCenters");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "TaskCenters");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SysUserLogs");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "SysUserLogs");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SysMessageReceiveds");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "SysMessageReceiveds");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SysMessageCenters");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "SysMessageCenters");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SysHelps");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "SysHelps");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SysHelpClasses");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "SysHelpClasses");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SysDepartments");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "SysDepartments");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "JsonDatas");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "JsonDatas");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "JsonDataHistorys");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "JsonDataHistorys");
        }
    }
}
