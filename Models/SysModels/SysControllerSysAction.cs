﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SysModels
{
    public class SysControllerSysAction : DbSetBaseId
    {
        [ForeignKey("SysController")]
        [Required]
        public string SysControllerId { get; set; }

        public virtual SysController SysController { get; set; }

        [ForeignKey("SysAction")]
        [Required]
        public string SysActionId { get; set; }

        public virtual SysAction SysAction { get; set; }

        public virtual ICollection<SysRoleSysControllerSysAction> SysRoleSysControllerSysActions { get; set; }
    }
}