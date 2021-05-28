using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SysModels
{
    /// <summary>
    /// 记录后台用户操作痕迹
    /// </summary>
    public class SysUserLog : DbSetBase
    {
        public SysUserLog()
        {
            ViewDuration = 0;
            ActionDuration = 0;
            Duration = 0;
        }

        [MaxLength(100)]
        public string SysArea { get; set; }

        [MaxLength(100)]
        public string SysController { get; set; }

        [MaxLength(100)]
        public string SysAction { get; set; }

        [MaxLength(100)]
        public string RecordId { get; set; }

        [MaxLength(100)]
        public string Ip { get; set; }

        [MaxLength(1000)]
        public string Url { get; set; }

        [MaxLength(100)]
        public string RequestType { get; set; }

        public double ViewDuration { get; set; }

        public double ActionDuration { get; set; }

        public double Duration { get; set; }
    }
}