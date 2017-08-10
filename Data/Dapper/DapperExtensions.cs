using CML.Infrastructure.Utils;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Data.Dapper
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：DapperExtensions.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：DapperExtensions
    /// 创建标识：cml 2017/7/6 11:55:36
    /// </summary>
    public static class DapperExtensions
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        /// <param name="table"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static long Insert(this IDbConnection connection, dynamic data, string table, IDbTransaction transaction = null
            , int? commandTimeout = null)
        {
            var obj = data as object;
            var properties = TypeUtil.GetProperties(obj);
            var columns = string.Join(",", properties);
            var values = string.Join(",", properties.Select(x => "@" + x));
            var sql = string.Format("insert into [{0}] ({1}) values ({2}) select cast(scope_identity() as bigint)", table, columns, values);

            return connection.ExecuteScalar<long>(sql, obj, transaction, commandTimeout);
        }
    }
}
