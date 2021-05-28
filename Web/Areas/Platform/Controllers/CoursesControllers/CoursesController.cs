using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// </summary>
    [Area("Platform")]
    [Authorize]
    public class CoursesController : Controller
    {
        /// <summary>
        /// 课程首页
        /// </summary>
        /// <returns>
        /// </returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}