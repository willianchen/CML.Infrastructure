using CML.Infrastructure.Extension;
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
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：TypeUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：TypeUtil
    /// 创建标识：cml 2017/7/6 14:08:58
    /// </summary>
    public static class TypeUtil
    {
        private static readonly ConcurrentDictionary<Type, List<PropertyInfo>> _cacheProperties = new ConcurrentDictionary<Type, List<PropertyInfo>>();
        public static List<PropertyInfo> GetPropertyInfos(object obj)
        {
            if (obj.IsNull())
                return new List<PropertyInfo>();
            return _cacheProperties.GetValue(obj.GetType(), () =>
            {
                return obj.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public).ToList();
            });
        }

        public static List<string> GetProperties(object obj)
        {
            if (obj.IsNull())
                return new List<string>();
            // if(obj is DynamicPara)
            return GetPropertyInfos(obj).Select(x => x.Name).ToList();
        }
    }
}
