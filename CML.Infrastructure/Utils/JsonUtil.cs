using Autofac;
using CML.Infrastructure.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Utils
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：JsonUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：JsonUtil
    /// 创建标识：cml 2017/6/27 9:34:01
    /// </summary>
    public static class JsonUtil
    {
        private static NewtonsoftJsonSerializer _jsonSerializer;

        static JsonUtil()
        {
            _jsonSerializer = new NewtonsoftJsonSerializer();
        }

        public static T Deserialize<T>(string value) where T : class
        {
            return _jsonSerializer.Deserialize<T>(value);
        }

        public static string Serialize(object obj)
        {
            return _jsonSerializer.Serialize(obj);
        }
    }
}
