using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Microsoft.EntityFrameworkCore;
using Models.UserModels;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.UserServices
{
    public class CityCodeService : RepositoryBase<CityCode>, ICityCodeService
    {
        public CityCodeService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }
    }
}