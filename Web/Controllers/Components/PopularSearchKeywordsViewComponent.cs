using IServices.ISysServices;
using IServices.IUserServices;
using Microsoft.AspNetCore.Mvc;
using Models.SysModels;
using Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class PopularSearchKeywordsViewComponent : ViewComponent
    {
        private ISysHelpService _ISysHelpService;
        private ISysHelpClassService _ISysHelpClassService;
        private IDomainLabelService _IDomainLabelService;

        public PopularSearchKeywordsViewComponent(ISysHelpService iSysHelpService, ISysHelpClassService iSysHelpClassService, IDomainLabelService iDomainLabelService)
        {
            _ISysHelpService = iSysHelpService;
            _ISysHelpClassService = iSysHelpClassService;
            _IDomainLabelService = iDomainLabelService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _IDomainLabelService.GetAll(a => a.LabelType == LabelTypes.用户搜索);

            return View(model);
        }
    }
}