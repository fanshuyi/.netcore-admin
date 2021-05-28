using IServices.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Models.SysModels;

namespace IServices.ISysServices
{
    public interface ISysRoleService :  IRepository<IdentityRole>
    {
      
    }


}