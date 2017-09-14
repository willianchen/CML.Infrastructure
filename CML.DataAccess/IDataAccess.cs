using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.DataAccess
{
    /// <summary>
    /// Copyright (C) 阿礼 版权所有。
    /// 接口名：IDataAccess.cs
    /// 接口功能描述：数据操作接口
    /// 创建标识：阿礼 2017/8/11 15:31:42
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// 获取一个数据库连接
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// 超时时间
        /// </summary>
        int CommandTimeOut { get; }

        /// <summary>
        /// 开始事务操作
        /// </summary>
        /// <returns>事务</returns>
        IDbTransaction BeginTran();

        /// <summary>
        /// 开始事务操作
        /// </summary>
        /// <param name="il">事务隔离级别</param>
        /// <returns>事务</returns>
        IDbTransaction BeginTran(IsolationLevel il);

        /// <summary>
        /// 打开连接
        /// </summary>
        void Open();

        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTran();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTran();

        /// <summary>
        /// 关闭连接
        /// </summary>
        void Close();

        /// <summary>
        /// 执行增删改方法，返回受影响的行数
        /// </summary>
        /// <param name="query">请求内容</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(SqlQuery query);

        /// <summary>
        /// 异步执行增删改方法，返回受影响的行数
        /// </summary>
        /// <param name="query">请求内容</param>
        /// <returns>受影响的行数</returns>
        Task<int> ExecuteNonQueryAsync(SqlQuery query);

        /// <summary>
        /// 取出第一条数据
        /// </summary>
        /// <typeparam name="T">数据返回类型</typeparam>
        /// <param name="query">请求内容</param>
        /// <returns>单条数据内容</returns>
        T ExecuteScalar<T>(SqlQuery query);

        /// <summary>
        /// 异步取出第一条数据
        /// </summary>
        /// <typeparam name="T">数据返回类型</typeparam>
        /// <param name="query">请求内容</param>
        /// <returns>单条数据内容</returns>
        Task<T> ExecuteScalarAsync<T>(SqlQuery query);

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <returns>结果集</returns>
        IEnumerable<T> Query<T>(SqlQuery query);

        /// <summary>
        /// 返回第一条结果信息
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <returns>第一条结果信息</returns>
        T QuerySingleOrDefault<T>(SqlQuery query);
    }
}
