﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Models.Infrastructure;

namespace Models.SysModels
{
    public class SysController : DbSetBaseId, IUserDictionary
    {
        public SysController()
        {
            SystemId = "000";
            TargetBlank = false;
            Display = true;
            Enable = true;
            Ico = "fa-ICollection-ul";
            Parameter = "";
        }

        [Display(Name = "Area")]
        [Required]
        [ForeignKey("SysArea")]
        public string SysAreaId { get; set; }

        [ScaffoldColumn(false)]
        public virtual SysArea SysArea { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        public string ControllerName { get; set; }

        [MaxLength(50)]
        public string ActionName { get; set; }

        [MaxLength(50)]
        public string Parameter { get; set; }

        [MaxLength(50)]
        [Required]
        public string SystemId { get; set; }

        public bool Display { get; set; }

        public bool TargetBlank { get; set; }

        [DataType("Ico")]
        public string Ico { get; set; }

        [ScaffoldColumn(false)]
        public virtual ICollection<SysControllerSysAction> SysControllerSysActions { get; set; }

        [ScaffoldColumn(false)]
        [NotMapped]
        public bool Selected { get; set; }

        public bool Enable { get; set; }
    }
}