using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AspNet.Docker.Integration.Helper;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Docker.Integration.Repository
{
    /// <summary>
    /// 泛型資料儲存庫介面
    /// </summary>
    /// <typeparam name="TEntity">實體資料型別</typeparam>
    /// <typeparam name="TDbContext"><see cref="DbContext"/> 實際型別</typeparam>
    public interface IGenericRepository<TEntity, TDbContext> : IDisposable
        where TEntity : class
        where TDbContext : DbContext
    {
        /// <summary>
        /// 新增一筆資料
        /// </summary>
        /// <param name="entity">欲新增之實體資料</param>
        /// <returns>是否新增成功</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// 更新一筆資料
        /// </summary>
        /// <param name="entity">欲更新的實體資料</param>
        /// <returns>是否更新成功</returns>
        bool Update(TEntity entity);

        /// <summary>
        /// 刪除一筆資料
        /// </summary>
        /// <param name="entity">欲刪除的實體資料</param>
        /// <returns>是否刪除成功</returns>
        bool Delete(TEntity entity);

        /// <summary>
        /// 取得符合條件的第一筆資料
        /// </summary>
        /// <param name="predicate">條件</param>
        /// <returns>符合條件的第一筆資料</returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 取得符合條件且包含指定內容的第一筆資料
        /// </summary>
        /// <param name="predicate">條件</param>
        /// <param name="includes">指定內容</param>
        /// <returns>符合條件且包含指定內容的第一筆資料</returns>
        TEntity GetInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// 取得包含指定內容的所有資料
        /// </summary>
        /// <param name="includes">指定內容</param>
        /// <returns>包含指定內容的所有資料</returns>
        List<TEntity> GetAllInclude(params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// 取得整個資料集合
        /// </summary>
        /// <returns>完整資料集</returns>
        List<TEntity> GetAll();
    }
}