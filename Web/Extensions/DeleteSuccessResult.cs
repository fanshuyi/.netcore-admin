using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Helpers;

namespace Web.Extensions
{

    /// <inheritdoc />
    /// <summary>
    /// 扩展删除后的返回结果，提示和跳转
    /// </summary>
    public class DeleteSuccessResult : ActionResult
    {

        private RouteValueDictionary RouteValues { get; set; }

        /// <summary>
        /// 跳转控制器名称
        /// </summary>
        private string Index { get; set; }

        /// <summary>
        /// 删除行数
        /// </summary>
        private int Resule { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="index"></param>
        /// <param name="routeValues"></param>
        public DeleteSuccessResult(int result ,string index = "Index", RouteValueDictionary routeValues = null)
        {
            Index = index;
            Resule = result;
            RouteValues = routeValues ?? new RouteValueDictionary();
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ActionContext context)
        {
            foreach (var key in context.HttpContext.Request.Query)
            {
                RouteValues.Add(key.Key, key.Value);
            }

            RouteValues["action"] = Index;

            var result = new RedirectToRouteResult(RouteValues);

            var factory = context.HttpContext.RequestServices.GetRequiredService<ITempDataProvider>();

            var tempdata = new Dictionary<string, object> { { AlertType.Alerts.Success, string.Format("删除成功{0}行！", Resule) } };

            factory.SaveTempData(context.HttpContext, tempdata);

            result.ExecuteResult(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ExecuteResultAsync(ActionContext context)
        {
            foreach (var key in context.HttpContext.Request.Query)
            {
                RouteValues.Add(key.Key, key.Value);
            }

            RouteValues["action"] = Index;

            var result = new RedirectToRouteResult(RouteValues);

            var factory = context.HttpContext.RequestServices.GetRequiredService<ITempDataProvider>();

            var tempdata = new Dictionary<string, object> { { AlertType.Alerts.Success, string.Format("删除成功{0}行！", Resule) } };

            factory.SaveTempData(context.HttpContext, tempdata);

            return result.ExecuteResultAsync(context);
        }

    }

}
