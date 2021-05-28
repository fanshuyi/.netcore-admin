using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models.SysModels;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Extensions;
using Web.SignalRHubs;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// </summary>
    [Area("Platform")]
    [Authorize]
    public class MessageCenterController : Controller
    {
        private readonly ISysMessageCenterService _iSysMessageService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly ISysMessageReceivedService _iSysMessageReceivedService;
        private readonly IHubContext<MessengerHub> _hub;

        /// <summary>
        /// </summary>
        /// <param name="iSysMessageService">
        /// </param>
        /// <param name="iUserInfo">
        /// </param>
        /// <param name="iUnitOfWork">
        /// </param>
        /// <param name="iSysMessageReceivedService">
        /// </param>
        /// <param name="hub">
        /// </param>
        public MessageCenterController(ISysMessageCenterService iSysMessageService, IUserInfo iUserInfo, IUnitOfWork iUnitOfWork, ISysMessageReceivedService iSysMessageReceivedService, IHubContext<MessengerHub> hub)
        {
            _iSysMessageService = iSysMessageService;
            _iUserInfo = iUserInfo;
            _iUnitOfWork = iUnitOfWork;
            _iSysMessageReceivedService = iSysMessageReceivedService;

            _hub = hub;
        }

        // GET: Platform/MyMessage
        /// <summary>
        /// 我收到的消息列表
        /// </summary>
        /// <returns>
        /// </returns>
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1)
        {
            var model = _iSysMessageService.GetAll(a => a.AddresseeId == null || a.AddresseeId.Contains(_iUserInfo.UserId) && a.SysMessageReceiveds.All(b => b.Remark != "Deleted")).OrderBy(a => a.SysMessageReceiveds.Any(r => r.CreatedBy == _iUserInfo.UserId)).ThenByDescending(a => a.CreatedDateTime).Select(a => new { read = a.SysMessageReceiveds.Any(b => b.CreatedBy == _iUserInfo.UserId), a.Title, a.Content, a.UserCreatedBy, a.CreatedDateTime, a.Id }).Search(keyword);

            return View(model.PageResult(pageIndex, 20));
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<ActionResult> DetailsAsync(string id)
        {
            var item = await _iSysMessageService.FindAsync(id);

            //创建一条浏览记录
            await _iSysMessageReceivedService.AddAsync(new SysMessageReceived { SysMessageId = item.Id });

            await _iUnitOfWork.CommitAsync();

            // 更新用户页面统计数字

            await _hub.Clients.User(_iUserInfo.UserId).SendAsync("newmessage", "");

            if (!string.IsNullOrEmpty(item.Url))
            {
                return Redirect(item.Url);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            //创建一条删除记录
            await _iSysMessageReceivedService.AddAsync(new SysMessageReceived { SysMessageId = id, Remark = "Deleted" });

            // 更新用户页面统计数字
            await _hub.Clients.User(_iUserInfo.UserId).SendAsync("newmessage", "");

            var result = await _iUnitOfWork.CommitAsync();
            return new DeleteSuccessResult(result);
        }
    }
}