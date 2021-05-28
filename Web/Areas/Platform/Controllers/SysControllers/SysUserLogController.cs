using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
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
    public class SysUserLogController : Controller
    {
        private readonly ISysUserLogService _sysUserLogService;
        private readonly ILogger<SysUserLogController> _logger;

        /// <summary>
        /// </summary>
        /// <param name="sysUserLogService">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="logger">
        /// </param>
        public SysUserLogController(ISysUserLogService sysUserLogService, IUnitOfWork unitOfWork, ILogger<SysUserLogController> logger)
        {
            _sysUserLogService = sysUserLogService;
            _logger = logger;
        }

        // GET: /Platform/SysUserLog/

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
            var model = _sysUserLogService.GetAll().Select(a => new
            {
                UserCreatedBy = a.UserCreatedBy.UserName,
                SysArea = a.SysArea,
                SysController = a.SysController,
                SysAction = a.SysAction,
                ActionDuration = a.ActionDuration,
                ViewDuration = a.ViewDuration,
                Duration = a.Duration,
                RequestType = a.RequestType,
                Ip = a.Ip,
                Url = a.Url,
                a.CreatedDateTime,
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
            var item = await _sysUserLogService.FindAsync(id);
            return View(item);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<ActionResult> Delete()
        {
            throw new Exception("日志记录无法删除！");
        }
    }
}