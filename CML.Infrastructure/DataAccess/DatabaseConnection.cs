namespace CML.Infrastructure.DataAccess
{
    /// <summary>
    /// Copyright (C) 阿礼 版权所有。
    /// 类名：DatabaseConnection.cs
    /// 类功能描述：数据库连接类
    /// 创建标识：阿礼 2017/8/11 14:47:05
    /// </summary>
    public class DataBaseConnection
    {
        private string _connectionString;
        private DataBaseType _databaseType;

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public DataBaseType DatabaseType
        {
            get
            {
                return _databaseType;
            }
            set
            {
                _databaseType = value;
            }
        }
    }
}
