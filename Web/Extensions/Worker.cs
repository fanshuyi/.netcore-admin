using IServices.ISysServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Extensions
{
    /// <summary>
    /// 后台任务
    /// </summary>
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private Timer _timer;
        private IServiceScopeFactory _IServiceScopeFactory;

        /// <summary>
        /// </summary>
        /// <param name="logger">
        /// </param>
        public Worker(ILogger<Worker> logger, IServiceScopeFactory iServiceScopeFactory)
        {
            _logger = logger;
            _IServiceScopeFactory = iServiceScopeFactory;
        }

        /// <summary>
        /// </summary>
        /// <param name="stoppingToken">
        /// </param>
        /// <returns>
        /// </returns>

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));           //每1分钟开启一个线程
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using var scope = _IServiceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IJsonDataService>();
            _logger.LogInformation("后台任务：" + DateTime.Now + "统计json数据" + context.GetAll().Count().ToString());
        }

        /// <summary>
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            _timer?.Dispose();
        }
    }
}