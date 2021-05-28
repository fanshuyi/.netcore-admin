using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Helpers;

namespace Web.Extensions
{



    /// <inheritdoc />
    /// <summary>
    /// 扩展添加和编辑后的返回结果，提示和跳转
    /// </summary>
    public class EditSuccessResult : ActionResult
    {
        private RouteValueDictionary RouteValues { get; set; }
        private object Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="routeValues"></param>
        public EditSuccessResult(object id, RouteValueDictionary routeValues = null)
        {
            Id = id;
            RouteValues = routeValues ?? new RouteValueDictionary();
        }



        //public override void ExecuteResult(ActionContext context)
        //{
        //    foreach (var key in context.HttpContext.Request.Query)
        //    {
        //        RouteValues.Add(key.Key, key.Value);
        //    }

        //    RouteValues["action"] = Id == null ? "Create" : "Index";

        //    var result = new RedirectToRouteResult(RouteValues);

        //    var factory = context.HttpContext.RequestServices.GetRequiredService<ITempDataProvider>();

        //    var iStringLocalizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<Resources.Lang>>();

        //    var tempdata = new Dictionary<string, object> { { AlertType.Alerts.Success, Id == null ? iStringLocalizer["AddSuccess"].ToString() : iStringLocalizer["EditSuccess"].ToString() } };

        //    factory.SaveTempData(context.HttpContext, tempdata);

        //    result.ExecuteResult(context);

        //    //base.ExecuteResult(context);
        //}

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

            RouteValues["action"] = Id == null ? "Create" : "Index";

            var result = new RedirectToRouteResult(RouteValues);

            var factory = context.HttpContext.RequestServices.GetRequiredService<ITempDataProvider>();

            var tempdata = new Dictionary<string, object> { { AlertType.Alerts.Success, Id == null ? "添加成功" : "编辑成功" } };

            factory.SaveTempData(context.HttpContext, tempdata);

            return result.ExecuteResultAsync(context);

        }
    }
}
