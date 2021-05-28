using EasyCaching.Core;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Web.SignalRHubs;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// </summary>
    [Area("Platform")]
    [Authorize]
    public class DesktopController : Controller
    {
        private readonly ILogger<DesktopController> _logger;
        private readonly IUserInfo _iUserInfo;
        private readonly IConfiguration _configuration;
        private readonly IDapperRepository _iDapperRepository;
        private readonly ISysUserLogService _iSysUserLogService;
        private readonly ISysMessageCenterService _iSysMessageCenterService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHubContext<MessengerHub> _hub;

        private IEasyCachingProviderFactory _IEasyCachingProviderFactory;

        /// <summary>
        /// </summary>
        /// <param name="logger">
        /// </param>
        /// <param name="iUserInfo">
        /// </param>
        /// <param name="configuration">
        /// </param>
        /// <param name="iDapperRepository">
        /// </param>
        /// <param name="IUserFundsFlowService">
        /// </param>
        /// <param name="iSysUserLogService">
        /// </param>
        /// <param name="iSysMessageCenterService">
        /// </param>
        /// <param name="userManager">
        /// </param>
        /// <param name="hub">
        /// </param>
        /// <param name="iEnterpriseService">
        /// </param>
        /// <param name="iFreelanceService">
        /// </param>
        /// <param name="iEasyCachingProviderFactory">
        /// </param>
        public DesktopController(ILogger<DesktopController> logger, IUserInfo iUserInfo, IConfiguration configuration, IDapperRepository iDapperRepository, ISysUserLogService iSysUserLogService, ISysMessageCenterService iSysMessageCenterService, UserManager<IdentityUser> userManager, IHubContext<MessengerHub> hub, IEasyCachingProviderFactory iEasyCachingProviderFactory)
        {
            _logger = logger;
            _iUserInfo = iUserInfo;
            _configuration = configuration;
            _iDapperRepository = iDapperRepository;

            _iSysUserLogService = iSysUserLogService;
            _iSysMessageCenterService = iSysMessageCenterService;
            this.userManager = userManager;
            _hub = hub;

            _IEasyCachingProviderFactory = iEasyCachingProviderFactory;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> IndexAsync()
        {
            await _hub.Clients.User(_iUserInfo.UserId).SendAsync("welcome", "欢迎您访问系统");

            //用户数量
            ViewBag.SysUserCount = await userManager.Users.CountAsync();

            // 系统使用次数
            ViewBag.shiyong = await _iSysUserLogService.GetAll().CountAsync();

            //执行速度
            ViewBag.SysUserLogDayDuration = _iDapperRepository.QueryAsync("select * from (SELECT top 10 'Keys'=CAST(createddatetime AS DATE ),'Value'=CEILING(AVG(Duration)) FROM sysuserlogs  GROUP BY CAST(createddatetime AS DATE ) order by keys desc) as temp order by keys").Result.ToDictionary(a => a.Keys.ToShortDateString(), b => b.Value);

            //使用次数 曲线图
            ViewBag.SysUserLogDayCount = _iDapperRepository.QueryAsync("select * from (SELECT top 10 'Keys'=CAST(createddatetime AS DATE ),'Value'=COUNT(*) FROM sysuserlogs  GROUP BY CAST(createddatetime AS DATE ) order by keys desc) as temp order by keys;").Result.ToDictionary(a => a.Keys.ToShortDateString(), b => b.Value);

            //当前访问的数据库大小
            ViewBag.DbSize = _iDapperRepository.QueryAsync($"sp_spaceused").Result.FirstOrDefault()?.database_size.ToString();

            //当前用户未读消息数量
            ViewBag.MessageUnread = await _iSysMessageCenterService.GetAll(a => (a.AddresseeId == null || a.AddresseeId.Contains(_iUserInfo.UserId)) && a.SysMessageReceiveds.All(b => b.CreatedBy != _iUserInfo.UserId)).CountAsync();

            ViewBag.CacheCountInMemory = await _IEasyCachingProviderFactory.GetCachingProvider(EasyCachingConstValue.DefaultInMemoryName).GetCountAsync();

            ViewBag.CacheCountRedis = await _IEasyCachingProviderFactory.GetCachingProvider(EasyCachingConstValue.DefaultRedisName).GetCountAsync();

            return View();
        }
    }
}