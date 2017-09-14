using CML.Infrastructure.Extension;
using CML.Infrastructure.Utils;
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
    /// 类名：SqlQueryUtil.cs
    /// 类功能描述：Sqlquery工具类
    /// 创建标识：阿礼 2017/8/14 15:06:44
    /// </summary>
    public static class SqlQueryUtil
    {
        /// <summary>
        /// 构建插入语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <param name="keyName"></param>
        /// <param name="ignoreFields"></param>
        /// <param name="isIdentity"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static SqlQuery BuildInsert<T>(T entity, string tableName, string keyName = null, string[] ignoreFields = null, bool isIdentity = true, DataBaseType dbType = DataBaseType.MSSqlServer) 
        {
            var propertyList = PropertyUtil.GetPropertyInfos(entity, ignoreFields);
            var proNameList = propertyList.Select(m => m.Name);
            var columns = string.Join(",", proNameList);
            var values = string.Join(",", proNameList.Select(p => GetSign(dbType) + p));
            string commandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2}){3};", tableName, columns, values, isIdentity ? GetIdentityKeyScript(keyName, dbType) : string.Empty);
            return new SqlQuery(commandText, entity);
        }

        /// <summary>
        /// 构建删除语句
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="tableName"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static SqlQuery BuildDelete(object condition, string tableName, DataBaseType dbType = DataBaseType.MSSqlServer)
        {
            var propertyList = PropertyUtil.GetPropertyInfos(condition);
            var propertyName = propertyList.Select(p => p.Name);
            string whereSql = string.Empty;
            if (propertyName.Any())
                whereSql = string.Format(" Where {0}", string.Join("And", propertyName.Select(p => p + "=" + GetSign(dbType) + p)));
            string deleteSql = string.Format("delete from {0} {1}", tableName, whereSql);
            return new SqlQuery(deleteSql, condition);
        }

        /// <summary>
        /// 构建更新语句
        /// </summary>
        /// <param name="data"></param>
        /// <param name="condition"></param>
        /// <param name="tableName"></param>
        /// <param name="ignoreFields"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static SqlQuery BuildUpdate(object data, object condition, string tableName, string[] ignoreFields = null, DataBaseType dbType = DataBaseType.MSSqlServer)
        {
            var propertyUpdateInfos = PropertyUtil.GetPropertyInfos(data, ignoreFields);
            var propertyWhereInfos = PropertyUtil.GetPropertyInfos(condition);

            var updateProperties = propertyUpdateInfos.Select(p => p.Name);
            var whereProperties = propertyWhereInfos.Select(p => p.Name);

            var updateFields = string.Join(",", updateProperties.Select(p => p + "=" + GetSign(dbType) + p));
            string whereSql = string.Empty;
            if (whereProperties.Any())
            {
                whereSql = string.Format("Where {0}", string.Join(" and ", whereProperties.Select(p => p + "=" + GetSign(dbType) + p)));
            }
            string updateSql = string.Format("Update {0}  Set {1} {2}", tableName, updateFields, whereSql);
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.CommandText = updateSql;
            sqlQuery.AddParameter(data);
            sqlQuery.AddParameter(condition);

            return sqlQuery;
        }

        /// <summary>
        /// 创建查询SQL
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="tableName"></param>
        /// <param name="orderBy"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static SqlQuery BuilderQuerySqlQuery(object condition, string tableName, string[] ignoreFields = null, string orderBy = "", DataBaseType dbType = DataBaseType.MSSqlServer)
        {
            SqlQuery query = new SqlQuery();

            var propertyWhereInfos = PropertyUtil.GetPropertyInfos(condition);
            var whereProperties = propertyWhereInfos.Select(p => p.Name);

            string whereSql = string.Empty;
            if (whereProperties.Any())
            {
                whereSql = string.Format("Where {0}", string.Join(" And ", whereProperties.Select(p => p + "=" + (GetSign(dbType) + p))));
            }
            string orderBySql = string.Empty;
            if (orderBy != "")
                orderBySql = " order by " + orderBy;
            string updateSql = string.Format("Select * from  {0}  {1} {2}", tableName, whereSql, orderBySql);
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.CommandText = updateSql;
            sqlQuery.AddParameter(condition);
            return sqlQuery;
        }

        /// <summary>
        /// 创建查询sql 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="condition"></param>
        /// <param name="tableName"></param>
        /// <param name="ignoreFields"></param>
        /// <param name="orderBy"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static SqlQuery BuilderQuerySqlQuery<TModel>(object condition, string tableName, string[] ignoreFields = null, string orderBy = "", DataBaseType dbType = DataBaseType.MSSqlServer)
        {
            SqlQuery query = new SqlQuery();

            var propertyInfos = PropertyUtil.GetPropertyByType(typeof(TModel), ignoreFields);
            var columns = string.Join(",", propertyInfos.Select(m => m.Name));

            var propertyWhereInfos = PropertyUtil.GetPropertyInfos(condition);
            var whereProperties = propertyWhereInfos.Select(p => p.Name);

            string whereSql = string.Empty;
            if (whereProperties.Any())
            {
                whereSql = string.Format("Where {0}", string.Join(" And ", whereProperties.Select(p => p + "=" + GetSign(dbType) + p)));
            }
            string orderBySql = string.Empty;
            if (orderBy != "")
                orderBySql = " order by " + orderBy;
            string updateSql = string.Format("Select {3} from  {0}  {1} {2}", tableName, whereSql, orderBySql, columns);
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.CommandText = updateSql;
            sqlQuery.AddParameter(condition);
            return sqlQuery;
        }

        /// <summary>
        /// 构建取总数sql
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="tableName"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static SqlQuery BuilderQueryCountSqlQuery(object condition, string tableName, DataBaseType dbType = DataBaseType.MSSqlServer)
        {
            var whereFields = string.Empty;
            var whereProperties = PropertyUtil.GetPropertyInfos(condition);
            var whereFieldNames = whereProperties.Select(p => p.Name);
            if (whereFieldNames.Any())
            {
                whereFields = " WHERE " + string.Join(" AND ", whereFieldNames.Select(p => p + " = " + GetSign(dbType) + p));
            }
            var sql = string.Format("SELECT COUNT(0) FROM {0}{1};", tableName, whereFields);
            return new SqlQuery(sql, condition);
        }

        /// <summary>
        /// 构建分页SQLquery
        /// </summary>
        /// <param name="selectColumn"></param>
        /// <param name="selectTable"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dbType"></param>
        /// <param name="cmdparams"></param>
        /// <returns></returns>
        public static SqlQuery BuilderPageSqlQuery(string selectColumn, string selectTable, string where, string order, int pageIndex, int pageSize, DataBaseType dbType = DataBaseType.MSSqlServer, object cmdParams = null)
        {
            SqlQuery query = new SqlQuery();
            string sql = string.Empty;//select语句
            if (pageIndex == 1)
            {
                switch (dbType)
                {
                    case DataBaseType.PostgreSql:
                        sql = string.Format(@"SELECT {0} FROM {1} {2} ORDER BY {3} limit @NUM", string.IsNullOrWhiteSpace(selectColumn) ? "*" : selectColumn, selectTable, string.IsNullOrWhiteSpace(where) ? string.Empty : string.Format(" WHERE {0} ", where), order);
                        break;

                    default:
                        sql = string.Format(@"SELECT TOP(@NUM) {0} FROM {1} {2} ORDER BY {3}", string.IsNullOrWhiteSpace(selectColumn) ? "*" : selectColumn, selectTable, string.IsNullOrWhiteSpace(where) ? string.Empty : string.Format(" WHERE {0} ", where), order);
                        break;
                }

                var param = new ParameterInfo { ParameterName = "@NUM", Value = pageSize, DbType = DbType.Int32 };
                query.AddParameter(param);
            }
            else
            {
                switch (dbType)
                {
                    case DataBaseType.PostgreSql:
                        sql = string.Format(@"SELECT * FROM ( SELECT {0},row_number() over(ORDER BY {3}) as [num] FROM {1} {2} ) as [tab] WHERE NUM BETWEEN @NumStart and @NumEnd", string.IsNullOrWhiteSpace(selectColumn) ? "*" : selectColumn, selectTable, string.IsNullOrWhiteSpace(where) ? string.Empty : string.Format(" WHERE {0} ", where), order);
                        break;

                    default:
                        sql = string.Format(@"SELECT * FROM ( SELECT {0},row_number() over(ORDER BY {3}) as [num] FROM {1} {2} ) as [tab] WHERE NUM BETWEEN @NumStart and @NumEnd", string.IsNullOrWhiteSpace(selectColumn) ? "*" : selectColumn, selectTable, string.IsNullOrWhiteSpace(where) ? string.Empty : string.Format(" WHERE {0} ", where), order);
                        break;
                }
                List<ParameterInfo> listParms = new List<ParameterInfo>();
                listParms.Add(new ParameterInfo { ParameterName = "@NumStart", Value = ((pageIndex - 1) * pageSize + 1), DbType = DbType.Int32 });
                listParms.Add(new ParameterInfo { ParameterName = "@NumEnd", Value = (pageIndex * pageSize).ToString(), DbType = DbType.Int32 });
                query.AddParameter(listParms);
            }
            if (cmdParams != null)
            {
                query.AddParameter(cmdParams);
            }
            query.CommandText = sql;
            query.CommandType = CommandType.Text;
            return query;
        }

        #region 通用方法
        private static string GetIdentityKeyScript(string keyName, DataBaseType dbType)
        {
            string identityScript = string.Empty;
            switch (dbType)
            {
                case DataBaseType.MySql:
                case DataBaseType.MSSqlServer:
                    identityScript = " SELECT scope_identity() ";
                    break;

                case DataBaseType.PostgreSql:
                    identityScript = string.Concat(" RETURNING ", keyName, " ");
                    break;
            }
            return identityScript;
        }

        private static string GetSign(DataBaseType dbType)
        {
            switch (dbType)
            {
                case DataBaseType.MSSqlServer:
                case DataBaseType.PostgreSql:
                    return "@";

                case DataBaseType.MySql:
                    return "?";
                default:
                    throw new NotSupportedException(dbType.ToString());
            }
        }

        #endregion

    }
}
