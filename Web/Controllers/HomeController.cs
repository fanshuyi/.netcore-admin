using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class HomeController : Controller
    {



        /// <summary>
        /// 导师展示
        /// </summary>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> IndexAsync()
        {
            return RedirectToAction("Index", "Home", new { area = "Platform" });



        }

        public async Task<IActionResult> PrivacyAsync()
        {
            return View();
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            // Get the details of the exception that occurred
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var area = (string)RouteData.Values["Area"];

            // Get which route the exception occurred at
            string routeWhereExceptionOccurred = exceptionFeature?.Path;

            // Get the exception that occurred
            Exception exceptionThatOccurred = exceptionFeature?.Error;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Content(exceptionThatOccurred.Message);
            }
            else
            {
                return View("", exceptionThatOccurred.Message);
            }

            // TODO: Do something with the exception Log it with Serilog? Send an e-mail, text, fax,
            // or carrier pidgeon? Maybe all of the above? Whatever you do, be careful to catch any
            // exceptions, otherwise you'll end up with a blank page and throwing a 500
        }
    }
}