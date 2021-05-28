using IServices.ISysServices;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Services.Repository;

namespace Services.SysServices
{
    public class SysLogService : RepositoryBase<SysLog>, ISysLogService
    {
        public SysLogService(DbContext databaseFactory, IUserInfo userInfo)
               : base(databaseFactory, userInfo)
        {
        }
    }
}