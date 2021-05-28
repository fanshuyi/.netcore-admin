using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IServices.Infrastructure
{
    public interface IDapperRepository
    {
        /// <summary>
        /// 执行SQL基于dapper
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);

        /// <summary>
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> QueryAsync(string sql, object parameters = null);

        /// <summary>
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string sql, object parameters = null);
    }

    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        Task AddAsync(T entity);

        /// <summary>
        /// 添加多个
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddRangeAsync(T[] entities);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        Task Update(T entity);

        /// <summary>
        /// 更新多个
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateRange(T[] entity);

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        Task SaveAsync(object id, T entity);

        /// <summary>
        /// 按照单主键删除数据
        /// </summary>
        /// <param name="id"></param>
        Task Delete(object id);

        /// <summary>
        /// 按照单个实体删除数据
        /// </summary>
        /// <param name="item"></param>
        Task Delete(T item);

        /// <summary>
        /// 按照条件批量删除数据
        /// </summary>
        /// <param name="where"></param>
        Task Delete(Expression<Func<T, bool>> where);

        /// <summary>
        /// 根据单个主键获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindAsync(object id);

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <param name="allEnt">是否包含全部企业的数据</param>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="allEnt">查询全部企业数据</param>
        /// <returns></returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> where);
    }
}