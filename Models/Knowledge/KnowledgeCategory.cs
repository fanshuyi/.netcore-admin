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
        public string Name { get; set; }



        /// <summary>
        /// 系统编号
        /// </summary>
        [Required]
        public string SystemId { get; set; }


        public bool Enable { get; set; }
    }
}