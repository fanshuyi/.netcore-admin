
using IServices.ISysServices;
using IServices.IUserServices;
using Microsoft.EntityFrameworkCore;
using Models.UserModels;
using Services.Repository;
using System;
using System.Linq.Expressions;

namespace Services.UserServices
{
    public class DomainLabelService : RepositoryBase<DomainLabel>, IDomainLabelService
    {
        public DomainLabelService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }
  
    }
}