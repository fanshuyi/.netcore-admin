using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.SysModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Extensions;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// </summary>
    [Area("Platform")]
    public class SysRoleController : Controller
    {
        private readonly ISysControllerService _sysControllerService;
        private readonly ISysRoleService _iSysRoleService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysRoleSysControllerSysActionService _sysRoleSysControllerSysActionService;
        private readonly IUserInfo _iUserInfo;
        private readonly ILogger<SysRoleController> _logger;
        private RoleManager<IdentityRole> roleManager;

        /// <summary>
        /// </summary>
        /// <param name="iSysRoleService">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="sysControllerService">
        /// </param>
        /// <param name="sysRoleSysControllerSysActionService">
        /// </param>
        /// <param name="iUserInfo">
        /// </param>
        public SysRoleController(ISysRoleService iSysRoleService, IUnitOfWork unitOfWork, ISysControllerService sysControllerService, ISysRoleSysControllerSysActionService sysRoleSysControllerSysActionService, IUserInfo iUserInfo, ILogger<SysRoleController> logger, RoleManager<IdentityRole> roleManager)
        {
            _iSysRoleService = iSysRoleService;
            _unitOfWork = unitOfWork;
            _sysControllerService = sysControllerService;
            _sysRoleSysControllerSysActionService = sysRoleSysControllerSysActionService;
            _iUserInfo = iUserInfo;
            _logger = logger;
            this.roleManager = roleManager;
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
        public async Task<IActionResult> IndexAsync(string keyword, string ordering, int pageIndex = 1, bool search = false, bool toExcelFile = false)
        {
            var model = roleManager.Roles.Select(a => new
            {
                a.Name,
                a.Id
            }).AsQueryable().Search(keyword);

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
        public async Task<IActionResult> DetailsAsync(string id)
        {
            var item = await _iSysRoleService.FindAsync(id);
            ViewBag.SysControllers = _sysControllerService.GetAll(a => a.Enable);
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
            var item = new IdentityRole();
            if (!string.IsNullOrEmpty(id))
            {
                item = await roleManager.FindByIdAsync(id);
            }

            ViewBag.SysControllers = _sysControllerService.GetAll(a => a.Enable);

            return View(item);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <param name="collection">
        /// </param>
        /// <param name="sysControllerSysActionsId">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(string id, IdentityRole collection, IEnumerable<string> sysControllerSysActionsId)
        {
            if (!ModelState.IsValid)
            {
                await Edit(id);
                return View(collection);
            }

            if (!string.IsNullOrEmpty(id))
            {
                var roleExis = await roleManager.RoleExistsAsync(collection.Name);

                if (!roleExis)
                {
                    //更新角色
                    await roleManager.UpdateAsync(collection);
                }

                //清除原有数据
                await _sysRoleSysControllerSysActionService.Delete(a => a.RoleId.Equals(id));
            }
            else
            {
                // 新建角色
                var identityResult = await roleManager.CreateAsync(collection);

                if (!identityResult.Succeeded)
                {
                    foreach (var error in identityResult.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }
            }

            if (sysControllerSysActionsId != null)
            {
                foreach (var sysControllerSysActionId in sysControllerSysActionsId)
                {
                    await _sysRoleSysControllerSysActionService.SaveAsync(null, new SysRoleSysControllerSysAction
                    {
                        RoleId = collection.Id,
                        SysControllerSysActionId = sysControllerSysActionId
                    });
                }
            }

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
            _iSysRoleService.Delete(a => id.Contains(a.Id));
            var result = await _unitOfWork.CommitAsync();
            return new DeleteSuccessResult(result);
        }
    }
}