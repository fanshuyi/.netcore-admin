using IServices.ISysServices;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Services.Repository;

namespace Services.SysServices
{
    public class SysRoleSysControllerSysActionService : RepositoryBase<SysRoleSysControllerSysAction>,
        ISysRoleSysControllerSysActionService
    {
        public SysRoleSysControllerSysActionService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }
    }
}