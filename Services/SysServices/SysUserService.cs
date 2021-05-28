using IServices.ISysServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Services.Repository;

namespace Services.SysServices
{
    public class SysUserService : RepositoryBase<IdentityUser>, ISysUserService
    {
        public SysUserService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }
    }
}