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
    /// 知识库分类
    /// </summary>
    public class KnowledgeCategory
    {
        public KnowledgeCategory()
        {
            SystemId = "000";
            Enable = true;
        }

        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }



        /// <summary>
        /// 系统编号
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string SystemId { get; set; }

        [ScaffoldColumn(false)]
        [NotMapped]
        public bool Selected { get; set; }

        public bool Enable { get; set; }
    }
}