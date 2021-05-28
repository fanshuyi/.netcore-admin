using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DomainLabels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Label = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LabelType = table.Column<int>(type: "int", nullable: false),
                    Heat = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainLabels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysActions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SystemId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysAreas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    AreaName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SystemId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Log = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JsonDataHistorys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    RecordID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    JsonDataType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    JsonDataStr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JsonDataHistorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JsonDataHistorys_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JsonDataHistorys_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JsonDatas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    JsonDataType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    JsonDataStr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JsonDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JsonDatas_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JsonDatas_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysDepartments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SystemId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysDepartments_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysDepartments_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysHelpClasses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SystemId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysHelpClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysHelpClasses_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysHelpClasses_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysMessageCenters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AddresseeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AbsoluteExpirationUtcDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysMessageCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysMessageCenters_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysMessageCenters_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysUserLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SysArea = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SysController = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SysAction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ip = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RequestType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ViewDuration = table.Column<double>(type: "float", nullable: false),
                    ActionDuration = table.Column<double>(type: "float", nullable: false),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUserLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysUserLogs_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysUserLogs_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskCenters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TaskType = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Files = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduleEndTime = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ActualEndTime = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    TaskExecutorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskCenters_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskCenters_AspNetUsers_TaskExecutorId",
                        column: x => x.TaskExecutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskCenters_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysControllers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SysAreaId = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Parameter = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SystemId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Display = table.Column<bool>(type: "bit", nullable: false),
                    TargetBlank = table.Column<bool>(type: "bit", nullable: false),
                    Ico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysControllers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysControllers_SysAreas_SysAreaId",
                        column: x => x.SysAreaId,
                        principalTable: "SysAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysDepartmentSysUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SysDepartmentId = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    SysUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysDepartmentSysUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysDepartmentSysUsers_AspNetUsers_SysUserId",
                        column: x => x.SysUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysDepartmentSysUsers_SysDepartments_SysDepartmentId",
                        column: x => x.SysDepartmentId,
                        principalTable: "SysDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysHelps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SysHelpClassId = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysHelps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysHelps_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysHelps_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysHelps_SysHelpClasses_SysHelpClassId",
                        column: x => x.SysHelpClassId,
                        principalTable: "SysHelpClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysMessageReceiveds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SysMessageId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysMessageReceiveds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysMessageReceiveds_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysMessageReceiveds_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysMessageReceiveds_SysMessageCenters_SysMessageId",
                        column: x => x.SysMessageId,
                        principalTable: "SysMessageCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysControllerSysActions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SysControllerId = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    SysActionId = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysControllerSysActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysControllerSysActions_SysActions_SysActionId",
                        column: x => x.SysActionId,
                        principalTable: "SysActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysControllerSysActions_SysControllers_SysControllerId",
                        column: x => x.SysControllerId,
                        principalTable: "SysControllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysRoleSysControllerSysActions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SysControllerSysActionId = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRoleSysControllerSysActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysRoleSysControllerSysActions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysRoleSysControllerSysActions_SysControllerSysActions_SysControllerSysActionId",
                        column: x => x.SysControllerSysActionId,
                        principalTable: "SysControllerSysActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_IsDeleted",
                table: "AspNetRoleClaims",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_IsDeleted",
                table: "AspNetRoles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_IsDeleted",
                table: "AspNetUserClaims",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_IsDeleted",
                table: "AspNetUserLogins",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_IsDeleted",
                table: "AspNetUserRoles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IsDeleted",
                table: "AspNetUsers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_IsDeleted",
                table: "AspNetUserTokens",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DomainLabels_IsDeleted",
                table: "DomainLabels",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_JsonDataHistorys_CreatedBy",
                table: "JsonDataHistorys",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_JsonDataHistorys_CreatedDateTime",
                table: "JsonDataHistorys",
                column: "CreatedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_JsonDataHistorys_IsDeleted",
                table: "JsonDataHistorys",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_JsonDataHistorys_UpdatedBy",
                table: "JsonDataHistorys",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_JsonDatas_CreatedBy",
                table: "JsonDatas",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_JsonDatas_CreatedDateTime",
                table: "JsonDatas",
                column: "CreatedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_JsonDatas_IsDeleted",
                table: "JsonDatas",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_JsonDatas_UpdatedBy",
                table: "JsonDatas",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysActions_IsDeleted",
                table: "SysActions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SysAreas_IsDeleted",
                table: "SysAreas",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SysControllers_IsDeleted",
                table: "SysControllers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SysControllers_SysAreaId",
                table: "SysControllers",
                column: "SysAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_SysControllerSysActions_IsDeleted",
                table: "SysControllerSysActions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SysControllerSysActions_SysActionId",
                table: "SysControllerSysActions",
                column: "SysActionId");

            migrationBuilder.CreateIndex(
                name: "IX_SysControllerSysActions_SysControllerId",
                table: "SysControllerSysActions",
                column: "SysControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_SysDepartments_CreatedBy",
                table: "SysDepartments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysDepartments_CreatedDateTime",
                table: "SysDepartments",
                column: "CreatedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SysDepartments_IsDeleted",
                table: "SysDepartments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SysDepartments_UpdatedBy",
                table: "SysDepartments",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysDepartmentSysUsers_IsDeleted",
                table: "SysDepartmentSysUsers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SysDepartmentSysUsers_SysDepartmentId",
                table: "SysDepartmentSysUsers",
                column: "SysDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SysDepartmentSysUsers_SysUserId",
                table: "SysDepartmentSysUsers",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelpClasses_CreatedBy",
                table: "SysHelpClasses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelpClasses_CreatedDateTime",
                table: "SysHelpClasses",
                column: "CreatedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelpClasses_IsDeleted",
                table: "SysHelpClasses",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelpClasses_UpdatedBy",
                table: "SysHelpClasses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelps_CreatedBy",
                table: "SysHelps",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelps_CreatedDateTime",
                table: "SysHelps",
                column: "CreatedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelps_IsDeleted",
                table: "SysHelps",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelps_SysHelpClassId",
                table: "SysHelps",
                column: "SysHelpClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelps_UpdatedBy",
                table: "SysHelps",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysLogs_CreatedDateTime",
                table: "SysLogs",
                column: "CreatedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SysLogs_IsDeleted",
                table: "SysLogs",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SysMessageCenters_CreatedBy",
                table: "SysMessageCenters",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysMessageCenters_CreatedDateTime",
                table: "SysMessageCenters",
                column: "CreatedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SysMessageCenters_IsDeleted",
                table: "SysMessageCenters",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SysMessageCenters_UpdatedBy",
                table: "SysMessageCenters",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysMessageReceiveds_CreatedBy",
                table: "SysMessageReceiveds",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysMessageReceiveds_CreatedDateTime",
                table: "SysMessageReceiveds",
                column: "CreatedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SysMessageReceiveds_IsDeleted",
                table: "SysMessageReceiveds",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SysMessageReceiveds_SysMessageId",
                table: "SysMessageReceiveds",
                column: "SysMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_SysMessageReceiveds_UpdatedBy",
                table: "SysMessageReceiveds",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysRoleSysControllerSysActions_IsDeleted",
                table: "SysRoleSysControllerSysActions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SysRoleSysControllerSysActions_RoleId",
                table: "SysRoleSysControllerSysActions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SysRoleSysControllerSysActions_SysControllerSysActionId",
                table: "SysRoleSysControllerSysActions",
                column: "SysControllerSysActionId");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserLogs_CreatedBy",
                table: "SysUserLogs",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserLogs_CreatedDateTime",
                table: "SysUserLogs",
                column: "CreatedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserLogs_IsDeleted",
                table: "SysUserLogs",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserLogs_UpdatedBy",
                table: "SysUserLogs",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCenters_CreatedBy",
                table: "TaskCenters",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCenters_CreatedDateTime",
                table: "TaskCenters",
                column: "CreatedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCenters_IsDeleted",
                table: "TaskCenters",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCenters_TaskExecutorId",
                table: "TaskCenters",
                column: "TaskExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCenters_UpdatedBy",
                table: "TaskCenters",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DomainLabels");

            migrationBuilder.DropTable(
                name: "JsonDataHistorys");

            migrationBuilder.DropTable(
                name: "JsonDatas");

            migrationBuilder.DropTable(
                name: "SysDepartmentSysUsers");

            migrationBuilder.DropTable(
                name: "SysHelps");

            migrationBuilder.DropTable(
                name: "SysLogs");

            migrationBuilder.DropTable(
                name: "SysMessageReceiveds");

            migrationBuilder.DropTable(
                name: "SysRoleSysControllerSysActions");

            migrationBuilder.DropTable(
                name: "SysUserLogs");

            migrationBuilder.DropTable(
                name: "TaskCenters");

            migrationBuilder.DropTable(
                name: "SysDepartments");

            migrationBuilder.DropTable(
                name: "SysHelpClasses");

            migrationBuilder.DropTable(
                name: "SysMessageCenters");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "SysControllerSysActions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SysActions");

            migrationBuilder.DropTable(
                name: "SysControllers");

            migrationBuilder.DropTable(
                name: "SysAreas");
        }
    }
}
