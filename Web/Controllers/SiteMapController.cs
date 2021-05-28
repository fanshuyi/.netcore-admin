using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    /// <summary>
    /// 网站地图
    /// </summary>
    public class SiteMapController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns>
        /// </returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}