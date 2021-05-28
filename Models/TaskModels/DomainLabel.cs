using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.UserModels
{
    public enum LabelTypes
    {
        系统预设,
        用户搜索,
        系统禁用
    }

    /// <summary>
    /// 标签库
    /// </summary>
    public class DomainLabel : DbSetBaseId
    {
        /// <summary>
        /// 标签 例如：装修 餐饮 礼仪
        /// </summary>
        [Required]
        [MaxLength(128)]
        [DisplayName("Label")]
        public string Label { get; set; }

        /// <summary>
        /// 标签类型
        /// </summary>
        [Required]
        [DisplayName("LabelType")]
        public LabelTypes LabelType { get; set; }

        /// <summary>
        /// 热度
        /// </summary>
        [DisplayName("Heat")]
        public int Heat { get; set; }
    }
}