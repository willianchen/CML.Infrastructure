using CML.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.DataAccess
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
        public static SqlQuery BuildInsert<T>(T entity, string tableName, string keyName = null, string[] ignoreFields = null, bool isIdentity = true, DataBaseType dbType = DataBaseType.MSSqlServer) where T : class
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
                whereSql = string.Format(" Where {0}", string.Join("And", propertyList.Select(p => p + "=" + GetSign(dbType) + p)));
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
            var propertyUpdateInfos = PropertyUtil.GetPropertyInfos(data);
            var propertyWhereInfos = PropertyUtil.GetPropertyInfos(condition);

            var updateProperties = propertyUpdateInfos.Select(p => p.Name);
            var whereProperties = propertyWhereInfos.Select(p => p.Name);

            var updateFields = string.Join(",", updateProperties.Select(p => p = GetSign(dbType) + p));
            string whereSql = string.Empty;
            if (whereProperties.Any())
            {
                whereSql = string.Format("Where {0}", string.Join(",", whereProperties.Select(p => p = GetSign(dbType) + p)));
            }
            string updateSql = string.Format("Update {0}  Set {1} {3}", tableName, updateFields, whereSql);
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.CommandText = updateSql;
            sqlQuery.AddParameter(data);
            sqlQuery.AddParameter(condition);

            return sqlQuery;
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
