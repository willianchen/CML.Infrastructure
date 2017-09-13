using Dapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.DataAccess
{
    /// <summary>
    /// Copyright (C) 阿礼 版权所有。
    /// 类名：SqlQuery.cs
    /// 类功能描述：sql语句操作类 
    /// 创建标识：阿礼 2017/8/11 15:33:54
    /// </summary>
    public sealed class SqlQuery
    {
        private string _commandText;
        private int _commandTimeout = 30000;
        private CommandType _commandType = CommandType.Text;
        private DynamicParameters _parameters;
        private ConcurrentDictionary<string, ParameterInfo> _parametersDic = new ConcurrentDictionary<string, ParameterInfo>();


        public SqlQuery()
        { }

        public SqlQuery(string sql, object param)
        {
            _commandText = sql;
            AddParameter(param);
        }

        public void InitParameter()
        {
            if (_parameters == null)
            {
                _parameters = new DynamicParameters();
            }
        }
        /// <summary>
        /// 添加动态参数
        /// </summary>
        /// <param name="param"></param>
        public void AddParameter(object param)
        {
            InitParameter();
            _parameters.AddDynamicParams(param);
        }


        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        public void AddParameter(string name, object value, DbType? dbType, ParameterDirection? direction, int? size)
        {
            InitParameter();
            _parameters.Add(name, value, dbType, direction, size);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="paramInfo"></param>
        public void AddParameter(ParameterInfo paramInfo)
        {
            AddParameter(paramInfo.ParameterName, paramInfo.Value, paramInfo.DbType, paramInfo.ParameterDirection, paramInfo.Size);
        }

        /// <summary>
        /// 添加参数列表
        /// </summary>
        /// <param name="infoList"></param>
        public void AddParameter(List<ParameterInfo> infoList)
        {
            if (infoList != null && infoList.Any())
            {
                foreach (var item in infoList)
                {
                    AddParameter(item);
                }
            }
        }

        public string CommandText
        {
            get
            {
                return _commandText;
            }
            set
            {
                this._commandText = value;
            }
        }

        public int CommandTimeout
        {
            get
            {
                return this._commandTimeout;
            }
            set
            {
                if (value < 10)
                {
                    this._commandTimeout = 300;
                    return;
                }
                this._commandTimeout = value;
            }
        }

        public CommandType CommandType
        {
            get { return _commandType; }
            set { _commandType = value; }
        }

        public DynamicParameters Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
