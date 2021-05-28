using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Mvc;
using Models.SysModels;
using Newtonsoft.Json;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Extensions;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// </summary>
    [Area("Platform")]
    public class JsonDataController : Controller
    {
        private readonly IJsonDataService _iJsonDataService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// </summary>
        /// <param name="iJsonDataService"></param>
        /// <param name="unitOfWork"></param>

        public JsonDataController(IJsonDataService iJsonDataService, IUnitOfWork unitOfWork)
        {
            _iJsonDataService = iJsonDataService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="ordering"></param>
        /// <param name="pageIndex"></param>
        /// <param name="search"></param>
        /// <param name="toExcelFile"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string keyword, string ordering, int pageIndex = 1, bool search = false, bool toExcelFile = false)
        {
            var model =
                _iJsonDataService.GetAll()
                                 .Select(
                                     a =>
                                     new
                                     {
                                         a.JsonDataType,
                                         a.JsonDataStr,
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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DetailsAsync(string id)
        {
            var item = await _iJsonDataService.FindAsync(id);
            return View(item);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            return RedirectToAction("Edit");
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            var item = new JsonData();
            if (!string.IsNullOrEmpty(id))
            {
                item = await _iJsonDataService.FindAsync(id);
            }
            return View(item);
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, JsonData collection)
        {
            // 通过JsonConvert验证
            try
            {
                JsonConvert.DeserializeObject(collection.JsonDataStr);
            }
            catch (JsonException e)
            {
                ModelState.AddModelError("JsonDataStr", e.Message);
            }

            // 通过数据库验证
            //if (_IRepository.ExecuteScalarAsync<int>($"SELECT ISJSON('{collection.JsonDataStr}')").Result == 0)
            //{
            //    ModelState.AddModelError("JsonDataStr", "格式错误！");
            //}

            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            if (!string.IsNullOrEmpty(id))
            {
                await _iJsonDataService.Update(collection);
            }
            else
            {
                await _iJsonDataService.AddAsync(collection);
            }

            await _unitOfWork.CommitAsync();

            return new EditSuccessResult(id);
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(string[] id)
        {
            _iJsonDataService.Delete(a => id.Any(b => b == a.Id));
            var result = await _unitOfWork.CommitAsync();
            return new DeleteSuccessResult(result);
        }
    }
}