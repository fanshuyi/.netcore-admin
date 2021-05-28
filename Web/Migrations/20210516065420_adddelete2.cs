using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class adddelete2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "TaskCenters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "TaskCenters",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SysUserLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "SysUserLogs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SysMessageReceiveds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "SysMessageReceiveds",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SysMessageCenters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "SysMessageCenters",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SysHelps",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "SysHelps",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SysHelpClasses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "SysHelpClasses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SysDepartments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "SysDepartments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "JsonDatas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "JsonDatas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "JsonDataHistorys",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "JsonDataHistorys",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskCenters_DeleteBy",
                table: "TaskCenters",
                column: "DeleteBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserLogs_DeleteBy",
                table: "SysUserLogs",
                column: "DeleteBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysMessageReceiveds_DeleteBy",
                table: "SysMessageReceiveds",
                column: "DeleteBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysMessageCenters_DeleteBy",
                table: "SysMessageCenters",
                column: "DeleteBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelps_DeleteBy",
                table: "SysHelps",
                column: "DeleteBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelpClasses_DeleteBy",
                table: "SysHelpClasses",
                column: "DeleteBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysDepartments_DeleteBy",
                table: "SysDepartments",
                column: "DeleteBy");

            migrationBuilder.CreateIndex(
                name: "IX_JsonDatas_DeleteBy",
                table: "JsonDatas",
                column: "DeleteBy");

            migrationBuilder.CreateIndex(
                name: "IX_JsonDataHistorys_DeleteBy",
                table: "JsonDataHistorys",
                column: "DeleteBy");

            migrationBuilder.AddForeignKey(
                name: "FK_JsonDataHistorys_AspNetUsers_DeleteBy",
                table: "JsonDataHistorys",
                column: "DeleteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JsonDatas_AspNetUsers_DeleteBy",
                table: "JsonDatas",
                column: "DeleteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysDepartments_AspNetUsers_DeleteBy",
                table: "SysDepartments",
                column: "DeleteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysHelpClasses_AspNetUsers_DeleteBy",
                table: "SysHelpClasses",
                column: "DeleteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysHelps_AspNetUsers_DeleteBy",
                table: "SysHelps",
                column: "DeleteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysMessageCenters_AspNetUsers_DeleteBy",
                table: "SysMessageCenters",
                column: "DeleteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysMessageReceiveds_AspNetUsers_DeleteBy",
                table: "SysMessageReceiveds",
                column: "DeleteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysUserLogs_AspNetUsers_DeleteBy",
                table: "SysUserLogs",
                column: "DeleteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCenters_AspNetUsers_DeleteBy",
                table: "TaskCenters",
                column: "DeleteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JsonDataHistorys_AspNetUsers_DeleteBy",
                table: "JsonDataHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_JsonDatas_AspNetUsers_DeleteBy",
                table: "JsonDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_SysDepartments_AspNetUsers_DeleteBy",
                table: "SysDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_SysHelpClasses_AspNetUsers_DeleteBy",
                table: "SysHelpClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_SysHelps_AspNetUsers_DeleteBy",
                table: "SysHelps");

            migrationBuilder.DropForeignKey(
                name: "FK_SysMessageCenters_AspNetUsers_DeleteBy",
                table: "SysMessageCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_SysMessageReceiveds_AspNetUsers_DeleteBy",
                table: "SysMessageReceiveds");

            migrationBuilder.DropForeignKey(
                name: "FK_SysUserLogs_AspNetUsers_DeleteBy",
                table: "SysUserLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskCenters_AspNetUsers_DeleteBy",
                table: "TaskCenters");

            migrationBuilder.DropIndex(
                name: "IX_TaskCenters_DeleteBy",
                table: "TaskCenters");

            migrationBuilder.DropIndex(
                name: "IX_SysUserLogs_DeleteBy",
                table: "SysUserLogs");

            migrationBuilder.DropIndex(
                name: "IX_SysMessageReceiveds_DeleteBy",
                table: "SysMessageReceiveds");

            migrationBuilder.DropIndex(
                name: "IX_SysMessageCenters_DeleteBy",
                table: "SysMessageCenters");

            migrationBuilder.DropIndex(
                name: "IX_SysHelps_DeleteBy",
                table: "SysHelps");

            migrationBuilder.DropIndex(
                name: "IX_SysHelpClasses_DeleteBy",
                table: "SysHelpClasses");

            migrationBuilder.DropIndex(
                name: "IX_SysDepartments_DeleteBy",
                table: "SysDepartments");

            migrationBuilder.DropIndex(
                name: "IX_JsonDatas_DeleteBy",
                table: "JsonDatas");

            migrationBuilder.DropIndex(
                name: "IX_JsonDataHistorys_DeleteBy",
                table: "JsonDataHistorys");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "TaskCenters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "TaskCenters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SysUserLogs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "SysUserLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SysMessageReceiveds",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "SysMessageReceiveds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SysMessageCenters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "SysMessageCenters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SysHelps",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "SysHelps",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SysHelpClasses",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "SysHelpClasses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SysDepartments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "SysDepartments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "JsonDatas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "JsonDatas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "JsonDataHistorys",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "JsonDataHistorys",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
