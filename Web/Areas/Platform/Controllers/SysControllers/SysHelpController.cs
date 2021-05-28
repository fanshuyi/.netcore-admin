using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Mvc;
using Models.SysModels;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Extensions;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// </summary>
    [Area("Platform")]
    public class SysHelpController : Controller
    {
        private readonly ISysHelpService _iSysHelpService;
        private readonly ISysHelpClassService _iSysHelpClassService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// </summary>
        /// <param name="iSysHelpService">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="iSysHelpClassService">
        /// </param>
        public SysHelpController(ISysHelpService iSysHelpService, IUnitOfWork unitOfWork, ISysHelpClassService iSysHelpClassService)
        {
            _iSysHelpService = iSysHelpService;
            _unitOfWork = unitOfWork;
            _iSysHelpClassService = iSysHelpClassService;
        }

        /// <summary>
        /// </summary>
        /// <param name="keyword">
        /// </param>
        /// <param name="ordering">
        /// </param>
        /// <param name="pageIndex">
        /// </param>
        /// <param name="search">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> Index(string keyword, string ordering, int pageIndex = 1, bool search = false, bool toExcelFile = false)
        {
            var model =
                _iSysHelpService.GetAll()
                                 .Select(
                                     a =>
                                     new
                                     {
                                         Class = a.SysHelpClass.Name,
                                         a.Title,
                                         a.Sort,
                                         CreatedBy = a.UserCreatedBy.UserName,
                                         a.CreatedDateTime,
                                         UpdatedBy = a.UserUpdatedBy.UserName,
                                         a.UpdatedDateTime,
                                         a.Remark,
                                         a.Id
                                     }).Search(keyword);
            if (search)
            {
                model = model.Search(Request.Query);
            }
            if (!string.IsNullOrEmpty(ordering))
            {
                model = model.OrderBy(ordering);
            }
            if (toExcelFile)
            {
                return model.ToExcelFile();
            }
            return View(model.PageResult(pageIndex, 20));
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> Details(string id)
        {
            var item = await _iSysHelpService.FindAsync(id);
            ViewBag.SysHelpClassId = item.SysHelpClass.ToString();
            return View(item);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> Create()
        {
            return RedirectToAction("Edit");
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> Edit(string id)
        {
            var item = new SysHelp();
            if (!string.IsNullOrEmpty(id))
            {
                item = await _iSysHelpService.FindAsync(id);
            }
            ViewBag.SysHelpClassId = _iSysHelpClassService.GetAll(a => a.Enable).ToSystemIdSelectList(item.SysHelpClassId);
            return View(item);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <param name="collection">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SysHelp collection)
        {
            if (!ModelState.IsValid)
            {
                await Edit(id);
                ViewBag.SysHelpClassId = _iSysHelpClassService.GetAll(a => a.Enable).ToSystemIdSelectList(collection.SysHelpClassId);
                return View(collection);
            }

            await _iSysHelpService.SaveAsync(id, collection);

            await _unitOfWork.CommitAsync();

            return new EditSuccessResult(id);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string[] id)
        {
            _iSysHelpService.Delete(a => id.Contains(a.Id));
            var result = await _unitOfWork.CommitAsync();
            return new DeleteSuccessResult(result);
        }
    }
}