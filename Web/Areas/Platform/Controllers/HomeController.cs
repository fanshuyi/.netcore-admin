using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// </summary>
    [Area("Platform")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISysControllerService _sysControllerService;
        private readonly IUserInfo _iUserInfo;
        private readonly ISysHelpService _iSysHelpService;
        private readonly UserManager<IdentityUser> _iSysUserService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly ISysMessageCenterService _iSysMessageCenterService;
        private ISysDepartmentSysUserService _ISysDepartmentSysUserService;

        /// <summary>
        /// </summary>
        /// <param name="sysControllerService">
        /// </param>
        /// <param name="iUserInfo">
        /// </param>
        /// <param name="iSysHelpService">
        /// </param>
        /// <param name="iSysUserService">
        /// </param>
        /// <param name="iUnitOfWork">
        /// </param>
        /// <param name="logger">
        /// </param>
        /// <param name="iSysMessageCenterService">
        /// </param>
        /// <param name="iSysDepartmentSysUserService">
        /// </param>
        /// <param name="signInManager">
        /// </param>
        public HomeController(ISysControllerService sysControllerService, IUserInfo iUserInfo, ISysHelpService iSysHelpService, UserManager<IdentityUser> iSysUserService, IUnitOfWork iUnitOfWork, ILogger<HomeController> logger, ISysMessageCenterService iSysMessageCenterService, ISysDepartmentSysUserService iSysDepartmentSysUserService, SignInManager<IdentityUser> signInManager)
        {
            _sysControllerService = sysControllerService;
            _iUserInfo = iUserInfo;
            _iSysHelpService = iSysHelpService;
            _iSysUserService = iSysUserService;
            _iUnitOfWork = iUnitOfWork;
            _logger = logger;
            _iSysMessageCenterService = iSysMessageCenterService;
            _ISysDepartmentSysUserService = iSysDepartmentSysUserService;
            _signInManager = signInManager;
        }

        /// <summary>
        /// </summary>
        /// <param name="DepartmentId">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> IndexAsync(string DepartmentId)
        {
            var user = await _iSysUserService.GetUserAsync(HttpContext.User);

            var deps = _ISysDepartmentSysUserService.GetAll(a => a.SysUserId == _iUserInfo.UserId);

            //切换部门
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                deps = deps.Where(a => a.SysDepartment.SystemId == DepartmentId);
            }

            if (deps.Any())
            {
                var claims = await _iSysUserService.GetClaimsAsync(user);

                await _iSysUserService.RemoveClaimsAsync(user, claims.Where(a => a.Type == "DepartmentId"));

                //保存到用户信息
                await _iSysUserService.AddClaimAsync(user, new Claim("DepartmentId", deps.First().SysDepartment.SystemId));

                await _signInManager.SignInAsync(user, true);
            }

            var userroles = await _iSysUserService.GetRolesAsync(user);

            ViewBag.MessageCount = _iSysMessageCenterService.GetAll(a => (a.AddresseeId == null || a.AddresseeId.Contains(_iUserInfo.UserId)) && a.SysMessageReceiveds.All(b => b.CreatedBy != _iUserInfo.UserId)).Count();

            ViewBag.Menu = JsonConvert.SerializeObject(_sysControllerService.GetAll(a =>
               a.Display && a.Enable &&
               a.SysControllerSysActions.Any(
                   b => b.SysRoleSysControllerSysActions.Any(c => userroles.Any(d => d == c.IdentityRole.Name)
                       )) &&
               a.SysArea.AreaName.Equals("Platform")).Select(a => new { id = a.SystemId, pId = a.SystemId.Substring(0, a.SystemId.Length - 3), name = a.Name, url = a.ControllerName == null ? "javascript:;" : "#" + Url.Action(a.ActionName, a.ControllerName), target = "_self" }));

            ViewBag.OffsiderbarHelp = _iSysHelpService.GetAll();

            var nickName = HttpContext.User.FindFirstValue("Nickname");

            if (string.IsNullOrEmpty(nickName))
            {
                nickName = HttpContext.User.Identity.Name;
            }

            ViewBag.nickName = nickName;

            // 读取部门
            ViewBag.Department = new SelectList(_ISysDepartmentSysUserService.GetAll(a => a.SysUserId == _iUserInfo.UserId).Select(a => a.SysDepartment).OrderBy(a => a.SystemId), "SystemId", "Name", _iUserInfo.DepartmentId);

            _logger.LogInformation("Access Platform Successful!");

            return View();
        }
    }
}