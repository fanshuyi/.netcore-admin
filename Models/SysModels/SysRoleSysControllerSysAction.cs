using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

namespace Models.SysModels
{
    public class SysRoleSysControllerSysAction : DbSetBaseId
    {
        [ForeignKey("IdentityRole")]
        [Required]
        public string RoleId { get; set; }

        public virtual IdentityRole IdentityRole { get; set; }

        [ForeignKey("SysControllerSysAction")]
        [Required]
        public string SysControllerSysActionId { get; set; }

        public virtual SysControllerSysAction SysControllerSysAction { get; set; }
    }
}