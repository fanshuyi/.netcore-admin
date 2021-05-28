using IServices.ISysServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class FooterViewComponent : ViewComponent
    {
        private ISysHelpService _ISysHelpService;
        private ISysHelpClassService _ISysHelpClassService;

        public FooterViewComponent(ISysHelpService iSysHelpService, ISysHelpClassService iSysHelpClassService)
        {
            _ISysHelpService = iSysHelpService;
            _ISysHelpClassService = iSysHelpClassService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _ISysHelpClassService.GetAll(a => a.SystemId.StartsWith("800"));

            return View(model);
        }
    }
}