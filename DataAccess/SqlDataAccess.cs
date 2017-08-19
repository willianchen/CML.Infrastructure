using Dapper;
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
    public class SqlDataAccess : IDataAccess
    {
        protected int _cmdTimeout = 3000;//超时时间
        protected IDbConnection _connection;//连接
        protected IDbTransaction _tran;//事务
        protected DataBaseType _dbType;//数据库类型

        // private SqlConnection _connection;

        public SqlDataAccess() { }
        public SqlDataAccess(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            this.Open();
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="dbProperty">数据库属性信息</param>
        /// <param name="isWrite">是否写连接（默认使用读连接）</param>
        public SqlDataAccess(DataBaseProperty dbProperty, bool isWrite = false)
        {
            //EnsureUtil.NotNull(dbProperty, "dbProperty不能为空!");
            DataBaseConnection dbConnection = isWrite ? dbProperty.Writer : dbProperty.Reader;
            _connection = CreateConnection(dbConnection);
        }

        /// <summary>
        /// 根据连接信息创建连接对象
        /// </summary>
        /// <param name="dbConnection">连接信息</param>
        /// <returns>连接对象</returns>
        private IDbConnection CreateConnection(DataBaseConnection dbConnection)
        {
            IDbConnection conn;
            _dbType = dbConnection.DatabaseType;
            switch (dbConnection.DatabaseType)
            {
                case DataBaseType.MSSqlServer:
                    conn = new SqlConnection(dbConnection.ConnectionString);
                    break;

                //case DataBaseType.PostgreSql:
                //    conn = new NpgsqlConnection(dbConnection.ConnectionString);
                //    break;

                default:
                    throw new NotSupportedException("DatabaseType NotSupported");
            }
            return conn;
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

        public int CommandTimeOut => throw new NotImplementedException();

        public void Open()
        {
            if (this.IsClosed)
                this.Connection.Open();
        }

        public void Close()
        {
            this.Connection.Close();
        }

        public IDbTransaction BeginTran()
        {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTran(IsolationLevel il)
        {
            throw new NotImplementedException();
        }

        public void CommitTran()
        {
            throw new NotImplementedException();
        }

        public void RollbackTran()
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(SqlQuery query)
        {
            return _connection.Execute(query.CommandText, query.Parameters, _tran, query.CommandTimeout, query.CommandType);
        }

        public Task<int> ExecuteNonQueryAsync(SqlQuery query)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(SqlQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecuteScalarAsync<T>(SqlQuery query)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query<T>(SqlQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
