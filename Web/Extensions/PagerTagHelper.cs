using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Web.Extensions
{
    /// <inheritdoc />
    /// <summary>
    /// 分页标签
    /// </summary>
    public class PagerTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        /// <summary>
        /// </summary>
        /// <param name="urlHelperFactory">
        /// </param>
        public PagerTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        /// <summary>
        /// 视图内容
        /// </summary>
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// 分页参数
        /// </summary>
        public PagedResult PagerOption { get; set; }

        /// <summary>
        /// 是否使用ajax链接
        /// </summary>
        public bool PagerAjax { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="context">
        /// </param>
        /// <param name="output">
        /// </param>
        /// <returns>
        /// </returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            return base.ProcessAsync(context, output);
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        /// </param>
        /// <param name="output">
        /// </param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //构造分页样式
            //output.TagName = "div";

            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            var currentPage = PagerOption.CurrentPage;

            var pageSize = PagerOption.PageSize;

            var rowCount = PagerOption.RowCount;

            var pageCount = PagerOption.PageCount;

            var start = currentPage - 5 >= 1 ? currentPage - 5 : 1;
            var end = pageCount - start >= 10 ? start + 10 : pageCount;

            if (pageCount - currentPage < 5)
            {
                start = pageCount - 10;
                if (start < 1)
                {
                    start = 1;
                }
            }

            var vs = ViewContext.RouteData.Values;

            var queryString = ViewContext.HttpContext.Request.Query;

            foreach (var key in queryString)
                vs[key.Key] = key.Value;

            vs.Remove("X-Requested-With");
            vs.Remove("X-HTTP-Method-Override");
            vs.Remove("_");

            var builder = new StringBuilder();
            builder.AppendFormat("<ul class=\"list-inline\"><li class=\"list-inline-item\"><ul class=\"pagination\">");

            if (currentPage > 1)
            {
                vs["pageIndex"] = 1;

                builder.Append($"<li class=\"page-item\"><a class=\"page-link\" href='#{urlHelper.Action(vs["action"].ToString(), vs)}'>|<</a></li>");

                vs["pageIndex"] = currentPage - 1;

                builder.Append($"<li class=\"page-item\"><a  class=\"page-link\" href='#{urlHelper.Action(vs["action"].ToString(), vs)}'><</a></li>");
            }

            for (var i = start; i <= end; i++) //前后各显示5个数字页码
            {
                vs["pageIndex"] = i;

                builder.Append(i == currentPage ? "<li class=\"page-item active\">" : "<li>");

                builder.Append($"<a class=\"page-link\" href='#{urlHelper.Action(vs["action"].ToString(), vs)}'>{i}</a></li>");
            }

            if (currentPage * pageSize < rowCount)
            {
                vs["pageIndex"] = currentPage + 1;

                builder.Append($"<li class=\"page-item\"><a  class=\"page-link\" href='#{urlHelper.Action(vs["action"].ToString(), vs)}'>></a></li>");

                vs["pageIndex"] = pageCount;

                builder.Append($"<li class=\"page-item\"><a  class=\"page-link\" href='#{urlHelper.Action(vs["action"].ToString(), vs)}'>>|</a></li>");
            }

            builder.Append($"</ul></li><li class=\"list-inline-item\">");//可以改成从资源文件读取 适应多语言

            builder.Append($"<span>每页{pageSize}条/共{rowCount}条 第{currentPage}页/共{pageCount}页</span></li></ul>");

            var buliderstring = builder.ToString();

            if (!PagerAjax)
            {
                buliderstring = buliderstring.Replace("#", "");
            }

            output.Content.SetHtmlContent(buliderstring);

            base.Process(context, output);
        }
    }
}