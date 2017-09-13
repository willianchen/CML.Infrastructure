using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Extension
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：ConcurrentDictionaryExtention.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ConcurrentDictionaryExtention
    /// 创建标识：cml 2017/5/24 17:38:01
    /// </summary>
    public static class ConcurrentDictionaryExtention
    {
        public static TValue GetValue<TKey, TValue>(this ConcurrentDictionary<TKey,TValue> o, TKey key, Func<TValue> handel)
        {
            TValue value;
            if (o.TryGetValue(key, out  value)) return value;
            TValue tValue = handel();
            o[key] = tValue;
            return tValue;
        }
    }
}
