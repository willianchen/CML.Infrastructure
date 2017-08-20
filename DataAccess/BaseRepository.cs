using System;
using System.Collections.Generic;
using CML.Infrastructure.Result;

namespace CML.Infrastructure.DataAccess
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
        /// 删除记录
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int Delete(object condition)
        {
            SqlQuery query = SqlQueryUtil.BuildDelete(condition, _tableName);
            return GetDataAccess().ExecuteNonQuery(query);
        }

        public TDto GetDto<TDto>(object condition, string[] ignoreFields = null, bool isWrite = false)
        {
            throw new NotImplementedException();
        }

        public T GetInfo(object condition, string[] ignoreFields = null, bool isWrite = false)
        {
            throw new NotImplementedException();
        }

        public int QueryCount(object condition, bool isWrite = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryList(bool isWrite = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryList(object condition, bool isWrite = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TDto> QueryList<TDto>(string[] ignoreFields = null, bool isWrite = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TDto> QueryList<TDto>(object condition, string[] ignoreFields = null, bool isWrite = false)
        {
            throw new NotImplementedException();
        }

        public IPageResult<TModel> QueryPageList<TModel>(string selectColumn, string selectTable, string where, string order,int pageIndex,int pageSize, object cmdParms = null)
        {
            throw new NotImplementedException();
        }

        public int Update(object data, object condition, string[] ignoreFields = null)
        {
            throw new NotImplementedException();
        }
    }
}
