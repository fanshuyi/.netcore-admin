
using System.Threading.Tasks;
using IServices.ISysServices;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Services.Repository;

namespace Services.SysServices
{
    public class SysControllerSysActionService : RepositoryBase<SysControllerSysAction>, ISysControllerSysActionService
    {
        public SysControllerSysActionService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

    

    }
}