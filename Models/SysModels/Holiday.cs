using System;
using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    /// <summary>
    /// 节假日设置
    /// </summary>
    public class Holiday
    {
        public Holiday()
        {
            StartDate = DateTimeOffset.Now.Date;
            EndDate = DateTimeOffset.Now.Date.AddDays(1);
        }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(256)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 实际结束时间 填写该时间代表任务完成
        /// </summary>

        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>

        public DateTimeOffset EndDate { get; set; }

        /// <summary>
        /// 是否为调休上班日
        /// </summary>
        public bool Work { get; set; }
    }
}