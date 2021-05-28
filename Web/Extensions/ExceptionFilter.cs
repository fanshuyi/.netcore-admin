using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Models;
using Services;
using System.Threading.Tasks;
using IServices;

namespace Web.Extensions
{
    /// <summary>
    /// </summary>
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IEmailSender _emailSender;
        private readonly AdministratorContact _options;

        /// <summary>
        /// </summary>
        /// <param name="emailSender"></param>
        /// <param name="options"></param>
        public ExceptionFilter(IEmailSender emailSender, IOptions<AdministratorContact> options)
        {
            _emailSender = emailSender;
            _options = options.Value;
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            //发送邮件 到管理员邮箱
            _emailSender.SendEmailAsync(_options.Email, "error", context.Exception.Message).GetAwaiter();
            base.OnException(context);
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            return base.OnExceptionAsync(context);
        }
    }
}