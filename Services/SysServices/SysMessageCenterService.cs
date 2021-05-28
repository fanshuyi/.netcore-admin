
using System;
using IServices.ISysServices;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Services.Repository;
using System.Linq;
using System.Linq.Expressions;

namespace Services.SysServices
{

    public class SysMessageCenterService : RepositoryBase<SysMessageCenter>, ISysMessageCenterService
    {
        public SysMessageCenterService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
         
        }

     
    }
}