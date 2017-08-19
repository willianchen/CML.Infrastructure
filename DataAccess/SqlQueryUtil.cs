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
        public static SqlQuery BuildInsert<T>(T entity, string tableName, string keyName = null, string[] ignoreFields = null, bool isIdentity = true, DataBaseType dbType = DataBaseType.MSSqlServer) where T : class
        {
            var propertyList = PropertyUtil.GetPropertyInfos(entity, ignoreFields);
            var proNameList = propertyList.Select(m => m.Name);
            var columns = string.Join(",", proNameList);
            var values = string.Join(",", proNameList.Select(p => GetSign(dbType) + p));
            string commandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2}){3};", tableName, columns, values, isIdentity ? GetIdentityKeyScript(keyName, dbType) : string.Empty);
            return new SqlQuery(commandText, entity);
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
