using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using Microsoft.Extensions.DependencyInjection;
using Models.SysModels;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web
{
    /// <summary>
    /// </summary>
    public class SeedData
    {
        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public static async System.Threading.Tasks.Task EnsureSeedDataAsync(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

            await db.Database.MigrateAsync();

            // 定义好的区域
            var sysAreas = new[]
        {
                        new SysArea
                        {
                            Id = "Platform",
                            AreaName = "Platform",
                            Name = "管理平台",
                            SystemId = "002"
                        },
                        new SysArea
                        {
                            Id = "Admin",
                            AreaName = "Admin",
                            Name = "企业管理平台",
                            SystemId = "010"
                        }
                    };

            foreach (var sysArea in sysAreas)
            {
                if (
                    !await db.SysAreas.AnyAsync(
                        a => a.Id == sysArea.Id &&
                             a.AreaName == sysArea.AreaName && a.Name == sysArea.Name &&
                             a.SystemId == sysArea.SystemId))
                {
                    await db.SysAreas.AddAsync(sysArea);
                }
            }

            // 操作类型
            var sysActions = new[]
            {
                        new SysAction
                        {
                            Name = "列表",  //包含导出
                            ActionName = "Index",
                            SystemId = "001",
                            System = true
                        },
                        new SysAction
                        {
                            Name = "详细",
                            ActionName = "Details",
                            SystemId = "003",
                            System = true
                        },
                        new SysAction
                        {
                            Name = "新建",
                            ActionName = "Create",
                            SystemId = "004",
                            System = true
                        },
                        new SysAction
                        {
                            Name = "编辑",
                            ActionName = "Edit",
                            SystemId = "005",
                            System = true
                        },
                          new SysAction
                        {
                            Name = "审核",
                            ActionName = "Audit",
                            SystemId = "007",
                            System = true
                        },
                        new SysAction
                        {
                            Name = "删除",
                            ActionName = "Delete",
                            SystemId = "006",
                            System = true
                        },
                    };
            foreach (var sysAction in sysActions)
            {
                if (!await db.SysActions.AnyAsync(a => a.ActionName == sysAction.ActionName))
                {
                    await db.SysActions.AddAsync(sysAction);
                }
            }

            #region SysController 控制器

            var sysControllers = new[]
            {
                        #region 基础内容 100

                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "管理平台",
                            ControllerName = "Home",
                            SystemId = "100",
                            Display = false
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "桌面",
                            ControllerName = "Desktop",
                            ActionName="Index",
                            SystemId = "100100",
                            Display = false
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "使用帮助",
                            ControllerName = "Help",
                            ActionName="Index",
                            SystemId = "100200",
                            Display = false
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "消息中心",
                            ControllerName = "MessageCenter", ActionName="Index",
                            SystemId = "100300",
                            Display = false
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "修改密码",
                            ControllerName = "ChangePassword", ActionName="Index",
                            SystemId = "100400",
                            Display = false
                        },

                #endregion 基础内容 100

                        #region 用户管理 900

                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "用户管理",
                            SystemId = "900",
                            Ico = "fa-users",
                            Display = true
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "组织架构",
                            ControllerName = "SysDepartment", ActionName="Index",
                            SystemId = "900200",
                            Ico = "fa-sitemap"
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "角色管理",
                            ControllerName = "SysRole", ActionName="Index",
                            SystemId = "900300",
                            Ico = "fa-users"
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "用户管理",
                            ControllerName = "SysUser", ActionName="Index",
                            SystemId = "900400",
                            Ico = "fa-user"
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "用户日志",
                            ControllerName = "SysUserLog", ActionName="Index",
                            SystemId = "900990",
                            Ico = "fa-calendar"
                        },

                        #endregion 用户管理 900

                        #region 系统开发配置 950

                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "系统设置",
                            SystemId = "950",
                            Ico = "fa-cog"
                        }, new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "系统模块",
                            ControllerName = "SysController", ActionName="Index",
                            SystemId = "950100",
                            Ico = "fa-info-circle"
                        },

                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "Json数据",
                            ControllerName = "JsonData", ActionName="Index",
                            SystemId = "950200",
                            Ico = "fa-info-circle"
                        },  new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "中国行政区域",
                            ControllerName = "CityCode", ActionName="Index",
                            SystemId = "950250",
                            Ico = "fa-info-circle"
                        }
                        ,
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "节假日设置",
                            ControllerName = "Holiday", ActionName="Index",
                            SystemId = "950300",
                            Ico = "fa-info-circle"
                        },

 new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "数据字典",
                            ControllerName = "DataDictionary", ActionName="Index",
                            SystemId = "950310",
                            Ico = "fa-info-circle"
                        },

                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "标签",
                            ControllerName = "DomainLabel", ActionName="Index",
                            SystemId = "950400",
                            Ico = "fa-info-circle"
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "帮助信息",
                            ControllerName = "SysHelp", ActionName="Index",
                            SystemId = "950800",
                            Ico = "fa-info-circle"
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "帮助信息分类",
                            ControllerName = "SysHelpClass", ActionName="Index",
                            SystemId = "950800100",
                            Ico = "fa-info-circle"
                        },

                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "系统日志",
                            ControllerName = "SysLog", ActionName="Index",
                            SystemId = "950900",
                            Ico = "fa-info-circle",
                        },

                        #endregion 系统开发配置 950
                    };

            //foreach (var sysController in sysControllers)
            //    if (
            //        !db.SysControllers.AnyAsync(
            //            a =>
            //                a.SysAreaId == sysController.SysAreaId &&
            //                a.ControllerName == sysController.ControllerName).Result)
            //        db.SysControllers.AddAsync(sysController).Wait();

            var sysControllers1 = sysControllers.Where(b => !db.SysControllers.Any(a => a.Name == b.Name && a.SysAreaId == b.SysAreaId && a.ControllerName == b.ControllerName && a.ActionName == b.ActionName && a.Parameter == b.Parameter)).ToList();

            //var sysControllers2 = db.SysControllers.Where(b => !sysControllers.Any(a => a.SysAreaId == b.SysAreaId && a.ControllerName == b.ControllerName && a.ActionName == b.ActionName && a.Parameter == b.Parameter)).ToList();

            //db.SysControllers.RemoveRange(sysControllers2);

            db.SysControllers.AddRange(sysControllers1);

            #endregion SysController 控制器

            await db.SaveChangesAsync();

            var sysControllerSysActions = (from sysAction in db.SysActions.Where(a => a.System)
                                           from sysController in db.SysControllers
                                           select
                                           new SysControllerSysAction
                                           {
                                               SysActionId = sysAction.Id,
                                               SysControllerId = sysController.Id
                                           }).ToArray();

            foreach (var sysControllerSysAction in sysControllerSysActions)
            {
                if (!await db.SysControllerSysActions.AnyAsync(
                    a =>
                        a.SysActionId == sysControllerSysAction.SysActionId &&
                        a.SysControllerId == sysControllerSysAction.SysControllerId))
                {
                    await db.SysControllerSysActions.AddAsync(sysControllerSysAction);
                }
            }

            await db.SaveChangesAsync();

            //初始化用户角色
            var sysRoles = new List<IdentityRole> { new IdentityRole { Name = "管理员", NormalizedName = "管理员" }, new IdentityRole { Name = "注册用户", NormalizedName = "注册用户" } };

            foreach (var item in sysRoles)
            {
                if (!await db.Roles.AnyAsync(
                    a => a.Name == item.Name))
                {
                    await db.Roles.AddAsync(item);
                }
            }
            await db.SaveChangesAsync();

            // 为管理员角色添加全部权限
            foreach (var item in db.SysControllerSysActions)
            {
                var role = db.Roles.FirstOrDefault(a => a.Name == "管理员");

                if (!await db.SysRoleSysControllerSysActions.AnyAsync(a => a.IdentityRole == role && a.SysControllerSysAction == item))
                {
                    await db.SysRoleSysControllerSysActions.AddAsync(new SysRoleSysControllerSysAction() { SysControllerSysAction = item, IdentityRole = role });
                }
            }

            await db.SaveChangesAsync();

            await db.SysLogs.AddAsync(new SysLog() { Id = Guid.NewGuid().ToString(), Log = "数据库初始化成功！" }); ;

            await db.SaveChangesAsync();

            // jsondata 创建全文检索
            //   await db.Database.ExecuteSqlInterpolatedAsync($"EXEC sp_fulltext_database 'enable';IF NOT EXISTS(  SELECT   *   FROM  sys.fulltext_catalogs WITH(NOLOCK)   WHERE    name = 'jsondatafull')  CREATE FULLTEXT CATALOG jsondatafull AS DEFAULT ; IF NOT EXISTS( SELECT  *  FROM sys.fulltext_index_fragments AS a, sys.tables AS b  WHERE  a.table_id = b.object_id AND name = 'JsonDatas') CREATE FULLTEXT INDEX ON dbo.JsonDatas(  JsonDataStr   Language 2052 ) KEY INDEX PK_JsonDatas ON jsondatafull WITH CHANGE_TRACKING AUTO; ");

            //启用全文检索 sql server
            // await db.Database.ExecuteSqlRawAsync($"EXEC sp_fulltext_database 'enable'");

            var tableName = "JsonDatas";
            var fildName = "JsonDataStr";

            //创建全文目录
            await db.Database.ExecuteSqlRawAsync($"IF NOT EXISTS(  SELECT   *   FROM  sys.fulltext_catalogs WITH(NOLOCK)   WHERE    name = '{tableName}fulltext') EXEC sp_fulltext_catalog '{tableName}fulltext','create'");

            //表启用全文检索
            await db.Database.ExecuteSqlRawAsync($" IF NOT EXISTS(  select * from sys.fulltext_indexes AS a, sys.tables AS b  WHERE  a.object_id = b.object_id and name='{tableName}') EXEC sp_fulltext_table 'dbo.{tableName}', 'create', '{tableName}fulltext', 'PK_{tableName}'");

            //添加字段
            await db.Database.ExecuteSqlRawAsync($" exec sp_fulltext_column 'dbo.{tableName}', '{fildName}', 'add','2052';");

            // 激活全文检索
            await db.Database.ExecuteSqlRawAsync($"EXEC sp_fulltext_table  'dbo.{tableName}','start_background_updateindex';");

            //全文检索sql
            // SELECT top 1 *  FROM dbo.JsonDatas AS A   INNER JOIN    FREETEXTTABLE(dbo.JsonDatas, JsonDataStr, '测试') AS K   ON A.id = K.[KEY]  where k.rank>0   ORDER BY k.RANK DESC
        }
    }
}