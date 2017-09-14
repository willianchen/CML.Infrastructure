using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.DataAccess
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ParameterInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ParameterInfo
    /// 创建标识：cml 2017/9/11 19:37:24
    /// </summary>
    public class ParameterInfo
    {
        public ParameterInfo()
        {
        }

        public ParameterInfo(string parameterName, object value, DbType? dbType, int? size, ParameterDirection? direction, byte? scale = null) : this()
        {
            ParameterName = parameterName;
            Value = value;
            if (dbType.HasValue)
            {
                DbType = dbType.Value;
            }
            if (size.HasValue)
            {
                Size = size.Value;
            }
            if (direction.HasValue)
            {
                ParameterDirection = direction.Value;
            }
            if (scale.HasValue)
            {
                Scale = scale.Value;
            }
        }

        /// <summary>
        /// 参数名字
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public ParameterDirection ParameterDirection { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType DbType { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 获取或设置所解析的 Value 的小数位数。
        /// </summary>
        public byte? Scale { get; set; }
    }
}
