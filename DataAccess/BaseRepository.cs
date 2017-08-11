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
        private string _connectString;

        protected string TableName { get { return _tableName; } }
        protected string ConnectString { get { return _connectString; } }

        public BaseRepository(string tableName, string connectString)
        {
            _tableName = tableName;
            _connectString = connectString;
        }

        public long Insert(T info)
        {
            SqlDataAccess da = new SqlDataAccess(ConnectString);
            return da.Connection.Insert(info, TableName);
        }
    }
}
