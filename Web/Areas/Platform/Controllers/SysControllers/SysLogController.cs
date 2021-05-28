using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Extensions;

namespace Web.Areas.Platform.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [Area("Platform")]
    public class SysLogController : Controller
    {
        private readonly ISysLogService _sysLogService;

        /// <summary>
        /// </summary>
        /// <param name="sysLogService">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        public SysLogController(ISysLogService sysLogService, IUnitOfWork unitOfWork)
        {
            _sysLogService = sysLogService;
        }

        // GET: /Platform/SysLog/

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
        public ActionResult Index(string keyword, string ordering, int pageIndex = 1, bool search = false, bool toExcelFile = false)
        {
            var model =
                _sysLogService.GetAll()
                                 .Select(
                                     a =>
                                     new
                                     {
                                         a.MachineName,
                                         a.Log,
                                         a.CreatedDateTime,
                                         a.Id
                                     }).Search(keyword);

            model = model.OrderByDescending(a => a.CreatedDateTime);
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
            var item = await _sysLogService.FindAsync(id);
            return View(item);
        }
    }
}