using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Components
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：ObjectContainer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：容器实现类
    /// 创建标识：cml 2017/6/29 14:22:04
    /// </summary>
    public class ObjectContainer
    {
        /// <summary>
        /// 静态容器实例
        /// </summary>
        public static IObjectContainer Current { get; private set; }

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <param name="_objectContainer"></param>
        public static void SetContainer(IObjectContainer _objectContainer)
        {
            Current = _objectContainer;
        }

        /// <summary>
        /// 注册一个类
        /// </summary>
        /// <param name="implementationType"></param>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        public static void RegisterType(Type implementationType, string serviceName = null, LifeScope life = LifeScope.Singleton)
        {
            Current.RegisterType(implementationType, serviceName, life);
        }

        /// <summary>
        /// 注册一个Service类型的实现类
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        public static void RegisterType(Type serviceType, Type implementationType, string serviceName = null, LifeScope life = LifeScope.Singleton)
        {
            Current.RegisterType(serviceType, implementationType, serviceName, life);
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        public static void RegisterType<TService>(string serviceName = null, LifeScope life = LifeScope.Singleton)
        {
            Current.RegisterType<TService>(serviceName, life);
        }
        /// <summary>
        /// 注册一个Service类型的实现类
        /// </summary>
        /// <typeparam name="TSerivce"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        public static void Register<TSerivce, TImplementer>(string serviceName = null, LifeScope life = LifeScope.Singleton)
            where TSerivce : class
            where TImplementer : class, TSerivce
        {
            Current.Register<TSerivce, TImplementer>(serviceName, life);
        }

        /// <summary>
        /// 注册一个TService类型类实例
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="instance"></param>
        /// <param name="serviceName"></param>
        public static void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService
        {
            Current.RegisterInstance<TService, TImplementer>(instance, serviceName);
        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService Resolve<TService>() where TService : class
        {
            return Current.Resolve<TService>();
        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static object Resolve(Type serviceType)
        {
            return Current.Resolve(serviceType);
        }

        /// <summary>
        /// 尝试解析一个Tservice类型的实例
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool TryResolve<TService>(out TService instance)
            where TService : class
        {
            return Current.TryResolve<TService>(out instance);
        }

        /// <summary>
        /// 尝试解析一个TService类型的实例
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool TryResolve(Type serviceType, out object instance)
        {
            return Current.TryResolve(serviceType, out instance);
        }

        /// <summary>
        /// 解析一个类
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            return Current.ResolveNamed<TService>(serviceName);
        }

        /// <summary>
        /// 解析一个类
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static object ResolveNamed(Type serviceType, string serviceName)
        {
            return Current.ResolveNamed(serviceType, serviceName);
        }

        /// <summary>
        /// 尝试解析一个类实例
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="serviceName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool TryResolveNamed(Type serviceType, string serviceName, out object instance)
        {
            return Current.TryResolveNamed(serviceType, serviceName, out instance);
        }
    }
}
