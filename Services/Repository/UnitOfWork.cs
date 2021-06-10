using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dataContext;
        private readonly IUserInfo _userInfo;

        public UnitOfWork(DbContext context, IUserInfo userInfo)
        {
            _dataContext = context ?? throw new ArgumentNullException(nameof(context));
            _userInfo = userInfo;
            //_dataContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public Task<int> CommitAsync()
        {
            var newRecords = _dataContext.ChangeTracker.Entries()
               .Where(x => x.State == EntityState.Added && x.Entity is IDbSetBase)
               .Select(x => x.Entity as IDbSetBase);
            foreach (var record in newRecords)
            {
                if (record == null) continue;
                record.CreatedDateTime = DateTimeOffset.Now;
                record.CreatedBy = _userInfo.UserId;
                record.DepartmentId = _userInfo.DepartmentId;
            }

            var updatedRecords = _dataContext.ChangeTracker.Entries()
             .Where(x => x.State == EntityState.Modified && x.Entity is IDbSetBase)
             .Select(x => x.Entity as IDbSetBase);
            foreach (var record in updatedRecords)
            {
                if (record == null) continue;
                _dataContext.Entry(record).Property("CreatedDateTime").IsModified = false;
                _dataContext.Entry(record).Property("CreatedBy").IsModified = false;
                record.UpdatedDateTime = DateTimeOffset.Now;
                record.UpdatedBy = _userInfo.UserId;
            }

            var deletedRecords = _dataContext.ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Deleted)
            .Select(x => x);
            foreach (var record in deletedRecords)
            {
                if (record == null) continue;

                //record.Property("IsDeleted").CurrentValue = true;
                //record.Property("DeletedDateTime").CurrentValue = DateTimeOffset.Now;
                record.Property("DeletedBy").CurrentValue = _userInfo.UserId;

                // record.State = EntityState.Modified;
            }

            return _dataContext.SaveChangesAsync();
        }
    }
}