using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAssembly.Client.Pages
{
    public partial class Login
    {
        [NotNull]
        private LoginInputModel Model { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Model = new LoginInputModel();
        }

        private Task OnInvalidSubmit1(EditContext context)
        {
            return Task.CompletedTask;
        }

        private Task OnValidSubmit1(EditContext context)
        {
            return Task.CompletedTask;
        }
    }

    public class LoginInputModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "请填写用户名！")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请填写密码！")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}