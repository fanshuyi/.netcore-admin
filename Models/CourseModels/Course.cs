using Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.CourseModels
{
    /// <summary>
    /// 课程分类
    /// </summary>
    public class CourseType : DbSetBaseId, IUserDictionary
    {
        public CourseType()
        {
            SystemId = "000";
            Enable = true;
        }

        /// <summary>
        /// 分类名称
        /// </summary>
        [MaxLength(40)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        [MaxLength(30)]
        [Required]
        public string SystemId { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        [ScaffoldColumn(false)]
        [NotMapped]
        public bool Selected { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool Enable { get; set; }

        /// <summary>
        /// 热门分类
        /// </summary>
        [Required]
        public bool Hot { get; set; }

        [ScaffoldColumn(false)]
        public virtual ICollection<Course> Courses { get; set; }
    }

    /// <summary>
    /// 课程基础信息
    /// </summary>
    public class Course : DbSetBase
    {
        public Course()
        {

        }

        /// <summary>
        /// 课程分类
        /// </summary>
        [Required]
        public virtual CourseType CourseType { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        [MaxLength(200)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        [MaxLength(100)]
        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [MaxLength]
        [DataType(DataType.Html)]
        public string Introduction { get; set; }

        /// <summary>
        /// 授课方式
        /// </summary>
        public TeachingMethod TeachingMethod { get; set; }

        /// <summary>
        /// 上课时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 下课时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 课程价格
        /// </summary>
        [Required]
        [Range(0, 999)]
        public decimal Price { get; set; }

        /// <summary>
        /// 视频地址 录播或者上传视频
        /// </summary>
        [MaxLength(1024)]
        public string VideoUrl { get; set; }

    }

    /// <summary>
    /// 授课方式
    /// </summary>
    public enum TeachingMethod
    {
        /// <summary>
        /// 直播
        /// </summary>
        Live,
        /// <summary>
        /// 录播
        /// </summary>
        Recording,
        /// <summary>
        /// 线下
        /// </summary>
        Offline
    }
}