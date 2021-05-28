using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.ManageViewModels
{
    /// <summary>
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// </summary>
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// </summary>
        [Display(Name = "Nick Name")]
        [Required]
        public string Nickname { get; set; }

        /// <summary>
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Description = "Retrieve password")]
        public string Email { get; set; }

        /// <summary>
        /// </summary>
        [Phone]
        [Required]
        [MaxLength(11)]
        [MinLength(11)]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// </summary>
        public string StatusMessage { get; set; }
    }
}