using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Web.Extensions
{
    /// <summary>
    /// </summary>
    public sealed class AuthorizeForApiKeyAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The HTTP Header Name to be used in the request. The default is
        /// <code>x-api-key </code>
        /// </summary>
        public string HeaderName { get; set; } = "x-api-key";

        /// <summary>
        /// </summary>
        /// <param name="context">
        /// </param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var keyHeader = context.HttpContext.Request.Headers[HeaderName].ToString();
            if (string.IsNullOrEmpty(keyHeader))
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    //Content = "Access Key required"
                };

                return;
            }

            // Here should be your code to validate the key itself. I used the IoC feature in
            // ASP.NET Core to get the available Key Store
            var keystoreService = context.HttpContext.RequestServices.GetService<IKeyStoreService>();

            if (!keystoreService.IsValidKey(keyHeader))
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    //Content = "The provided key is either invalid or expired."
                };

                return;
            }

            base.OnActionExecuting(context);
        }
    }

    /// <summary>
    /// </summary>
    public interface IKeyStoreService
    {
        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        bool IsValidKey(string key);
    }

    /// <summary>
    /// </summary>
    public class InMemoryKeyStoreService : IKeyStoreService
    {
        private static Dictionary<string, DateTime> _keyStore = new Dictionary<string, DateTime>
        {
            { "a689a999a5e62055bda8c21b1dbe92c119308def1", new DateTime(2030,1,1) },
            { "0dca48f101f6458f456df25e62cc89d83b7c467c2", new DateTime(2030,6,1) },
            { "f58ab51695b501095418ac9718da10d0c70d4b593", new DateTime(2035,1,1) },
        };

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        public bool IsValidKey(string key)
        {
            return _keyStore.TryGetValue(key, out DateTime expiryDate) && expiryDate > DateTime.Now;
        }
    }
}