using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Platform.Controllers.CoursesControllers
{
    /// <summary>
    /// 购买的课程
    /// </summary>
    [Area("Platform")]
    [Authorize]
    public class PurchasedCoursesController : Controller
    {
        /// <summary>
        /// 我已经购买的课程
        /// </summary>
        /// <returns>
        /// </returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}