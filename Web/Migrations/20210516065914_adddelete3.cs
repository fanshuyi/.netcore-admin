using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class adddelete3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "TaskCenters",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "TaskCenters",
                newName: "DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_TaskCenters_DeleteBy",
                table: "TaskCenters",
                newName: "IX_TaskCenters_DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "SysUserLogs",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "SysUserLogs",
                newName: "DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_SysUserLogs_DeleteBy",
                table: "SysUserLogs",
                newName: "IX_SysUserLogs_DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "SysRoleSysControllerSysActions",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "SysRoleSysControllerSysActions",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "SysMessageReceiveds",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "SysMessageReceiveds",
                newName: "DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_SysMessageReceiveds_DeleteBy",
                table: "SysMessageReceiveds",
                newName: "IX_SysMessageReceiveds_DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "SysMessageCenters",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "SysMessageCenters",
                newName: "DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_SysMessageCenters_DeleteBy",
                table: "SysMessageCenters",
                newName: "IX_SysMessageCenters_DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "SysLogs",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "SysLogs",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "SysHelps",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "SysHelps",
                newName: "DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_SysHelps_DeleteBy",
                table: "SysHelps",
                newName: "IX_SysHelps_DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "SysHelpClasses",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "SysHelpClasses",
                newName: "DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_SysHelpClasses_DeleteBy",
                table: "SysHelpClasses",
                newName: "IX_SysHelpClasses_DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "SysDepartmentSysUsers",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "SysDepartmentSysUsers",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "SysDepartments",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "SysDepartments",
                newName: "DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_SysDepartments_DeleteBy",
                table: "SysDepartments",
                newName: "IX_SysDepartments_DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "SysControllerSysActions",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "SysControllerSysActions",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "SysControllers",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "SysControllers",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "SysAreas",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "SysAreas",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "SysActions",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "SysActions",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "JsonDatas",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "JsonDatas",
                newName: "DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_JsonDatas_DeleteBy",
                table: "JsonDatas",
                newName: "IX_JsonDatas_DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "JsonDataHistorys",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "JsonDataHistorys",
                newName: "DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_JsonDataHistorys_DeleteBy",
                table: "JsonDataHistorys",
                newName: "IX_JsonDataHistorys_DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "DomainLabels",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "DomainLabels",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "AspNetUserTokens",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "AspNetUserTokens",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "AspNetUsers",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "AspNetUsers",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "AspNetUserRoles",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "AspNetUserRoles",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "AspNetUserLogins",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "AspNetUserLogins",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "AspNetUserClaims",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "AspNetUserClaims",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "AspNetRoles",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "AspNetRoles",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "AspNetRoleClaims",
                newName: "DeletedDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteBy",
                table: "AspNetRoleClaims",
                newName: "DeletedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_JsonDataHistorys_AspNetUsers_DeletedBy",
                table: "JsonDataHistorys",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JsonDatas_AspNetUsers_DeletedBy",
                table: "JsonDatas",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysDepartments_AspNetUsers_DeletedBy",
                table: "SysDepartments",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysHelpClasses_AspNetUsers_DeletedBy",
                table: "SysHelpClasses",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysHelps_AspNetUsers_DeletedBy",
                table: "SysHelps",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysMessageCenters_AspNetUsers_DeletedBy",
                table: "SysMessageCenters",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysMessageReceiveds_AspNetUsers_DeletedBy",
                table: "SysMessageReceiveds",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysUserLogs_AspNetUsers_DeletedBy",
                table: "SysUserLogs",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCenters_AspNetUsers_DeletedBy",
                table: "TaskCenters",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JsonDataHistorys_AspNetUsers_DeletedBy",
                table: "JsonDataHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_JsonDatas_AspNetUsers_DeletedBy",
                table: "JsonDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_SysDepartments_AspNetUsers_DeletedBy",
                table: "SysDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_SysHelpClasses_AspNetUsers_DeletedBy",
                table: "SysHelpClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_SysHelps_AspNetUsers_DeletedBy",
                table: "SysHelps");

            migrationBuilder.DropForeignKey(
                name: "FK_SysMessageCenters_AspNetUsers_DeletedBy",
                table: "SysMessageCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_SysMessageReceiveds_AspNetUsers_DeletedBy",
                table: "SysMessageReceiveds");

            migrationBuilder.DropForeignKey(
                name: "FK_SysUserLogs_AspNetUsers_DeletedBy",
                table: "SysUserLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskCenters_AspNetUsers_DeletedBy",
                table: "TaskCenters");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "TaskCenters",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "TaskCenters",
                newName: "DeleteBy");

            migrationBuilder.RenameIndex(
                name: "IX_TaskCenters_DeletedBy",
                table: "TaskCenters",
                newName: "IX_TaskCenters_DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "SysUserLogs",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SysUserLogs",
                newName: "DeleteBy");

            migrationBuilder.RenameIndex(
                name: "IX_SysUserLogs_DeletedBy",
                table: "SysUserLogs",
                newName: "IX_SysUserLogs_DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "SysRoleSysControllerSysActions",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SysRoleSysControllerSysActions",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "SysMessageReceiveds",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SysMessageReceiveds",
                newName: "DeleteBy");

            migrationBuilder.RenameIndex(
                name: "IX_SysMessageReceiveds_DeletedBy",
                table: "SysMessageReceiveds",
                newName: "IX_SysMessageReceiveds_DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "SysMessageCenters",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SysMessageCenters",
                newName: "DeleteBy");

            migrationBuilder.RenameIndex(
                name: "IX_SysMessageCenters_DeletedBy",
                table: "SysMessageCenters",
                newName: "IX_SysMessageCenters_DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "SysLogs",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SysLogs",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "SysHelps",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SysHelps",
                newName: "DeleteBy");

            migrationBuilder.RenameIndex(
                name: "IX_SysHelps_DeletedBy",
                table: "SysHelps",
                newName: "IX_SysHelps_DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "SysHelpClasses",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SysHelpClasses",
                newName: "DeleteBy");

            migrationBuilder.RenameIndex(
                name: "IX_SysHelpClasses_DeletedBy",
                table: "SysHelpClasses",
                newName: "IX_SysHelpClasses_DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "SysDepartmentSysUsers",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SysDepartmentSysUsers",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "SysDepartments",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SysDepartments",
                newName: "DeleteBy");

            migrationBuilder.RenameIndex(
                name: "IX_SysDepartments_DeletedBy",
                table: "SysDepartments",
                newName: "IX_SysDepartments_DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "SysControllerSysActions",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SysControllerSysActions",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "SysControllers",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SysControllers",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "SysAreas",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SysAreas",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "SysActions",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "SysActions",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "JsonDatas",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "JsonDatas",
                newName: "DeleteBy");

            migrationBuilder.RenameIndex(
                name: "IX_JsonDatas_DeletedBy",
                table: "JsonDatas",
                newName: "IX_JsonDatas_DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "JsonDataHistorys",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "JsonDataHistorys",
                newName: "DeleteBy");

            migrationBuilder.RenameIndex(
                name: "IX_JsonDataHistorys_DeletedBy",
                table: "JsonDataHistorys",
                newName: "IX_JsonDataHistorys_DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "DomainLabels",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "DomainLabels",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "AspNetUserTokens",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "AspNetUserTokens",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "AspNetUsers",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "AspNetUsers",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "AspNetUserRoles",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "AspNetUserRoles",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "AspNetUserLogins",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "AspNetUserLogins",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "AspNetUserClaims",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "AspNetUserClaims",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "AspNetRoles",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "AspNetRoles",
                newName: "DeleteBy");

            migrationBuilder.RenameColumn(
                name: "DeletedDateTime",
                table: "AspNetRoleClaims",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "AspNetRoleClaims",
                newName: "DeleteBy");

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
    }
}
