using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.DataAccess
{
    /// <summary>
    /// Copyright (C) 阿礼 版权所有。
    /// 类名：DataBaseProperty.cs
    /// 类功能描述：数据库连接
    /// 创建标识：阿礼 2017/8/11 14:50:23
    /// </summary>
    public class DataBaseProperty
    {
        private DataBaseConnection _reader;
        private DataBaseConnection _writer;

        public DataBaseConnection Reader
        {
            get { return _reader; }
        }

        public DataBaseConnection Writer
        {
            get { return _writer; }
        }

        public DataBaseProperty(DataBaseConnection reader, DataBaseConnection writer)
        {
            _reader = reader;
            _writer = writer;
        }
    }
}
