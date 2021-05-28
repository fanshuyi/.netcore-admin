using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAssembly.Client.Shared
{
    public sealed partial class MainLayout
    {
        private bool UseTabSet { get; set; } = true;

        private string Theme { get; set; } = "";

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = true;

        private bool IsFullSide { get; set; } = true;

        private bool ShowFooter { get; set; } = true;

        private List<MenuItem> Menus { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            Menus = GetIconSideMenuItems();

            base.OnInitialized();
        }

        private static List<MenuItem> GetIconSideMenuItems()
        {
            var menus = new List<MenuItem>
{
                new MenuItem() { Text = "桌面", Icon = "fa fa-fw fa-check-square-o", Url = "" },
                new MenuItem() { Text = "用户管理", Icon = "fa fa-fw fa-check-square-o", Url = "counter" },
                new MenuItem() { Text = "角色管理", Icon = "fa fa-fw fa-database", Url = "fetchdata" },
                new MenuItem() { Text = "系统日志", Icon = "fa fa-fw fa-database", Url = "SysLog" },
            };

            return menus;
        }
    }
}