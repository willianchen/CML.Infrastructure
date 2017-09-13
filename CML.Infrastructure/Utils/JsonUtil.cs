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
    /// 类属性：公共类（非静态）
    /// 类功能描述：JsonUtil
    /// 创建标识：cml 2017/6/27 9:34:01
    /// </summary>
    public class JsonUtil
    {
        private IJsonSerializer jsonSerializer;
    
        public JsonUtil(IJsonSerializer _jsonSerializer)
        {
            this.jsonSerializer = _jsonSerializer;
        }

        public T Deserialize<T>(string value) where T : class
        {
            return jsonSerializer.Deserialize<T>(value);
        }

        public  string Serialize(object obj)
        {
            return jsonSerializer.Serialize(obj);
        }
    }
}
