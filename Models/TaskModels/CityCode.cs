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
    /// 城市库 http://www.ip33.com/area_code.html
    /// </summary>
    public class CityCode
    {
        public CityCode()
        {
            SystemId = "00"; //按照国家规则2位一组
            Enable = true;
        }

        /// <summary>
        /// 城市名称
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        [MaxLength(6)]
        public string ZipCode { get; set; }

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