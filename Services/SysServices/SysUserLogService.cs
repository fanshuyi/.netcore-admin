
using IServices.ISysServices;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Services.Repository;

namespace Services.SysServices
{
    public class SysUserLogService : RepositoryBase<SysUserLog>, ISysUserLogService
    {
        public SysUserLogService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }
     
    
    }

    
}