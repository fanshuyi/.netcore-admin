using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Platform.Controllers.CoursesControllers
{
    /// <summary>
    /// 发布的课程
    /// </summary>
    [Area("Platform")]
    [Authorize]
    public class PublishedCoursesController : Controller
    {
        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public IActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// 发布课程
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {


            return View();
        }
    }





}