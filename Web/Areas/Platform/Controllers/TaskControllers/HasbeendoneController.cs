using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.Areas.Platform.Controllers.TaskControllers
{
    [Area("Platform")]
    public class HasbeendoneController : Controller
    {
        public IActionResult Index()
        {
            throw new Exception("Test");
        }
    }
}