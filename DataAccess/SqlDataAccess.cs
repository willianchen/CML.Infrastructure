using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.DataAccess
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：DataAccess.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：DataAccess
    /// 创建标识：cml 2017/7/6 11:37:46
    /// </summary>
    public class SqlDataAccess
    {
        private SqlConnection _connection;

        public SqlDataAccess() { }
        public SqlDataAccess(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            this.Open();
        }

        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
        }
        public bool IsClosed
        {
            get
            {
                return this.Connection.State.Equals(ConnectionState.Closed);
            }
        }
        public void Open()
        {
            if (this.IsClosed)
                this.Connection.Open();
        }

        public void Close()
        {
            this.Connection.Close();
        }

        public static object ExecuteScalar(string connectString, dynamic data, string table, IDbTransaction transaction = null
            , int? commandTimeout = null)
        {
            SqlDataAccess da = new SqlDataAccess(connectString);
                
            return null;
        }
    }
}
