using IServices.ISysServices;
using Microsoft.EntityFrameworkCore;
using Models.SysModels;
using Services.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Services.SysServices
{
    public class JsonDataService : RepositoryBase<JsonData>, IJsonDataService
    {
        private readonly IJsonDataHistoryService _jsonDataHistoryService;

        public JsonDataService(DbContext databaseFactory, IUserInfo userInfo, IJsonDataHistoryService jsonDataHistoryService)
            : base(databaseFactory, userInfo)
        {
            _jsonDataHistoryService = jsonDataHistoryService;
        }

        public IQueryable<JsonData> GetAll<T>()
        {
            return base.GetAll(a => a.JsonDataType == typeof(T).Name);
        }

        public IQueryable<JsonData> GetAll<T>(Expression<Func<JsonData, bool>> where)
        {
            return base.GetAll(a => a.JsonDataType == typeof(T).Name).Where(where);
        }

        public async Task<JsonData> SaveAsync(string id, object entity)
        {
            var item = new JsonData
            {
                JsonDataType = entity.GetType().Name
            };

            if (!string.IsNullOrEmpty(id))
            {
                item = await FindAsync(id);
                item.JsonDataStr = JsonConvert.SerializeObject(entity);
            }
            else
            {
                JObject jObj = JObject.Parse(JsonConvert.SerializeObject(entity));

                jObj["Id"] = item.Id;

                item.JsonDataStr = jObj.ToString();
            }

            //同时在备份数据
            await _jsonDataHistoryService.AddAsync(new JsonDataHistory()
            {
                RecordID = item.Id,
                JsonDataStr = item.JsonDataStr,
                JsonDataType = item.JsonDataType,
            });

            await base.SaveAsync(id, item);

            return item;
        }
    }

    public class JsonDataHistoryService : RepositoryBase<JsonDataHistory>, IJsonDataHistoryService
    {
        public JsonDataHistoryService(DbContext databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }
    }
}