using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.AccountViewModels
{
    /// <summary>
    /// </summary>
    public class RegisterViewModel
    {
        [Required]
        [Remote("CheckUserAccountExists", "Account", ErrorMessage = "用户名已存在！")] // 远程验证（Ajax）
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// </summary>

        //[Remote("CheckUserEmailExists", "Account", ErrorMessage = "用户邮箱已存在！")] // 远程验证（Ajax）
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        //[Remote("CheckUserEmailExists", "Account", ErrorMessage = "号码已存在！")] // 远程验证（Ajax）
        //[Display(Name = "PhoneNumber")]
        //public string PhoneNumber { get; set; }

        /// <summary>
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// </summary>
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password", ErrorMessage = "两次输入的密码不同！")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// </summary>
        [Remote("CheckRefereeExists", "Account", ErrorMessage = "推荐人不存在，请重新检查！")] // 远程验证（Ajax）
        [Display(Name = "Referee")]
        public string Referee { get; set; }
    }
}