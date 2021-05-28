using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.ITaskServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Models.TaskModels;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Extensions;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// 待办事宜
    /// </summary>
    [Area("Platform")]
    public class TobedoneController : Controller
    {
        private readonly ITaskCenterService _iTaskCenterService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _iSysUserService;
        private readonly ISysMessageCenterService _iSysMessageService;

        /// <summary>
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="iTaskCenterService"></param>
        /// <param name="iSysUserService"></param>
        /// <param name="iSysMessageService"></param>
        public TobedoneController(IUnitOfWork unitOfWork, ITaskCenterService iTaskCenterService, UserManager<IdentityUser> iSysUserService, ISysMessageCenterService iSysMessageService)
        {
            _unitOfWork = unitOfWork;
            _iTaskCenterService = iTaskCenterService;
            _iSysUserService = iSysUserService;
            _iSysMessageService = iSysMessageService;
        }

        /// <summary>
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="ordering"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string keyword, string ordering, int pageIndex = 1, int pageSize = 20, bool search = false)
        {
            var model = _iTaskCenterService.GetAll()
                                 .Select(
                                     a =>
                                     new
                                     {
                                         a.TaskType,
                                         a.Title,
                                         a.UserCreatedBy,
                                         a.CreatedDateTime,
                                         a.UserUpdatedBy,
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

            return View(model.PageResult(pageIndex, pageSize));
        }

        // GET: /Platform/iDepartment/Details/5

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            var item = await _iTaskCenterService.GetAll().Include(a => a.UserCreatedBy).Include(a => a.UserUpdatedBy).FirstAsync(a => a.Id == id);
            return View(item);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            return RedirectToAction("Edit");
        }

        // GET: /Platform/iDepartment/Edit/5
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            var item = new TaskCenter();
            if (!string.IsNullOrEmpty(id))
            {
                item = await _iTaskCenterService.FindAsync(id);
            }
            ViewBag.TaskExecutorId = new SelectList(_iSysUserService.Users.Select(a => new { a.UserName, a.Id }), "Id", "UserName", item.TaskExecutorId);
            return View(item);
        }

        // POST: /Platform/iDepartment/Edit/5
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(string id, TaskCenter collection)
        {
            if (!ModelState.IsValid)
            {
                await Edit(id);
                return View(collection);
            }

            await _iTaskCenterService.SaveAsync(id, collection);

            await _iSysMessageService.AddAsync(new SysMessageCenter() { Title = collection.Title, Content = collection.Content, AddresseeId = collection.TaskExecutorId });

            await _unitOfWork.CommitAsync();

            return new EditSuccessResult(id);
        }

        // POST: /Platform/iDepartment/Delete/5
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _iTaskCenterService.Delete(id);
            var result = await _unitOfWork.CommitAsync();
            return new DeleteSuccessResult(result);
        }
    }
}