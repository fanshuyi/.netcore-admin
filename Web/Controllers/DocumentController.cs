using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    /// <summary>
    /// 平台使用教程
    /// </summary>
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}