using System;
using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    /// <summary>
    /// 助理记录
    /// </summary>
    public class Assistant
    {

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public string Content { get; set; }

    }
}