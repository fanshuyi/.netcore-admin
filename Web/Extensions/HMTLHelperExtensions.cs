using IServices.ISysServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Extensions
{
    /// <summary>
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// 将数据变成 无限分类下拉框
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">需要实现 IUserDictionary</param>
        /// <param name="id">选中值的ID</param>
        /// <returns></returns>
        public static IEnumerable<IUserDictionary> ToSystemIdSelectList<T>(this IQueryable<T> model, string id)
        {
            var systemIdSelectList = model as IEnumerable<IUserDictionary>;

            foreach (var item in systemIdSelectList ?? throw new InvalidOperationException())
            {
                item.Selected = id == item.Id;
            }
            return systemIdSelectList;
        }

        public static IEnumerable<IUserDictionary> ToSystemIdMultipleSelectList<T>(this IQueryable<T> model, string[] id)
        {
            var systemIdSelectList = model as IEnumerable<IUserDictionary>;

            foreach (var item in systemIdSelectList ?? throw new InvalidOperationException())
            {
                item.Selected = id.Any(a => item.Id == a);
            }
            return systemIdSelectList;
        }

        /// <summary>
        /// 判断是否当前action
        /// </summary>
        /// <param name="html"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string IsActive(this IHtmlHelper html, string controller = null, string action = null)
        {
            const string activeClass = "active"; // change here if you another name to activate sidebar items
            // detect current app state
            var actualAction = (string)html.ViewContext.RouteData.Values["action"];
            var actualController = (string)html.ViewContext.RouteData.Values["controller"];

            if (string.IsNullOrEmpty(controller))
                controller = actualController;

            if (string.IsNullOrEmpty(action))
                action = actualAction;

            return (controller == actualController && action == actualAction) ? activeClass : string.Empty;
        }

        /// <summary>
        /// 获取当前控制器中文名称
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ThisControllerName(this IHtmlHelper html)
        {
            var iSysControllerService = html.ViewContext.HttpContext.RequestServices.GetRequiredService<ISysControllerService>();

            var area = (string)html.ViewContext.RouteData.Values["area"];

            var controller = (string)html.ViewContext.RouteData.Values["controller"];

            return iSysControllerService.GetAll(a => a.ControllerName == controller && a.SysArea.AreaName == area).Select(a => a.Name).FirstOrDefault();
        }

        /// <summary>
        /// 去除html代码
        /// </summary>
        /// <param name="html"></param>
        /// <param name="length">截取长度</param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(this string html, int length = 0)
        {
            if (string.IsNullOrEmpty(html))
            {
                return "";
            }

            var strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");

            if (string.IsNullOrEmpty(strText))
            {
                return "";
            }

            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

            return length > 0 ? strText.CutString(length) : strText;
        }

        /// <summary>
        /// 截取指定长度的字符串
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="len">要截取的长度</param>
        /// <param name="flag">截取后是否加省略号(true加,false不加)</param>
        /// <returns></returns>
        public static string CutString(this string str, int len, bool flag = true)
        {
            if (string.IsNullOrEmpty(str.Trim()))
            {
                return "";
            }

            var outString = "";
            var strlen = 0;
            for (var i = 0; i < str.Length; i++)
            {
                if (char.ConvertToUtf32(str, i) >= Convert.ToInt32("4e00", 16) &&
                    char.ConvertToUtf32(str, i) <= Convert.ToInt32("9fff", 16))
                {
                    strlen += 2;
                    if (strlen > len) //截取的长度若是最后一个占两个字节，则不截取
                    {
                        break;
                    }
                }
                else
                {
                    strlen++;
                }

                try
                {
                    outString += str.Substring(i, 1);
                }
                catch
                {
                    break;
                }
                if (strlen >= len)
                {
                    break;
                }
            }
            if (str != outString && flag) //判断是否添加省略号
            {
                outString += "...";
            }
            return outString;
        }
    }
}