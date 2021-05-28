// Copyright (c) Brock Allen & Dominick Baier. All rights reserved. Licensed under the Apache
// License, Version 2.0. See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace Web.Models.AccountViewModels
{
    /// <summary>
    /// 用户登录模型
    /// </summary>
    public class LoginInputModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    /// <summary>
    /// </summary>
    public class PhoneNumberLoginInputModel
    {
        /// <summary>
        /// </summary>
        [Required]
        [Display(Name = "PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// </summary>
        [Required]
        [Display(Name = "Verification Code")]
        public string VerificationCode { get; set; }
    }
}