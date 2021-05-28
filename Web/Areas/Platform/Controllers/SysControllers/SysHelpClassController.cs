using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class SysHelpClassController : Controller
    {
        private readonly ISysHelpClassService _iSysHelpClassService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// </summary>
        /// <param name="iSysHelpClassService">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        public SysHelpClassController(ISysHelpClassService iSysHelpClassService, IUnitOfWork unitOfWork)
        {
            _iSysHelpClassService = iSysHelpClassService;
            _unitOfWork = unitOfWork;
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
                _iSysHelpClassService.GetAll()
                                 .Select(
                                     a =>
                                     new
                                     {
                                         a.Name,
                                         a.SystemId,
                                         a.Enable,
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
            var item = await _iSysHelpClassService.FindAsync(id);
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
            var item = new SysHelpClass();
            if (!string.IsNullOrEmpty(id))
            {
                item = await _iSysHelpClassService.FindAsync(id);
            }
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
        public async Task<IActionResult> Edit(string id, SysHelpClass collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            await _iSysHelpClassService.SaveAsync(id, collection);

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
        public async Task<IActionResult> DeleteAsync(string[] id)
        {
            _iSysHelpClassService.Delete(a => id.Contains(a.Id));
            var result = await _unitOfWork.CommitAsync();
            return new DeleteSuccessResult(result);
        }
    }
}