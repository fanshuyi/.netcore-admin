using System.Linq;
using System.Threading.Tasks;
using IServices.ISysServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Services.SysServices;

namespace Web.SignalRHubs
{
    /// <summary>
    /// </summary>
    public class SysMessageService : SysMessageCenterService
    {
        private readonly IUserInfo _userInfo;
        private readonly IHubContext<MessengerHub> _messengerHub;
        private readonly UserManager<IdentityUser> _iSysUserService;

        /// <summary>
        /// </summary>
        /// <param name="databaseFactory"></param>
        /// <param name="userInfo"></param>
        /// <param name="messengerHub"></param>
        /// <param name="iSysUserService"></param>
        public SysMessageService(DbContext databaseFactory, IUserInfo userInfo, IHubContext<MessengerHub> messengerHub, UserManager<IdentityUser> iSysUserService) : base(databaseFactory, userInfo)
        {
            _userInfo = userInfo;
            _messengerHub = messengerHub;
            _iSysUserService = iSysUserService;
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        public override async Task AddAsync(SysMessageCenter entity)
        {
            if (string.IsNullOrEmpty(entity.AddresseeId))
            {
                //发给所有人
                foreach (var userId in _iSysUserService.Users.Select(a => a.Id))
                {
                    await _messengerHub.Clients.User(userId).SendAsync("newmessage", $"<a href='#/Platform/MessageCenter'>{entity.Title}</a>");
                }
            }
            else
            {
                await _messengerHub.Clients.User(entity.AddresseeId).SendAsync("newmessage", $"<a href='#/Platform/MessageCenter'>{entity.Title}</a>");
            }

            await base.AddAsync(entity);
        }
    }
}