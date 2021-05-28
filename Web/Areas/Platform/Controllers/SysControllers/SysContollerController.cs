using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Mvc;
using Models.SysModels;
using OfficeOpenXml;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Extensions;
using Web.Helpers;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// 系统模块
    /// </summary>
    [Area("Platform")]
    public class SysControllerController : Controller
    {
        private readonly ISysControllerService _iSysControllerService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// </summary>
        /// <param name="iSysControllerService">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        public SysControllerController(ISysControllerService iSysControllerService, IUnitOfWork unitOfWork)
        {
            _iSysControllerService = iSysControllerService;
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
        /// <param name="pageSize">
        /// </param>
        /// <param name="search">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> Index(string keyword, string ordering, int pageIndex = 1, int pageSize = 20, bool search = false, bool toExcelFile = false)
        {
            var model =
                _iSysControllerService.GetAll()
                                 .Select(
                                     a =>
                                     new
                                     {
                                         a.Name,
                                         a.SystemId,
                                         a.Enable,
                                         a.SysArea.AreaName,
                                         a.ControllerName,
                                         a.ActionName,
                                         a.Parameter,
                                         a.Display,
                                         a.TargetBlank,
                                         a.Ico,

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

            return View(model.PageResult(pageIndex, pageSize));
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> Details(string id)
        {
            var item = await _iSysControllerService.FindAsync(id);

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
        /// 导入数据
        /// </summary>
        /// <param name="file">
        /// </param>
        /// <returns>
        /// </returns>

        [HttpPost]
        public async Task<IActionResult> Create(string file)
        {
            var excel = new ExcelPackage(Request.Form.Files.First().OpenReadStream());

            for (var i = 2; i <= excel.Workbook.Worksheets[0].Dimension.End.Row; i++)
            {
                await _iSysControllerService.SaveAsync(null, new SysController()
                {
                    Name = excel.Workbook.Worksheets[0].Cells[i, 1].Value.ToString(),
                    SystemId = excel.Workbook.Worksheets[0].Cells[i, 2].Value.ToString(),
                });
            }

            await _unitOfWork.CommitAsync();

            TempData[AlertType.Alerts.Success] = "数据导入成功！";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> Edit(string id)
        {
            var item = new SysController();
            if (!string.IsNullOrEmpty(id))
            {
                item = await _iSysControllerService.FindAsync(id);
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
        public async Task<IActionResult> Edit(string id, SysController collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            await _iSysControllerService.SaveAsync(id, collection);

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
            _iSysControllerService.Delete(a => id.Contains(a.Id));
            var result = await _unitOfWork.CommitAsync();
            return new DeleteSuccessResult(result);
        }
    }
}