using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.UserModels;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Extensions;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// 知识库-内容
    /// </summary>
    [Area("Platform")]
    public class KnowledgeBaseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IJsonDataService jsonDataService;

        /// <summary>
        /// </summary>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="iCityCodeService">
        /// </param>
        /// <param name="iSysUserService">
        /// </param>
        public KnowledgeBaseController(IUnitOfWork unitOfWork, IJsonDataService jsonDataService)
        {
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
        /// <param name="pageSize">
        /// </param>
        /// <param name="search">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> IndexAsync(string keyword, string ordering, int pageIndex = 1, int pageSize = 20, bool search = false, bool toExcelFile = false)
        {
            var model = jsonDataService.GetAll<KnowledgeBase>()
                                 .Select(
                                     a =>
                                     new
                                     {
                                         KnowledgeCategory = SqlDbFunctions.JsonValue(a.JsonDataStr, "$.KnowledgeCategorys"),
                                         Title = SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Title"),
                                         Content = SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Content"),
                                         UsageCount = SqlDbFunctions.JsonValue(a.JsonDataStr, "$.UsageCount"),
                                         a.UserCreatedBy,
                                         a.CreatedDateTime,
                                         a.UserUpdatedBy,
                                         a.UpdatedDateTime,
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
        public async Task<IActionResult> DetailsAsync(string id)
        {
            var jsonData = await jsonDataService.FindAsync(id);
            var item = JsonConvert.DeserializeObject<KnowledgeBase>(jsonData.JsonDataStr);
            return View(item);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> CreateAsync()
        {
            return RedirectToAction("Edit");
        }

        // GET: /Platform/iDepartment/Edit/5
        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> EditAsync(string id)
        {
            var item = new KnowledgeBase();
            if (!string.IsNullOrEmpty(id))
            {
                item = JsonConvert.DeserializeObject<KnowledgeBase>(jsonDataService.FindAsync(id).Result.JsonDataStr);
            }

            ViewBag.KnowledgeCategorys = new MultiSelectList(jsonDataService.GetAll<KnowledgeCategory>(a => SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Enable") == "True").OrderBy(a => SqlDbFunctions.JsonValue(a.JsonDataStr, "$.SystemId")).Select(a => new { a.Id, Name = "(" + SqlDbFunctions.JsonValue(a.JsonDataStr, "$.SystemId") + ")" + SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Name"), }), "Name", "Name", item.KnowledgeCategorys);

            return View(item);
        }

        // POST: /Platform/iDepartment/Edit/5
        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>ß
        /// <param name="collection">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(string id, KnowledgeBase collection)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.KnowledgeCategorys = new MultiSelectList(jsonDataService.GetAll<KnowledgeCategory>(a => SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Enable") == "True").OrderBy(a => SqlDbFunctions.JsonValue(a.JsonDataStr, "$.SystemId")).Select(a => new { a.Id, Name = "(" + SqlDbFunctions.JsonValue(a.JsonDataStr, "$.SystemId") + ")" + SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Name"), }), "Name", "Name", collection.KnowledgeCategorys);

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
            await jsonDataService.Delete(a => id.Contains(a.Id));
            var result = await _unitOfWork.CommitAsync();
            return new DeleteSuccessResult(result);
        }
    }
}