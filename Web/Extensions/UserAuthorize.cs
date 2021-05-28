using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IServices.ISysServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Logging;

namespace Web.Extensions
{
    /// <summary>
    /// </summary>
    public interface IUserAuthorize
    {
        /// <summary>
        /// </summary>
        /// <param name="user">
        /// </param>
        /// <param name="area">
        /// </param>
        /// <param name="controller">
        /// </param>
        /// <param name="action">
        /// </param>
        /// <returns>
        /// </returns>
        bool Check(System.Security.Claims.ClaimsPrincipal user, string area, string controller, string action);
    }

    /// <summary>
    /// </summary>
    public class UserAuthorize : IUserAuthorize
    {
        private readonly ISysRoleSysControllerSysActionService _ISysRoleSysControllerSysActionService;
        private readonly UserManager<IdentityUser> sysUserService;

        /// <summary>
        /// </summary>
        /// <param name="iSysRoleSysControllerSysActionService">
        /// </param>
        /// <param name="sysUserService">
        /// </param>
        public UserAuthorize(ISysRoleSysControllerSysActionService iSysRoleSysControllerSysActionService, UserManager<IdentityUser> sysUserService)
        {
            _ISysRoleSysControllerSysActionService = iSysRoleSysControllerSysActionService;
            this.sysUserService = sysUserService;
        }

        public bool Check(System.Security.Claims.ClaimsPrincipal user, string area, string controller, string action)
        {
            // 检查  controller action 是否存在

            if (Assembly.GetExecutingAssembly().GetTypes().Any(
                type => type.IsSubclassOf(typeof(Controller)) && type.Name == controller + "Controller" && type.GetMethods().Any(m => (m.Name == action || m.Name == action + "Async") && m.IsPublic == true)))
            {
                var identityUser = sysUserService.GetUserAsync(user).Result;

                var roles = sysUserService.GetRolesAsync(identityUser).Result;

                return _ISysRoleSysControllerSysActionService.GetAll(a => roles.Any(b => b == a.IdentityRole.Name)).Any(b => b.SysControllerSysAction.SysController.SysArea.AreaName.Equals(area) &&
                                b.SysControllerSysAction.SysController.ControllerName.Equals(controller) &&
                                b.SysControllerSysAction.SysAction.ActionName.Equals(action));
            }

            return false;
        }
    }

    /// <summary>
    /// 用户身份验证
    /// </summary>
    public class UserAuthorizeFilter : AuthorizeFilter
    {
        /// <summary>
        /// </summary>
        /// <param name="policy">
        /// </param>
        public UserAuthorizeFilter(AuthorizationPolicy policy) : base(policy)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="authorizeData">
        /// </param>
        public UserAuthorizeFilter(IEnumerable<IAuthorizeData> authorizeData) : base(authorizeData)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="policy">
        /// </param>
        public UserAuthorizeFilter(string policy) : base(policy)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="policyProvider">
        /// </param>
        /// <param name="authorizeData">
        /// </param>
        public UserAuthorizeFilter(IAuthorizationPolicyProvider policyProvider, IEnumerable<IAuthorizeData> authorizeData) : base(policyProvider, authorizeData)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        /// </param>
        /// <returns>
        /// </returns>
        public override Task OnAuthorizationAsync(Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext context)
        {
            var iLogger = context.HttpContext.RequestServices.GetService(typeof(ILogger<UserAuthorizeFilter>)) as ILogger<UserAuthorizeFilter>;

            var area = (string)context.RouteData.Values["area"];

            if (string.IsNullOrEmpty(area) || context.HttpContext.RequestServices.GetService(typeof(ISysAreaService)) is not ISysAreaService iSysAreaService || !iSysAreaService.GetAll(a => a.AreaName == area).Any())
            {
                return Task.FromResult(0);
            }

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // 用户未登录
                context.Result = new UnauthorizedResult();
                return Task.FromResult(0);
            }

            //判断当前用户权限
            var controller = (string)context.RouteData.Values["controller"];
            var action = (string)context.RouteData.Values["action"];

            var sysUserService = context.HttpContext.RequestServices.GetService(typeof(UserManager<IdentityUser>)) as UserManager<IdentityUser>;

            var user = sysUserService.GetUserAsync(context.HttpContext.User).Result;

            if (user != null && context.HttpContext.RequestServices.GetService(typeof(RoleManager<IdentityRole>)) is RoleManager<IdentityRole> sysRoleService && sysUserService != null && context.HttpContext.RequestServices.GetService(typeof(ISysRoleSysControllerSysActionService)) is ISysRoleSysControllerSysActionService iSysRoleSysControllerSysActionService)
            {
                var roles = sysUserService.GetRolesAsync(user).Result;

                if (iSysRoleSysControllerSysActionService.GetAll(a => roles.Any(b => b == a.IdentityRole.Name)).Any(b => b.SysControllerSysAction.SysController.SysArea.AreaName.Equals(area) &&
                            b.SysControllerSysAction.SysController.ControllerName.Equals(controller) &&
                            b.SysControllerSysAction.SysAction.ActionName.Equals(action)))
                {
                    return base.OnAuthorizationAsync(context);
                }
            }

            // 用户无权限
            context.Result = new BadRequestObjectResult("用户：" + user.UserName + "(" + user.Id + ") 没有权限访问 " + area + " > " + controller + " > " + action + " ！请联系系统管理员进行权限分配！");
            return Task.FromResult(0);
        }
    }
}