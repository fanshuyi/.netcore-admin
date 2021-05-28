using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Platform.Models
{
    public class SysUserCreateModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
    }

    public class SysUserDetailsModel
    {
        [DataType("SystemId")]
        [Display(Name = "Department")]
        public string DepartmentId { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        [Display(Name = "RoleName")]
        public string[] SysRolesId { get; set; }
    }

    public class SysUserEditModel
    {
        [Required]
        [Display(Name = "Department")]
        public string[] DepartmentId { get; set; }

        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }

        [Display(Name = "RoleName")]
        [Required]
        public string[] SysRolesId { get; set; }
    }
}