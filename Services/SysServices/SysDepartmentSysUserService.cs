
using IServices.ISysServices;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Services.Repository;

namespace Services.SysServices
{
    public class SysDepartmentSysUserService : RepositoryBase<SysDepartmentSysUser>, ISysDepartmentSysUserService
    {
        public SysDepartmentSysUserService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

    }
}