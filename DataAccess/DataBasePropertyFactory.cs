using System;
using System.Configuration;

namespace CML.Infrastructure.DataAccess
{
    /// <summary>
    /// Copyright (C) 阿礼 版权所有。
    /// 类名：DataBasePropertyFactory.cs
    /// 类功能描述：数据库属性工厂类
    /// 创建标识：阿礼 2017/8/11 15:06:34
    /// </summary>
    public static class DataBasePropertyFactory
    {
        public static DataBaseProperty GetDataBaseProperty(string connectionName)
        {
            DataBaseConnection write = GetDbConnection(connectionName + ".Writer");
            DataBaseConnection read = GetDbConnection(connectionName + ".Reader");
            return new DataBaseProperty(read, write);
        }

        /// <summary>
        /// 获取数据连接
        /// </summary>
        /// <param name="connStrName"></param>
        /// <returns></returns>
        private static DataBaseConnection GetDbConnection(string connStrName)
        {
            ConnectionStringSettings connStrSetting = ConfigurationManager.ConnectionStrings[connStrName];
            if (connStrSetting == null || string.IsNullOrWhiteSpace(connStrSetting.ConnectionString))
            {
                throw new Exception("数据库连接字符串【" + connStrName + "】没有配置！");
            }
            DataBaseConnection dbConnection = new DataBaseConnection();
            dbConnection.ConnectionString = connStrSetting.ConnectionString;
            dbConnection.DatabaseType = GetDbType(connStrSetting.ProviderName);
            return dbConnection;
        }

        public static DataBaseConnection GetDefautDbConnectionByConnectString(string connectionString)
        {
            if ( string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("数据库连接字符串没有配置！");
            }
            DataBaseConnection dbConnection = default(DataBaseConnection);
            dbConnection.ConnectionString = connectionString;
            dbConnection.DatabaseType = DataBaseType.MSSqlServer;
            return dbConnection;
        }
        /// <summary>
        /// 获取数据库连接类型
        /// </summary>
        /// <param name="providerName"></param>
        /// <returns></returns>
        private static DataBaseType GetDbType(string providerName)
        {
            DataBaseType dbType = default(DataBaseType);
            switch (providerName)
            {
                case "System.Data.SqlClient":
                    dbType = DataBaseType.MSSqlServer;
                    break;
                case "MySql.Data.MySqlClient":
                    dbType = DataBaseType.MySql;
                    break;
                case "MySql.Data.PostgreSqlClient":
                    dbType = DataBaseType.PostgreSql;
                    break;
                default:
                    throw new NotSupportedException("Database Provider NotSupported");
            }
            return dbType;
        }
    }
}
