using AutoMapper;
using IServices;
using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Models.SysModels;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Areas.Platform.Models;
using Web.Extensions;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Area("Platform")]
    public class SysUserController : Controller
    {
        private readonly ISysDepartmentService _iDepartmentService;
        private readonly IUserInfo _iUserInfo;
        private readonly RoleManager<IdentityRole> _sysRoleService;
        private readonly ISysUserRoleService _iSysUserRoleService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _iEmailSender;
        private readonly ISysUserService SysUserService;
        private readonly ILogger<SysUserController> _logger;
        private readonly ISysDepartmentSysUserService _iSysDepartmentSysUserService;

        /// <summary>
        /// </summary>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="sysRoleService">
        /// </param>
        /// <param name="iDepartmentService">
        /// </param>
        /// <param name="iUserInfo">
        /// </param>
        /// <param name="iSysDepartmentSysUserService">
        /// </param>
        /// <param name="userManager">
        /// </param>
        /// <param name="iEmailSender">
        /// </param>
        /// <param name="iSysUserRoleService">
        /// </param>
        /// <param name="sysUserService">
        /// </param>
        /// <param name="logger">
        /// </param>
        public SysUserController(IUnitOfWork unitOfWork, RoleManager<IdentityRole> sysRoleService,
            ISysDepartmentService iDepartmentService, IUserInfo iUserInfo, UserManager<IdentityUser> userManager, IEmailSender iEmailSender, ISysUserRoleService iSysUserRoleService, ISysUserService sysUserService, ILogger<SysUserController> logger, ISysDepartmentSysUserService iSysDepartmentSysUserService)
        {
            _unitOfWork = unitOfWork;
            _sysRoleService = sysRoleService;
            _iDepartmentService = iDepartmentService;
            _iUserInfo = iUserInfo;
            _userManager = userManager;
            _iEmailSender = iEmailSender;
            _iSysUserRoleService = iSysUserRoleService;
            SysUserService = sysUserService;
            _logger = logger;
            _iSysDepartmentSysUserService = iSysDepartmentSysUserService;
        }

        /// <summary>
        /// 用户列表
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
        public async Task<ActionResult> Index(string keyword, string ordering, int pageIndex = 1, bool search = false, bool toExcelFile = false)
        {
            var model = SysUserService.GetAll().Select(a => new
            {
                a.UserName,
                Roles = string.Join(", ", _userManager.GetRolesAsync(a).GetAwaiter().GetResult()),
                PhoneNumber = a.PhoneNumberConfirmed ? a.PhoneNumber : "",
                Email = a.EmailConfirmed ? a.Email : "",
                a.TwoFactorEnabled,
                a.LockoutEnabled,
                a.LockoutEnd,
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
                model = model.OrderBy(a => a.UserName);
            }
            if (toExcelFile)
            {
                return model.ToExcelFile();
            }

            return View(model.PageResult(pageIndex, 20));
        }

        /// <summary>
        /// 查看用户详细信息
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> DetailsAsync(string id)
        {
            var item = await _userManager.FindByIdAsync(id);

            var config = new MapperConfiguration(a => a.CreateMap<IdentityUser, SysUserDetailsModel>());

            var aa = config.CreateMapper().Map<SysUserDetailsModel>(item);

            ViewBag.DepartmentId = string.Join(",", _iDepartmentService.GetAll(a => a.SysDepartmentSysUsers.Any(b => b.SysUserId == item.Id)).Select(a => a.Name));

            ViewBag.SysRolesId = string.Join(",", await _userManager.GetRolesAsync(item));

            return View(aa);
        }

        /// <summary>
        /// 新建用户
        /// </summary>
        /// <returns>
        /// </returns>

        public async Task<IActionResult> Create()
        {
            return RedirectToAction("Edit");
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> EditAsync(string id)
        {
            //编辑用户所在部门和角色
            var item = new IdentityUser();

            if (!string.IsNullOrEmpty(id))
            {
                item = await _userManager.FindByIdAsync(id);
            }

            var depid = _iDepartmentService.GetAll(a => a.SysDepartmentSysUsers.Any(b => b.SysUserId.Equals(id))).Select(a => a.Id);

            ViewBag.DepartmentId = new MultiSelectList(_iDepartmentService.GetAll().Select(a => new { a.Id, Name = "(" + a.SystemId + ")" + a.Name }), "Id", "Name", depid);

            var roleIds = _iSysUserRoleService.GetAll(a => a.UserId.Equals(id)).Select(a => a.RoleId);

            ViewBag.SysRolesId = new MultiSelectList(_sysRoleService.Roles.Select(a => new { a.Id, a.Name }), "Id", "Name", roleIds);

            var config = new MapperConfiguration(a => a.CreateMap<IdentityUser, SysUserEditModel>());

            var aa = config.CreateMapper().Map<SysUserEditModel>(item);

            return View(aa);
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <param name="collection">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(string id, SysUserEditModel collection)
        {
            if (!ModelState.IsValid)
            {
                await EditAsync(id);

                ViewBag.SysRolesId = new MultiSelectList(_sysRoleService.Roles.Select(a => new { a.Id, a.Name }), "Id", "Name", collection.SysRolesId);

                ViewBag.DepartmentId = new MultiSelectList(_iDepartmentService.GetAll().Select(a => new { id = a.Id, Name = a.SystemId + a.Name }), "Id", "Name", collection.DepartmentId);

                return View(collection);
            }

            if (string.IsNullOrEmpty(id))
            {
                //新建用户

                var config = new MapperConfiguration(a => a.CreateMap<SysUserEditModel, IdentityUser>());

                var user = config.CreateMapper().Map<IdentityUser>(collection);

                var identityResult = await _userManager.CreateAsync(user, collection.Password);

                if (!identityResult.Succeeded)
                {
                    foreach (var error in identityResult.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }

                id = user.Id;
            }
            else
            {
                //更新用户
                var user = await _userManager.FindByIdAsync(id);

                var config = new MapperConfiguration(a => a.CreateMap<SysUserEditModel, IdentityUser>());

                config.CreateMapper().Map(collection, user);

                var identityResult = await _userManager.UpdateAsync(user);

                if (!identityResult.Succeeded)
                {
                    foreach (var error in identityResult.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }

                if (!string.IsNullOrEmpty(collection.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    identityResult = await _userManager.ResetPasswordAsync(user, token, collection.Password);

                    if (!identityResult.Succeeded)
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            throw new Exception(error.Description);
                        }
                    }
                }
            }

            //部门
            await _iSysDepartmentSysUserService.Delete(a => a.SysUserId == id);

            foreach (var DepartmentId in collection.DepartmentId)
            {
                await _iSysDepartmentSysUserService.SaveAsync(null, new SysDepartmentSysUser { SysDepartmentId = DepartmentId, SysUserId = id });
            }

            //删除角色
            await _iSysUserRoleService.Delete(a => a.UserId.Equals(id) && !collection.SysRolesId.Any(b => b == a.RoleId));

            //添加角色
            foreach (var s in collection.SysRolesId.Where(a => !_iSysUserRoleService.GetAll(b => b.UserId.Equals(id)).Any(c => c.RoleId == a)))
            {
                await _iSysUserRoleService.AddAsync(new IdentityUserRole<string>() { RoleId = s, UserId = id });
            }

            await _unitOfWork.CommitAsync();

            return new EditSuccessResult(id);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(string[] id)
        {
            foreach (var s in id)
            {
                var user = await _userManager.FindByIdAsync(s);
                await _userManager.DeleteAsync(user);
            }
            return new DeleteSuccessResult(id.Count());
        }
    }
}