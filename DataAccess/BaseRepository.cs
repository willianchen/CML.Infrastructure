using CML.Infrastructure.DataAccess;
using CML.Infrastructure.DataAccess.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private DataBaseProperty _dbProperty;//数据库连接配置

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

            //SqlDataAccess da = new SqlDataAccess(ConnectString);
            //return da.Connection.Insert(info, TableName);
        }
    }
}
