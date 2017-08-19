using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Utils
{
    /// <summary>
    /// Copyright (C) 阿礼 版权所有。
    /// 类名：PropertyUtil.cs
    /// 类功能描述：<功能描述> 
    /// 创建标识：阿礼 2017/8/14 15:38:55
    /// </summary>
    public static class PropertyUtil
    {
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, List<PropertyInfo>> _propertyCache = new ConcurrentDictionary<RuntimeTypeHandle, List<PropertyInfo>>();

        /// <summary>
        /// 获取实例的属性信息
        /// </summary>
        /// <param name="obj">实例</param>
        /// <param name="ignoreProperties">忽略属性</param>
        /// <returns>实例的属性列表</returns>
        public static List<PropertyInfo> GetPropertyInfos(object obj, string[] ignoreProperties)
        {
            var properties = GetPropertyInfos(obj).Where(s =>
            {
                if (ignoreProperties == null) return true;
                if (ignoreProperties.Contains(s.Name)) return false;
                return true;
            }).ToList();
            return properties;
        }

        public static List<PropertyInfo> GetPropertyInfos(object obj)
        {
            if (obj == null) return new List<PropertyInfo>();
            return GetPropertyByType(obj.GetType());
        }

        public static List<PropertyInfo> GetPropertyByType(Type type)
        {
            if (type == null)
                return new List<PropertyInfo>();
            var typeHandle = type.TypeHandle;
            List<PropertyInfo> value = new List<PropertyInfo>();
            if (!_propertyCache.TryGetValue(typeHandle, out value))
            {
                value = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
                _propertyCache[typeHandle] = value;
            }
            return value;
        }
    }
}
