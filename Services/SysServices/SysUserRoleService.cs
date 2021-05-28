

using IServices.ISysServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Services.Repository;

namespace Services.SysServices
{
    public class SysUserRoleService : RepositoryBase<IdentityUserRole<string>>, ISysUserRoleService
    {
        public SysUserRoleService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }
     
    
    }

    
}