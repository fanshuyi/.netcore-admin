
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SysModels
{
    public class SysMessageReceived : DbSetBase//DbSetEnterpriseBase
    {
        [MaxLength(128)]
        [Required]
        [ForeignKey("SysBroadcast")]
        public string SysMessageId { get; set; }

        public virtual SysMessageCenter SysMessage { get; set; }
    }
}