using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.SysModels;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Extensions;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// </summary>
    [Area("Platform")]
    public class HolidayController : Controller
    {
        private readonly IHolidayService _iHolidayService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJsonDataService jsonDataService;

        /// <summary>
        /// </summary>
        /// <param name="iHolidayService">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="jsonDataService">
        /// </param>
        public HolidayController(IHolidayService iHolidayService, IUnitOfWork unitOfWork, IJsonDataService jsonDataService)
        {
            _iHolidayService = iHolidayService;
            _unitOfWork = unitOfWork;
            this.jsonDataService = jsonDataService;
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
        /// <param name="toExcelFile">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> Index(string keyword, string ordering, int pageIndex = 1, bool search = false, bool toExcelFile = false)
        {
            var model = jsonDataService.GetAll<Holiday>()

                     .Select(
                                     a =>
                                     new
                                     {
                                         Title = SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Title"),
                                         Work = SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Work"),
                                         StartDate = DateTime.Parse(SqlDbFunctions.JsonValue(a.JsonDataStr, "$.StartDate")),
                                         EndDate = DateTime.Parse(SqlDbFunctions.JsonValue(a.JsonDataStr, "$.EndDate")),
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
            var jsonData = await jsonDataService.FindAsync(id);
            var item = JsonConvert.DeserializeObject<Holiday>(jsonData.JsonDataStr);
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
            var item = new Holiday();
            if (!string.IsNullOrEmpty(id))
            {
                item = JsonConvert.DeserializeObject<Holiday>(jsonDataService.FindAsync(id).Result.JsonDataStr);
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
        public async Task<IActionResult> EditAsync(string id, Holiday collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            await jsonDataService.SaveAsync(id, collection);
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
            jsonDataService.Delete(a => id.Contains(a.Id));
            var result = await _unitOfWork.CommitAsync();
            return new DeleteSuccessResult(result);
        }
    }
}