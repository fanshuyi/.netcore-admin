

using IServices.ISysServices;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Services.Repository;

namespace Services.SysServices
{
    public class SysHelpClassService : RepositoryBase<SysHelpClass>, ISysHelpClassService
    {
        public SysHelpClassService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

    }
}