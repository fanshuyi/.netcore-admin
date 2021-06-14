using Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;

namespace Models.UserModels
{
    /// <summary>
    /// 知识库内容
    /// </summary>
    public class KnowledgeBase
    {
        public KnowledgeBase()
        {

        }

        /// <summary>
        /// 知识库分类
        /// </summary>
        [Required]
        [Display(Name = "KnowledgeCategory")]
        public string[] KnowledgeCategorys { get; set; }

        /// <summary>
        /// 知识库标题
        /// </summary>

        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataType(DataType.Html)]
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int UsageCount { get; set; }
    }
}