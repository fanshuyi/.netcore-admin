using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Areas.Api.Models;
using Web.Extensions;
using Web.Helpers;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// 报告分类
    /// </summary>
    [Area("Platform")]
    public class DataDictionaryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJsonDataService jsonDataService;

        /// <summary>
        /// </summary>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="iCityCodeService">
        /// </param>
        /// <param name="iSysUserService">
        /// </param>
        public DataDictionaryController(IUnitOfWork unitOfWork, IJsonDataService jsonDataService)
        {
            _unitOfWork = unitOfWork;
            this.jsonDataService = jsonDataService;
        }

        /// <summary>
        /// </summary>
        /// <param name="keyword">搜索关键词
        /// </param>
        /// <param name="ordering">排序
        /// </param>
        /// <param name="pageIndex">当前页
        /// </param>
        /// <param name="pageSize">每页大小
        /// </param>
        /// <param name="search">多条件搜索
        /// </param>
        /// <param name="recyclebin">已删除的数据</param>
        /// <param name="toExcelFile">导出到excel</param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> IndexAsync(string keyword, string ordering, int pageIndex = 1, int pageSize = 20, bool search = false, bool recyclebin = false, bool toExcelFile = false)
        {
            ViewBag.Import = true; //允许导入数据

            var DataDictionarys = jsonDataService.GetAll<DataDictionary>();

            //数据回收站
            if (recyclebin)
            {
                DataDictionarys = DataDictionarys.IgnoreQueryFilters().Where(a => a.IsDeleted == true);
            }

            var model = DataDictionarys
                .Select(
                    a =>
                    new
                    {
                        Category = SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Category"),
                        Name = SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Name"),
                        SystemId = SqlDbFunctions.JsonValue(a.JsonDataStr, "$.SystemId"),
                        Enable = Convert.ToBoolean(SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Enable")),
                        Remark = SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Remark"),
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
            else
            {
                model = model.OrderBy(a => a.Category).ThenBy(a => a.SystemId);
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
            var item = JsonConvert.DeserializeObject<DataDictionary>(jsonData.JsonDataStr);
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
            var excel = new ExcelPackage(Request.Form.Files[0].OpenReadStream());

            var count = 0;

            for (var i = 2; i <= excel.Workbook.Worksheets[0].Dimension.End.Row; i++)
            {
                var dic = new DataDictionary()
                {
                    Category = excel.Workbook.Worksheets[0].Cells[i, 1].Text,
                    Name = excel.Workbook.Worksheets[0].Cells[i, 2].Text,
                    SystemId = excel.Workbook.Worksheets[0].Cells[i, 3].Text,
                    Enable = (bool)excel.Workbook.Worksheets[0].Cells[i, 4].Value,
                    Remark = excel.Workbook.Worksheets[0].Cells[i, 5].Text,
                };

                //去重

                if (!jsonDataService.GetAll<DataDictionary>(a => SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Category") == dic.Category && SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Name") == dic.Name && SqlDbFunctions.JsonValue(a.JsonDataStr, "$.SystemId") == dic.SystemId).Any())
                {
                    //添加
                    await jsonDataService.SaveAsync(null, dic);
                    count++;
                }
            }

            await _unitOfWork.CommitAsync();

            TempData[AlertType.Alerts.Success] = count + "行数据导入成功！";

            return RedirectToAction("Index");
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
            var item = new DataDictionary();
            if (!string.IsNullOrEmpty(id))
            {
                item = JsonConvert.DeserializeObject<DataDictionary>(jsonDataService.FindAsync(id).Result.JsonDataStr);
            }
            return View(item);
        }

        // POST: /Platform/iDepartment/Edit/5
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
        public async Task<IActionResult> EditAsync(string id, DataDictionary collection)
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
            await jsonDataService.Delete(a => id.Contains(a.Id));
            var result = await _unitOfWork.CommitAsync();
            return new DeleteSuccessResult(result);
        }
    }
}