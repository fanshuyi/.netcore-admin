using IServices.ICourseServices;
using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Mvc;
using Models.CourseModels;
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
    /// 课程分类
    /// </summary>
    [Area("Platform")]
    public class CourseTypeController : Controller
    {
        private readonly ICourseTypeService _iCourseTypeService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// </summary>
        /// <param name="iCourseTypeService">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        public CourseTypeController(ICourseTypeService iCourseTypeService, IUnitOfWork unitOfWork)
        {
            _iCourseTypeService = iCourseTypeService;
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
            ViewBag.Import = true; //允许导入数据

            var model =
                _iCourseTypeService.GetAll()
                                 .Select(
                                     a =>
                                     new
                                     {
                                         a.Name,
                                         a.SystemId,
                                         a.Enable,
                                         a.Hot,
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
            var item = await _iCourseTypeService.FindAsync(id);

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
                await _iCourseTypeService.SaveAsync(null, new CourseType()
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
            var item = new CourseType();
            if (!string.IsNullOrEmpty(id))
            {
                item = await _iCourseTypeService.FindAsync(id);
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
        public async Task<IActionResult> Edit(string id, CourseType collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            await _iCourseTypeService.SaveAsync(id, collection);

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
            _iCourseTypeService.Delete(a => id.Contains(a.Id));
            var result = await _unitOfWork.CommitAsync();
            return new DeleteSuccessResult(result);
        }
    }
}