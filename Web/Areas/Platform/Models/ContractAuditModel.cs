using System.ComponentModel;

namespace Web.Areas.Platform.Models
{
    public enum ContractAuditModelState
    {
        审核通过 = 2,
        审核未通过 = 3,
    }

    public class ContractAuditModel
    {
        [DisplayName("Audit")]
        public ContractAuditModelState ContractAuditModelState { get; set; }

        public string Remark { get; set; }
    }
}