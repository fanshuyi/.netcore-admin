using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class adddelete1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SysRoleSysControllerSysActions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "SysRoleSysControllerSysActions",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SysLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "SysLogs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SysDepartmentSysUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "SysDepartmentSysUsers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SysControllerSysActions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "SysControllerSysActions",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SysControllers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "SysControllers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SysAreas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "SysAreas",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "SysActions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "SysActions",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "DomainLabels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "DomainLabels",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "AspNetUserTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "AspNetUserTokens",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "AspNetUserRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "AspNetUserRoles",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "AspNetUserLogins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "AspNetUserLogins",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "AspNetUserClaims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "AspNetUserClaims",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "AspNetRoles",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "AspNetRoleClaims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteDateTime",
                table: "AspNetRoleClaims",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SysRoleSysControllerSysActions");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "SysRoleSysControllerSysActions");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SysLogs");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "SysLogs");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SysDepartmentSysUsers");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "SysDepartmentSysUsers");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SysControllerSysActions");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "SysControllerSysActions");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SysControllers");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "SysControllers");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SysAreas");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "SysAreas");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "SysActions");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "SysActions");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "DomainLabels");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "DomainLabels");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "AspNetRoleClaims");
        }
    }
}
