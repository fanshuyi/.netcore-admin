
using System;
using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    /// <summary>
    /// json数据
    /// </summary>
    public class JsonData : DbSetBase
    {
        public JsonData()
        {
        }

        /// <summary>
        /// 自定义类型
        /// </summary>
        [Required]
        [MaxLength(128)]
        [Display(Description = "Custom type")]
        public string JsonDataType { get; set; }

        /// <summary>
        /// Json数据
        /// </summary>
        [MaxLength]
        [Required]
        [DataType(DataType.MultilineText)]
        public string JsonDataStr { get; set; }
    }

    /// <summary>
    /// json数据历史记录
    /// </summary>
    public class JsonDataHistory : DbSetBase
    {
        public JsonDataHistory()
        {
        }

        [Required]
        [MaxLength(128)]
        public string RecordID { get; set; }

        /// <summary>
        /// 自定义类型
        /// </summary>
        [Required]
        [MaxLength(128)]
        [Display(Description = "Custom type")]
        public string JsonDataType { get; set; }

        /// <summary>
        /// Json数据
        /// </summary>
        [MaxLength]
        [Required]
        [DataType(DataType.MultilineText)]
        public string JsonDataStr { get; set; }
    }
}