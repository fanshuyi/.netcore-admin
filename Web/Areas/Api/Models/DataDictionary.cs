using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Infrastructure;

namespace Web.Areas.Api.Models
{
    /// <summary>
    /// 数据字典模型
    /// </summary>
    public class DataDictionary
    {
        /// <summary>
        /// 数据字典模型
        /// </summary>
        public DataDictionary()
        {
            Enable = true;
        }

        /// <summary>
        /// 分类
        /// </summary>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        [Required]
        public string SystemId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool Enable { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }
    }
}