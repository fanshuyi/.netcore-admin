using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.SysModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Extensions
{
    /// <summary>
    /// 记录用户日志
    /// </summary>
    public class UserLogFilter : ActionFilterAttribute
    {
        private readonly ISysControllerSysActionService _iSysControllerSysActionService;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly ISysUserLogService _sysUserLogService;

        /// <summary>
        /// </summary>
        /// <param name="iSysControllerSysActionService">
        /// </param>
        /// <param name="iUnitOfWork">
        /// </param>
        /// <param name="sysUserLogService">
        /// </param>
        public UserLogFilter(ISysControllerSysActionService iSysControllerSysActionService, IUnitOfWork iUnitOfWork, ISysUserLogService sysUserLogService)
        {
            _iSysControllerSysActionService = iSysControllerSysActionService;
            _iUnitOfWork = iUnitOfWork;
            _sysUserLogService = sysUserLogService;
        }

        private DateTime _actiondatetimenow;

        private DateTime _viewdatetimenow;

        private double _actionDuration;

        #region Action时间监控

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="filterContext">
        /// </param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _actiondatetimenow = DateTime.Now;

            base.OnActionExecuting(filterContext);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _actiondatetimenow = DateTime.Now;

            return base.OnActionExecutionAsync(context, next);
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            _viewdatetimenow = DateTime.Now;

            return base.OnResultExecutionAsync(context, next);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="filterContext">
        /// </param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            _actionDuration = Math.Round((DateTime.Now - _actiondatetimenow).TotalMilliseconds, 3);
        }

        #endregion Action时间监控

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="filterContext">
        /// </param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            _viewdatetimenow = DateTime.Now;
            base.OnResultExecuting(filterContext);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="filterContext">
        /// </param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var viewDuration = Math.Round((DateTime.Now - _viewdatetimenow).TotalMilliseconds, 3);

            //记录用户访问记录
            var area = (string)filterContext.RouteData.Values["area"];
            var action = (string)filterContext.RouteData.Values["action"];
            var controller = (string)filterContext.RouteData.Values["controller"];


            var sysuserlog = new SysUserLog
            {
                Ip = filterContext.HttpContext.Connection.RemoteIpAddress.ToString(),
                Url = filterContext.HttpContext.Request.GetDisplayUrl(),

                ViewDuration = viewDuration,
                ActionDuration = _actionDuration,
                Duration = Math.Round((DateTime.Now - _actiondatetimenow).TotalMilliseconds, 3),
                RequestType = filterContext.HttpContext.Request.Method,
            };

            var sysControllerSysAction = _iSysControllerSysActionService.GetAll(a => a.SysController.ControllerName.Equals(controller) && a.SysController.SysArea.AreaName.Equals(area) && a.SysAction.ActionName.Equals(action)).OrderBy(a => a.SysController.SystemId).Select(a => new { a.Id, SysAreaName = a.SysController.SysArea.Name, SysControllerName = a.SysController.Name, SysActionName = a.SysAction.Name }).FirstOrDefault();


            if (sysControllerSysAction != null)
            {
                sysuserlog.SysArea = sysControllerSysAction?.SysAreaName ?? area;
                sysuserlog.SysController = sysControllerSysAction?.SysControllerName ?? controller;
                sysuserlog.SysAction = sysControllerSysAction?.SysActionName ?? action;
            }


            _sysUserLogService.SaveAsync(null, sysuserlog).Wait();

            _iUnitOfWork.CommitAsync().Wait();

            base.OnResultExecuted(filterContext);
        }
    }
}