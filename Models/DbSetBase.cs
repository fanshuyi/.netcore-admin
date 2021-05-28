using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

namespace Models
{
    public interface IDbSetBaseId
    {
        string Id { get; set; }
    }

    public interface IDbSetBase
    {
        DateTimeOffset CreatedDateTime { get; set; }

        DateTimeOffset? UpdatedDateTime { get; set; }

        DateTimeOffset? DeletedDateTime { get; set; }

        string CreatedBy { get; set; }

        string UpdatedBy { get; set; }

        string DeletedBy { get; set; }

        string DepartmentId { get; set; }

        bool IsDeleted { get; set; }
    }

    public abstract class DbSetBaseId : IDbSetBaseId
    {
        public DbSetBaseId()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        [ScaffoldColumn(false)]
        [MaxLength(128)]
        public string Id { get; set; }
    }

    /// <summary>
    /// 企业关联基础表
    /// </summary>
    public abstract class DbSetBase : DbSetBaseId, IDbSetBase
    {
        public DbSetBase()
        {
            Id = Guid.NewGuid().ToString();
            // CreatedDateTime = DateTimeOffset.Now;
        }

        /// <summary>
        /// 创建日期时间
        /// </summary>
        [Editable(false)]
        [Required]
        [Display(Name = "CreatedDateTime")]
        public DateTimeOffset CreatedDateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Editable(false)]
        [Display(Name = "CreatedBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [ScaffoldColumn(false)]
        [ForeignKey("CreatedBy")]
        public virtual IdentityUser UserCreatedBy { get; set; }

        /// <summary>
        /// 记录所属部门ID
        /// </summary>
        [MaxLength(128)]
        [ScaffoldColumn(false)]
        public string DepartmentId { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        [Editable(false)]
        [Display(Name = "UpdatedDateTime")]
        public DateTimeOffset? UpdatedDateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [Editable(false)]
        [Display(Name = "UpdatedBy")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [ScaffoldColumn(false)]
        [ForeignKey("UpdatedBy")]
        public virtual IdentityUser UserUpdatedBy { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "Remark", Description = "Remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [ScaffoldColumn(false)]
        public DateTimeOffset? DeletedDateTime { get; set; }

        /// <summary>
        /// 删除人ID
        /// </summary>
        [ScaffoldColumn(false)]
        public string DeletedBy { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>
        [ScaffoldColumn(false)]
        [ForeignKey("DeletedBy")]
        public virtual IdentityUser UserDeletedBy { get; set; }
    }
}