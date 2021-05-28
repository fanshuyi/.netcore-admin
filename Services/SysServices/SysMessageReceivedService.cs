using IServices.ISysServices;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Services.Repository;

namespace Services.SysServices
{
    public class SysMessageReceivedService : RepositoryBase<SysMessageReceived>, ISysMessageReceivedService
    {
        public SysMessageReceivedService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

    
    }
}