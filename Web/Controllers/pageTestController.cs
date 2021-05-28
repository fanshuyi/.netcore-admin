using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using IServices.ISysServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class pageTestController : Controller
    {
        private ISysUserLogService _ISysUserLogService;
        private ISysLogService _ISysLogService;

        public pageTestController(ISysUserLogService iSysUserLogService, ISysLogService iSysLogService)
        {
            _ISysUserLogService = iSysUserLogService;
            _ISysLogService = iSysLogService;
        }

        public async Task<IActionResult> Index()
        {
            var startdatetime = DateTime.Now;

            var model = _ISysUserLogService.GetAll().PageResult(1, 20);


            ViewBag.time = (DateTime.Now - startdatetime).TotalMilliseconds.ToString(CultureInfo.InvariantCulture);

            return View(model);
        }

        public IActionResult Index1()
        {
            var model = _ISysLogService.GetAll().PageResult(1, 20);

            return View(model);
        }
    }
}