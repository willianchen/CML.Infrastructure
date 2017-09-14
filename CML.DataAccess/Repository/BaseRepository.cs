using System;
using System.Collections.Generic;
using CML.Infrastructure.Result;
using System.Text;

namespace CML.DataAccess
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：BaseRepositorycs.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：BaseRepositorycs
    /// 创建标识：cml 2017/7/6 15:34:50  
    /// </summary>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private string _tableName;
        private readonly string _configName;

        protected string TableName { get { return _tableName; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="configName"></param>
        public BaseRepository(string tableName, string configName)
        {
            _tableName = tableName;
            _configName = configName;
        }

        /// <summary>
        /// 获取数据操作
        /// </summary>
        /// <param name="isWrite">是否需要执行写操作</param>
        /// <returns>数据操作</returns>
        protected IDataAccess GetDataAccess(bool isWriter = false)
        {
            var dbProperty = DataBasePropertyFactory.GetDataBaseProperty(_configName);
            return new SqlDataAccess(dbProperty, isWriter);
        }

        /// <summary>
        /// 执行添加方法
        /// </summary>
        /// <param name="info"></param>
        /// <param name="isIdentity"></param>
        /// <returns></returns>
        public object Insert(T info, string[] ignoreFields = null, bool isIdentity = false)
        {
            SqlQuery query = SqlQueryUtil.BuildInsert(info, _tableName, ignoreFields: ignoreFields);
            if (isIdentity)
            {
                return GetDataAccess().ExecuteScalar<object>(query);
            }
            else
            {
                return GetDataAccess().ExecuteNonQuery(query);
            }
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="data"></param>
        /// <param name="condition"></param>
        /// <param name="ignoreFields"></param>
        /// <returns></returns>
        public int Update(object data, object condition, string[] ignoreFields = null)
        {
            SqlQuery query = SqlQueryUtil.BuildUpdate(data, condition, _tableName, ignoreFields);
            return GetDataAccess(true).ExecuteNonQuery(query);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int Delete(object condition)
        {
            SqlQuery query = SqlQueryUtil.BuildDelete(condition, _tableName);
            return GetDataAccess().ExecuteNonQuery(query);
        }

        /// <summary>
        /// 根据条件获取Dto
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="condition"></param>
        /// <param name="ignoreFields"></param>
        /// <param name="isWrite"></param>
        /// <returns></returns>
        public TDto GetDto<TDto>(object condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery<TDto>(condition, _tableName);
            return GetDataAccess(isWrite).QuerySingleOrDefault<TDto>(query);
        }

        /// <summary>
        /// 获取Info
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ignoreFields"></param>
        /// <param name="isWrite"></param>
        /// <returns></returns>
        public T GetInfo(object condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery(condition, _tableName, ignoreFields);
            return GetDataAccess().QuerySingleOrDefault<T>(query);
        }

        /// <summary>
        /// 查询数量结果
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="isWrite"></param>
        /// <returns></returns>
        public int QueryCount(object condition, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQueryCountSqlQuery(condition, _tableName);
            return GetDataAccess(isWrite).ExecuteScalar<int>(query);
        }

        protected int QueryCount(string selectTable, string where, object cmdParms = null, bool isWrite = false)
        {
            StringBuilder selectSQL = new StringBuilder();
            selectSQL.Append(string.Format("SELECT COUNT(0) FROM {0} ", selectTable));
            if (!string.IsNullOrWhiteSpace(where)) selectSQL.Append(string.Format(" WHERE {0}", where));
            SqlQuery query = new SqlQuery(selectSQL.ToString(), cmdParms);
            return GetDataAccess(isWriter: isWrite).ExecuteScalar<int>(query);
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="isWrite"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryList(bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery<T>(null, _tableName);
            return GetDataAccess(isWrite).Query<T>(query);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="isWrite"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryList(object condition, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery<T>(condition, _tableName);
            return GetDataAccess(isWrite).Query<T>(query);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="ignoreFields"></param>
        /// <param name="isWrite"></param>
        /// <returns></returns>
        public IEnumerable<TDto> QueryList<TDto>(string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery<TDto>(null, _tableName, ignoreFields);
            return GetDataAccess(isWrite).Query<TDto>(query);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="condition"></param>
        /// <param name="ignoreFields"></param>
        /// <param name="isWrite"></param>
        /// <returns></returns>
        public IEnumerable<TDto> QueryList<TDto>(object condition, string[] ignoreFields = null, bool isWrite = false)
        {
            SqlQuery query = SqlQueryUtil.BuilderQuerySqlQuery<TDto>(condition, _tableName, ignoreFields);
            return GetDataAccess(isWrite).Query<TDto>(query);
        }

        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="selectColumn"></param>
        /// <param name="selectTable"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public IPageResult<TModel> QueryPageList<TModel>(string selectColumn, string selectTable, string where, string order, int pageIndex, int pageSize, object cmdParms = null)
        {
            int totalCount = QueryCount(selectTable, where, cmdParms: cmdParms);
            var dataList = PageQuery<TModel>(selectColumn, selectTable, where, order, pageIndex, pageSize, cmdParms: cmdParms);
            return new PageResult<TModel>(pageIndex, pageSize, totalCount, dataList);
        }

        /// <summary>
        /// 获取分页查询
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="selectColumn"></param>
        /// <param name="selectTable"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public IEnumerable<TModel> PageQuery<TModel>(string selectColumn, string selectTable, string where, string order, int pageIndex, int pageSize, object cmdParms = null)
        {
            SqlQuery query = SqlQueryUtil.BuilderPageSqlQuery(selectColumn, selectTable, where, order, pageIndex, pageSize, cmdParams: cmdParms);
            return GetDataAccess().Query<TModel>(query);
        }

     
    }
}
