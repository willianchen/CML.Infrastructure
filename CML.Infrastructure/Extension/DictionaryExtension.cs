using CML.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Extension
{
    /// <summary>
    /// Copyright (C) cml 版权所有。
    /// 类名：DictionaryExtension.cs
    /// 类功能描述：DictionaryExtension
    /// 创建标识：cml 2017/9/20 10:09:53
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// 转换obj为Dictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(this object obj)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (obj == null)
                return null;
            var propertyList = PropertyUtil.GetPropertyInfos(obj);
            foreach (PropertyInfo p in propertyList)
            {
                MethodInfo mi = p.GetGetMethod();
                if (mi != null && mi.IsPublic)
                {
                    dic.Add(p.Name, mi.Invoke(obj, new object[] { }));
                }
            }
            return dic;
        }
    }
}
