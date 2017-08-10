using CML.Infrastructure.Common;
using CML.Infrastructure.Extension;
using CML.Infrastructure.Utils;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CML.Infrastructure.MongoDB
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：MongoDBService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：MongoDB封装类
    /// 创建标识：cml 2017/5/31 13:57:47
    /// </summary>
    public class MongoDBService<T> where T : MongoDBEntity
    {
        private readonly string _connString = ConfigUtil.GetValue("MongoDb");
        private readonly MongoClient _mongoClient;
        private IMongoDatabase _db = null;
        private IMongoCollection<T> _collection = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MongoDBService()
        {
            _mongoClient = new MongoClient(_connString);
            var mongoDBAttribute = MongoDBAttribute.GetMongoDBAttribute<T>();
            if (mongoDBAttribute.IsNull())
                throw new ArgumentException("mongoDBAttribute不能为空");

            _db = _mongoClient.GetDatabase(mongoDBAttribute.Database);
            _collection = _db.GetCollection<T>(mongoDBAttribute.Collection);
        }

        #region 增

        /// <summary>
        /// 增（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsyn(T entity) 
        {
            await _collection.InsertOneAsync(entity).ConfigureAwait(false);
        }

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            AddAsyn(entity).Wait();
        }

        /// <summary>
        /// 批量增（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task BatchAddAsync(IList<T> entities)
        {
            await _collection.InsertManyAsync(entities).ConfigureAwait(false);
        }

        /// <summary>
        /// 批量增
        /// </summary>
        /// <param name="entities"></param>
        public void BatchAdd(IList<T> entities)
        {
            BatchAddAsync(entities).Wait();
        }

        #endregion

        #region 删

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public async Task<long> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _collection.DeleteManyAsync(predicate).ConfigureAwait(false);
            return result.DeletedCount;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<long> DeleteAsync(T entity)
        {
            return await DeleteAsync(s => s._id == entity._id);
        }

        #endregion

        #region 改

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<long> UpdateAsync(Expression<Func<T, bool>> predicate, T entity)
        {
            var updateDefinitionList = MongoDBUtil.BuilderDefinition<T>(entity);

            var updateDefinitionBuilder = new UpdateDefinitionBuilder<T>().Combine(updateDefinitionList);

            var result = await _collection.UpdateOneAsync<T>(predicate, updateDefinitionBuilder).ConfigureAwait(false);

            return result.ModifiedCount;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Update(Expression<Func<T, bool>> predicate, T entity)
        {
            return UpdateAsync(predicate, entity).Result;
        }

        #endregion

        #region 查
        /// <summary>
        /// 获取数据（异步）
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="project">字段</param>
        /// <returns></returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> project = null)
        {
            var find = _collection.Find(predicate);
            if (project.IsNotNull())
                find = find.Project(project);
            return await find.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="project">字段</param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> project = null)
        {
            return GetAsync(predicate, project).Result;
        }

        /// <summary>
        /// 获取列表（异步）
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="project">字段</param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> project = null, int limit = 0)
        {
            var find = _collection.Find(predicate);
            if (project.IsNotNull())
                find = find.Project(project);
            if (limit > 0)
                find = find.Limit(limit);

            return await find.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="project">字段</param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<T> List(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> project = null, int limit = 0)
        {
            return ListAsync(predicate, project, limit).Result;
        }

        #endregion

        #region 分页

        /// <summary>
        /// 分页（异步）
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="project"></param>
        /// <param name="orderby"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public async Task<PageList<T>> PageListAsync(Expression<Func<T, bool>> predicate, int page = 1, int pageSize = 20, Expression<Func<T, T>> project = null, Expression<Func<T, object>> orderby = null, bool desc = false)
        {
            long cnt = _collection.CountAsync(predicate).Result;
            var find = _collection.Find(predicate);
            if (project.IsNotNull())
                find = find.Project(project);
            if (orderby.IsNotNull())
                find = desc ? find.SortByDescending(orderby) : find.SortBy(orderby);
            find = find.Skip((page - 1) * pageSize).Limit(pageSize);
            var items = await find.ToListAsync().ConfigureAwait(false);

            return new PageList<T>(page, pageSize, cnt, items);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="project"></param>
        /// <param name="orderby"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public PageList<T> PageList(Expression<Func<T, bool>> predicate, int page = 1, int pageSize = 20, Expression<Func<T, T>> project = null, Expression<Func<T, object>> orderby = null, bool desc = false)
        {
            return PageListAsync(predicate, page, pageSize, project, orderby, desc).Result;
        }
        #endregion
    }
}
