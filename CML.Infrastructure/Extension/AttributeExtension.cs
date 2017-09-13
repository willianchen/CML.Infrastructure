using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Extension
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：AttributeExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：AttributeExtension
    /// 创建标识：cml 2017/5/24 17:45:17
    /// </summary>
    public static class AttributeExtension
    {
        public static T GetAttribute<T>(this Type type) where T : class
        {
            return Attribute.GetCustomAttribute(type, typeof(T)) as T;
        }
    }
}
