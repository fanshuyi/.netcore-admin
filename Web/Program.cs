using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Web.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Web
{
    /// <summary>
    /// </summary>
    public class Program
    {
        /// <summary>
        /// </summary>
        /// <param name="args">
        /// </param>
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            await SeedData.EnsureSeedDataAsync(host.Services);

            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN");
            host.Run();
        }

        /// <summary>
        /// </summary>
        /// <param name="args">
        /// </param>
        /// <returns>
        /// </returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .ConfigureServices((hostContext, services) =>
                 {
                     // services.AddHostedService<Worker>();//后台任务
                 })
                .UseStartup<Startup>();
    }
}