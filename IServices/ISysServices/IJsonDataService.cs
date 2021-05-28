using IServices.Infrastructure;
using Models.SysModels;
using Models.TaskModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IServices.ISysServices
{
    public interface IJsonDataService : IRepository<JsonData>
    {
        IQueryable<JsonData> GetAll<T>();

        IQueryable<JsonData> GetAll<T>(Expression<Func<JsonData, bool>> where);

        Task<JsonData> SaveAsync(string id, object entity);
    }

    public interface IJsonDataHistoryService : IRepository<JsonDataHistory>
    {
    }
}