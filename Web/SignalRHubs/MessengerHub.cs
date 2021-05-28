using IServices.ISysServices;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Web.SignalRHubs
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class MessengerHub : Hub, IMessengerHub
    {
        private readonly ISysMessageCenterService _iSysMessageCenterService;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSysMessageCenterService"></param>
        public MessengerHub(ISysMessageCenterService iSysMessageCenterService)
        {
            _iSysMessageCenterService = iSysMessageCenterService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            //发送未读消息数量

            //Clients.User(Context.Connection.UserIdentifier).SendAsync("usermessagecount", $"{_iSysMessageCenterService.GetUnread()}");

            return base.OnConnectedAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }





    }
}
