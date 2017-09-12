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
            this.Open();
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

        #region 属性

        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        /// <summary>
        /// 是否关闭连接
        /// </summary>
        public bool IsClosed
        {
            get
            {
                return this.Connection.State.Equals(ConnectionState.Closed);
            }
        }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int CommandTimeOut
        {
            get
            {
                return this._cmdTimeout;
            }
            set
            {
                this._cmdTimeout = value;
            }
        }

        /// <summary>
        /// 事务
        /// </summary>
        public virtual IDbTransaction Tran
        {
            get { return _tran; }
            set { _tran = value; }
        }

        #endregion

        #region 打开/关闭连接

        /// <summary>
        /// 打开连接
        /// </summary>
        public void Open()
        {
            if (this.IsClosed)
                this.Connection.Open();
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            this.Connection.Close();
        }

        #endregion

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

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="query"></param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(SqlQuery query)
        {
            return _connection.Execute(query.CommandText, query.Parameters, _tran, query.CommandTimeout, query.CommandType);
        }

        /// <summary>
        /// 异步执行sql
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<int> ExecuteNonQueryAsync(SqlQuery query)
        {
            return _connection.ExecuteAsync(query.CommandText, query.Parameters, _tran, query.CommandTimeout, query.CommandType);
        }

        /// <summary>
        /// 执行sql 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(SqlQuery query)
        {
            return _connection.ExecuteScalar<T>(query.CommandText, query.Parameters, Tran, query.CommandTimeout, query.CommandType);
        }

        /// <summary>
        /// 异步执行sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<T> ExecuteScalarAsync<T>(SqlQuery query)
        {
            return _connection.ExecuteScalarAsync<T>(query.CommandText, query.Parameters, Tran, query.CommandTimeout, query.CommandType);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(SqlQuery query)
        {
            return _connection.Query<T>(query.CommandText, query.Parameters, Tran, false, query.CommandTimeout, query.CommandType);
        }

        public T QuerySingleOrDefault<T>(SqlQuery query)
        {
            return _connection.QueryFirstOrDefault<T>(query.CommandText, query.Parameters, Tran, query.CommandTimeout, query.CommandType);
        }
    }
}
