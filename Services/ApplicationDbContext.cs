using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Models.TaskModels;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Models.UserModels;
using System.Threading;
using System.Threading.Tasks;
using Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using IdentityServer4.EntityFramework.Options;

namespace Services
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
            {
                Console.WriteLine("EntityName:" + entry.Entity.GetType().FullName + " IsKeySet:" + entry.IsKeySet + " EntityState:" + entry.State);

                if (entry.Entity.GetType().Name != "IdentityUserRole`1Proxy") //略过某些需要真实删除的类型
                {
                    entry.Property("IsDeleted").CurrentValue = true;
                    entry.Property("DeletedDateTime").CurrentValue = DateTimeOffset.Now;
                    entry.State = EntityState.Modified;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed. For
            // example, you can rename the ASP.NET Identity table names and more. Add your
            // customizations after calling base.OnModelCreating(builder);

            // 索引
            //builder.Entity<JsonData>().HasIndex(a => a.JsonDataType);
            //builder.Entity<JsonData>().HasIndex(a => a.JsonDataStr);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                //// 添加字段
                //entityType.AddProperty("IsDeleted", typeof(bool));

                // 过滤字段
                builder.Entity(entityType.ClrType).Property<bool>("IsDeleted");

                builder.Entity(entityType.ClrType).Property<DateTimeOffset?>("DeletedDateTime");

                builder.Entity(entityType.ClrType).Property<string>("DeletedBy");

                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var body = Expression.Equal(
                    Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant("IsDeleted")),
                Expression.Constant(false));

                builder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));

                // 索引
                if (entityType.FindProperty("CreatedDateTime") is var createdDateTime && createdDateTime != null)
                {
                    entityType.AddIndex(createdDateTime);
                }

                if (entityType.FindProperty("IsDeleted") is var IsDeleted && IsDeleted != null)
                {
                    entityType.AddIndex(IsDeleted);
                }
            }

            // 添加函数
            builder.HasDbFunction(typeof(SqlDbFunctions).GetMethod(nameof(SqlDbFunctions.JsonValue)))
                 .HasTranslation(args => new SqlFunctionExpression("JSON_VALUE", args, true, new[] { true, false }, typeof(string), null));

            // 添加函数
            builder.HasDbFunction(typeof(SqlDbFunctions).GetMethod(nameof(SqlDbFunctions.IsJson)))
                 .HasTranslation(args => new SqlFunctionExpression("ISJSON", args, true, new[] { true, false }, typeof(int), null));

            base.OnModelCreating(builder);
        }

        #region 任务中心

        public virtual DbSet<TaskCenter> TaskCenters { get; set; }

        //public DbSet<IdentityUser> IdentityUsers { get; set; }

        #endregion 任务中心

        #region 系统表

        /// <summary>
        /// 系统区域
        /// </summary>
        public virtual DbSet<SysArea> SysAreas { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public virtual DbSet<SysController> SysControllers { get; set; }

        /// <summary>
        /// 控制器对应的操作
        /// </summary>
        public virtual DbSet<SysControllerSysAction> SysControllerSysActions { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public virtual DbSet<SysAction> SysActions { get; set; }

        /// <summary>
        /// 角色对应的controller和action
        /// </summary>
        public virtual DbSet<SysRoleSysControllerSysAction> SysRoleSysControllerSysActions { get; set; }

        /// <summary>
        /// 系统帮助
        /// </summary>
        public virtual DbSet<SysHelp> SysHelps { get; set; }

        /// <summary>
        /// 系统帮助分类
        /// </summary>
        public virtual DbSet<SysHelpClass> SysHelpClasses { get; set; }

        /// <summary>
        /// 系统消息
        /// </summary>
        public virtual DbSet<SysMessageCenter> SysMessageCenters { get; set; }

        /// <summary>
        /// 系统消息读取记录
        /// </summary>
        public virtual DbSet<SysMessageReceived> SysMessageReceiveds { get; set; }

        /// <summary>
        /// 组织架构
        /// </summary>
        public virtual DbSet<SysDepartment> SysDepartments { get; set; }

        /// <summary>
        /// 用户关联部门
        /// </summary>
        public virtual DbSet<SysDepartmentSysUser> SysDepartmentSysUsers { get; set; }

        /// <summary>
        /// 系统日志
        /// </summary>
        public virtual DbSet<SysLog> SysLogs { get; set; }

        /// <summary>
        /// 用户日志
        /// </summary>
        public virtual DbSet<SysUserLog> SysUserLogs { get; set; }

        /// <summary>
        /// 通用json数据
        /// </summary>
        public virtual DbSet<JsonData> JsonDatas { get; set; }

        /// <summary>
        /// json数据历史记录
        /// </summary>
        public virtual DbSet<JsonDataHistory> JsonDataHistorys { get; set; }

        #endregion 系统表

        /// <summary>
        /// 标签
        /// </summary>
        public virtual DbSet<DomainLabel> DomainLabels { get; set; }
    }
}