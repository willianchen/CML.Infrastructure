using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Components
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：LifeScope.cs
    /// 类属性：枚举类
    /// 类功能描述：容器实例生命周期枚举
    /// 创建标识：cml 2017/6/29 13:46:10
    /// </summary>
    public enum LifeScope
    {
        /// <summary>
        /// 瞬态或者工厂，使用Per Dependency作用域，服务对于每次请求都会返回单独的实例。
        /// </summary>
        PerDependency,
        /// <summary>
        /// 单例，所有对父容器或者嵌套容器的请求都会返回同一个实例。
        /// </summary>
        Singleton,
        /// <summary>
        /// 这个作用域适用于嵌套的生命周期，一个使用Per Lifetime 作用域的component在一个 nested lifetime scope内最多有一个实例。
        /// </summary>
        PerLifeScope
    }
}
