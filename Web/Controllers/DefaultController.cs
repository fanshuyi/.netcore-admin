using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;

            //判断用户是否自行选择语言
            if (true)
            {
            }

            //如果没有 获取语言跳转
            return RedirectToAction("Index", "Home", new { culture });
        }
    }
}