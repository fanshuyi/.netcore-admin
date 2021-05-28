using Dapper;
using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class DapperRepository : IDapperRepository
    {
        private readonly IDbConnection _IDbConnection;

        private readonly ILogger<DapperRepository> _logger;

        public DapperRepository(ILogger<DapperRepository> logger, IDbConnection iDbConnection)
        {
            _logger = logger;
            _IDbConnection = iDbConnection;
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            _logger.LogInformation(sql + parameters);
            return _IDbConnection.QueryAsync<T>(sql, parameters);
        }

        public Task<IEnumerable<dynamic>> QueryAsync(string sql, object parameters = null)
        {
            _logger.LogInformation(sql + parameters);
            return _IDbConnection.QueryAsync(sql, parameters);
        }

        public Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null)
        {
            return _IDbConnection.QueryMultipleAsync(sql, param);
        }

        public Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            _logger.LogInformation(sql + parameters);
            return _IDbConnection.ExecuteAsync(sql, parameters);
        }
    }

    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dataContext;
        private readonly IUserInfo _userInfo;

        protected RepositoryBase(DbContext databaseFactory, IUserInfo userInfo)
        {
            _dataContext = databaseFactory;
            _userInfo = userInfo;
        }

        /// <inheritdoc/>
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        public virtual async Task AddAsync(T entity)
        {
            await _dataContext.AddAsync(entity);
        }

        /// <summary>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task AddRangeAsync(T[] entities)
        {
            await _dataContext.AddRangeAsync(entities);
        }

        /// <inheritdoc/>
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public virtual async Task Update(T entity)
        {
            _dataContext.Update(entity);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task UpdateRange(T[] entity)
        {
            _dataContext.UpdateRange(entity);
        }

        /// <inheritdoc/>
        /// <summary>
        /// 添加或者更新
        /// </summary>
        /// <param name="id">实体主键ID</param>
        /// <param name="entity">实体</param>
        public virtual async Task SaveAsync(object id, T entity)
        {
            if (id != null)
            {
                await Update(entity);
            }
            else
            {
                await AddAsync(entity);
            }
        }

        /// <inheritdoc/>
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="remove">物理删除标记 默认false</param>
        public virtual async Task Delete(object id)
        {
            _dataContext.Remove(FindAsync(id).Result);
        }

        /// <inheritdoc/>
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="item"></param>
        public virtual async Task Delete(T item)
        {
            _dataContext.Remove(item);
        }

        /// <inheritdoc/>
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where"></param>
        public virtual async Task Delete(Expression<Func<T, bool>> where)
        {
            _dataContext.RemoveRange(GetAll(where));
        }

        /// <summary>
        /// 获取单个记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> FindAsync(object id)
        {
            var item = await _dataContext.FindAsync<T>(id);
            return item;
        }

        /// <inheritdoc/>
        /// <summary>
        /// 获取符合条件的用户所在企业数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="allEnt">查询全部企业数据</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> where)
        {
            return GetAll().Where(where);
        }

        /// <inheritdoc/>
        /// <summary>
        /// 获取用户所在企业数据
        /// </summary>
        /// <param name="allEnt">查询全部企业数据</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            var model = _dataContext.Set<T>().AsQueryable();

            var paramterExpression = Expression.Parameter(typeof(T));

            if (typeof(IDbSetBase).IsAssignableFrom(typeof(T)))
            {
                model = model.OrderByDescending(Expression.Lambda<Func<T, DateTimeOffset>>(Expression.PropertyOrField(paramterExpression, "CreatedDateTime"),
                    paramterExpression));
            }

            if (typeof(IUserDictionary).IsAssignableFrom(typeof(T)))
            {
                model = model.OrderBy(Expression.Lambda<Func<T, string>>(Expression.PropertyOrField(paramterExpression, "SystemId"),
                    paramterExpression));
            }

            return model;
        }

        /// <summary>
        /// 获取我所在部门的数据
        /// </summary>
        /// <returns></returns>

        public virtual IQueryable<T> GetDepAll()
        {
            var model = GetAll();

            if (typeof(IDbSetBase).IsAssignableFrom(typeof(T)))
            {
                var paramterExpression = Expression.Parameter(typeof(T));

                model = model.Where(Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.PropertyOrField(paramterExpression, "DepartmentId"), Expression.Constant(_userInfo.DepartmentId)), paramterExpression));
            }

            return model;
        }
    }
}