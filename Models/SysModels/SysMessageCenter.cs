
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.SysModels
{
    /// <summary>
    /// 消息
    /// </summary>
    public class SysMessageCenter : DbSetBase//DbSetEnterpriseBase
    {
        //收件人 如果收件人为空则为站内广播 全体接收
        //如果有ID 则为个人消息

        [ScaffoldColumn(false)]
        //[MaxLength(128)]
        //[ForeignKey("Addressee")]
        public string AddresseeId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Picture { get; set; }

        [DataType(DataType.Html)]
        [Required]
        public string Content { get; set; }

        public virtual ICollection<SysMessageReceived> SysMessageReceiveds { get; set; }

        [MaxLength(500)] //根据消息类型判定是否有查看详情链接
        public string Url { get; set; }

        //根据活动截止时间设置
        public DateTime? AbsoluteExpirationUtcDateTime { get; set; } //活动类消息过期时间
    }
}