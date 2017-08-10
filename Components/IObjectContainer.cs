using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Components
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：IObjectContainer.cs
    /// 类属性：接口
    /// 类功能描述：容器接口
    /// 创建标识：cml 2017/6/29 13:46:10
    /// </summary>
    public interface IObjectContainer
    {
        /// <summary>
        /// 注册一个实现类
        /// </summary>
        /// <param name="implementationType">实现类型</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="life">生命周期</param>
        void RegisterType(Type implementationType, string serviceName = null, LifeScope life = LifeScope.Singleton);

        /// <summary>
        /// 注册一个实现ServiceType类型的实现类
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">实现类型</param>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        void RegisterType(Type serviceType, Type implementationType, string serviceName = null, LifeScope life = LifeScope.Singleton);

        /// <summary>
        /// 注册一个TService类型
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        void RegisterType<TService>(string serviceName = null, LifeScope life = LifeScope.Singleton);

        /// <summary>
        /// 注册一个实现ServiceType类型的实现类
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementer">实现类型</typeparam>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        void Register<TService, TImplementer>(string serviceName = null, LifeScope life = LifeScope.Singleton)
            where TService : class
            where TImplementer : class, TService;

        /// <summary>
        /// 注册一个实现ServiceType类型的实例
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementer">实现类型</typeparam>
        /// <param name="instance"></param>
        /// <param name="serviceName"></param>
        void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null)
               where TService : class
            where TImplementer : class, TService;

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        TService Resolve<TService>() where TService : class;

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        object Resolve(Type serviceType);

        /// <summary>
        /// 尝试解析一个类
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool TryResolve(Type serviceType, out object instance);

        /// <summary>
        /// 尝试解析一个类
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool TryResolve<TService>(out TService instance);

        /// <summary>
        /// 解析一个类
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        object ResolveNamed(Type serviceType, string serviceName);

        /// <summary>
        /// 解析一个类型
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        TService ResolveNamed<TService>(string serviceName);

        /// <summary>
        /// 尝试解析一个类实例
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="serviceName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool TryResolveNamed(Type serviceType, string serviceName, out object instance);
    }
}
