

using IServices.ISysServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Services.Repository;

namespace Services.SysServices
{
    public class SysRoleService : RepositoryBase<IdentityRole>, ISysRoleService
    {
        public SysRoleService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }
     
    
    }

    
}