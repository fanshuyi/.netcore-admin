using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Web.LocalizationResources;

namespace Web.Extensions
{
    /// <summary>
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="model">
        /// </param>
        /// <param name="keywords">
        /// </param>
        /// <returns>
        /// </returns>
        public static IQueryable<T> Search<T>(this IQueryable<T> model, string keywords)
        {
            if (string.IsNullOrEmpty(keywords) || string.IsNullOrEmpty(keywords.Trim())) return model;

            foreach (var keyword in keywords.Split(new[] { '+', ' ' }))
            {
                if (string.IsNullOrEmpty(keyword)) continue;

                var where = model.GetType().GetGenericArguments()[0].GetProperties()
                    .Where(item => item.PropertyType == typeof(string)).Aggregate("1!=1 ",
                        (current, item) => current + " or " + item.Name + ".Contains(@0)");

                if (int.TryParse(keyword, out var intKeyword))
                {
                    where = model.GetType().GetGenericArguments()[0].GetProperties()
                        .Where(item => item.PropertyType == typeof(int)).Aggregate(where,
                            (current, item) => current + " or " + item.Name + "==" + intKeyword);
                }

                model = model.Where(where, keyword);
            }

            return model;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="model">
        /// </param>
        /// <param name="item">
        /// </param>
        /// <returns>
        /// </returns>
        public static IQueryable<T> Search<T>(this IQueryable<T> model, IQueryCollection item)
        {
            foreach (var title in typeof(T).GetProperties())
            {
                if (!string.IsNullOrEmpty(item[title.Name]))
                {
                    if (title.PropertyType == typeof(string))
                    {
                        if (item[title.Name + "_method"] == "Contains")
                        {
                            model = model.Where($"{title.Name}.Contains(@0)", item[title.Name]);
                        }

                        if (item[title.Name + "_method"] == "==")
                        {
                            model = model.Where($"{title.Name}==@0", item[title.Name]);
                        }

                        if (item[title.Name + "_method"] == "!=")
                        {
                            model = model.Where($"{title.Name}!=@0", item[title.Name]);
                        }
                    }

                    if (title.PropertyType == typeof(int) || title.PropertyType == typeof(double) ||
                        title.PropertyType == typeof(decimal) || title.PropertyType == typeof(float) ||
                        title.PropertyType == typeof(int?) || title.PropertyType == typeof(double?) ||
                        title.PropertyType == typeof(decimal?) || title.PropertyType == typeof(float?))
                    {
                        try
                        {
                            var met = item[title.Name + "_method"];

                            // 防止错误
                            if (met == "==" || met == ">" || met == "<" || met == ">=" || met == "<=")
                            {
                                model = model.Where(title.Name + met + item[title.Name]);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw new Exception("您输入的" + item[title.Name] + "有误！请重新检查输入的内容。");
                        }
                    }

                    if (title.PropertyType == typeof(DateTime) || title.PropertyType == typeof(DateTime?))
                    {
                        if (DateTime.TryParse(item[title.Name], out var datetime))
                        {
                            model = model.Where($"{title.Name}>=@0", datetime);
                        }
                        else
                        {
                            throw new Exception("您输入的" + item[title.Name] + "有误！请重新检查输入的内容。");
                        }

                        if (!string.IsNullOrEmpty(item[title.Name + "_End"]))
                        {
                            if (DateTime.TryParse(item[title.Name + "_End"], out var datetimeEnd))
                            {
                                model = model.Where($"{title.Name}<=@0", datetimeEnd);
                            }
                            else
                            {
                                throw new Exception("您输入的" + item[title.Name + "_End"] + "有误！请重新检查输入的内容。");
                            }
                        }
                    }

                    if (title.PropertyType == typeof(DateTimeOffset) || title.PropertyType == typeof(DateTimeOffset?))
                    {
                        if (DateTimeOffset.TryParse(item[title.Name], out var datetime))
                        {
                            model = model.Where($"{title.Name}>=@0", datetime);
                        }
                        else
                        {
                            throw new Exception("您输入的" + item[title.Name] + "有误！请重新检查输入的内容。");
                        }

                        if (!string.IsNullOrEmpty(item[title.Name + "_End"]))
                        {
                            if (DateTimeOffset.TryParse(item[title.Name + "_End"], out var datetimeEnd))
                            {
                                model = model.Where($"{title.Name}<=@0", datetimeEnd);
                            }
                            else
                            {
                                throw new Exception("您输入的" + item[title.Name + "_End"] + "有误！请重新检查输入的内容。");
                            }
                        }
                    }

                    if (title.PropertyType == typeof(bool) || title.PropertyType == typeof(bool?))
                    {
                        if (bool.TryParse(item[title.Name], out var boolvalue))
                        {
                            model = model.Where(title.Name + "==" + boolvalue);
                        }
                        else
                        {
                            throw new Exception("您输入的" + item[title.Name + "_End"] + "有误！请重新检查输入的内容。");
                        }
                    }

                    if (title.PropertyType.BaseType == typeof(Enum))
                    {
                        if (int.TryParse(item[title.Name], out var intvalue))
                        {
                            model = model.Where($"{title.Name}==@0", intvalue);
                        }
                        else
                        {
                            throw new Exception("您输入的" + item[title.Name] + "有误！请重新检查输入的内容。");
                        }
                    }
                }
            }

            return model;
        }

        /// <summary>
        /// IEnumerable To ExcelFile
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="model">
        /// </param>
        /// <returns>
        /// </returns>
        public static FileResult ToExcelFile<T>(this IEnumerable<T> model)
        {
            var pck = new ExcelPackage();

            var ws = pck.Workbook.Worksheets.Add(DateTime.Now.ToShortDateString());

            PropertyInfo[] properties = model.GetType().GetGenericArguments()[0].GetProperties();

            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(typeof(ExpressLocalizationResource));

            for (int i = 0; i < properties.Length; i++)

            {
                try
                {
                    ws.Cells[1, i + 1].Value = rm.GetString(properties[i].Name);
                }
                catch
                {
                    ws.Cells[1, i + 1].Value = properties[i].Name;
                }
            }

            ws.Cells["A2"].LoadFromCollection(model, false);

            return new FileContentResult(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}