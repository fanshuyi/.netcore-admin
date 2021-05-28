using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IServices.ISysServices;

namespace Web.Controllers
{
    public class HelpController : Controller
    {
        private ISysHelpService _ISysHelpService;

        public HelpController(ISysHelpService iSysHelpService)
        {
            _ISysHelpService = iSysHelpService;
        }

        public IActionResult Index()
        {
            var model = _ISysHelpService.GetAll(a => a.SysHelpClass.Name == "帮助中心").OrderBy(a => a.Sort);
            return View(model);
        }

        public async Task<IActionResult> DetailsAsync(string Id)
        {
            var item = await _ISysHelpService.FindAsync(Id);
            return View(item);
        }
    }
}