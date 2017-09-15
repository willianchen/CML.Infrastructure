using Autofac;
using CML.Infrastructure.Components;
using System;

namespace CML.Infrastructure.Autofac
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：AutofacContainer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：Autofac容器
    /// 创建标识：cml 2017/6/29 15:41:50
    /// </summary>
    public class AutofacContainer : IObjectContainer
    {
        private IContainer _container;
        private ContainerBuilder _containerBuilder;

        private IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    lock (this)
                    {
                        if (_container == null)
                        {
                            _container = _containerBuilder.Build();
                        }
                    }
                }
                return _container;
            }
        }


        public AutofacContainer()
        {
            _containerBuilder = new ContainerBuilder();
        }

        public AutofacContainer(ContainerBuilder builder)
        {
            _containerBuilder = builder ?? new ContainerBuilder();
        }


        ///// <summary>
        ///// Init Constract
        ///// </summary>
        ///// <param name="builder"></param>
        //public AutofacContainer(ContainerBuilder builder)
        //{
        //    container = builder.Build();
        //}
        public void RegisterType(Type implementationType, string serviceName = null, LifeScope life = LifeScope.Singleton)
        {
            var registrationBuilder = _containerBuilder.RegisterType(implementationType);
            if (serviceName != null)
            {
                registrationBuilder.Named(serviceName, implementationType);
            }
            switch (life)
            {
                case LifeScope.PerDependency:
                    registrationBuilder.InstancePerDependency();
                    break;
                case LifeScope.Singleton:
                    registrationBuilder.SingleInstance();
                    break;
            }
        }

        public void RegisterType(Type serviceType, Type implementationType, string serviceName = null, LifeScope life = LifeScope.Singleton)
        {
            var registrationBuilder = _containerBuilder.RegisterType(implementationType).As(serviceType);
            if (serviceName != null)
            {
                registrationBuilder.Named(serviceName, serviceType);
            }
            switch (life)
            {
                case LifeScope.PerDependency:
                    registrationBuilder.InstancePerDependency();
                    break;
                case LifeScope.Singleton:
                    registrationBuilder.SingleInstance();
                    break;
            }
        }

        public void RegisterType<TService>(string serviceName = null, LifeScope life = LifeScope.Singleton)
        {
            var registrationBuilder = _containerBuilder.RegisterType<TService>();
            if (serviceName != null)
            {
                registrationBuilder.Named<TService>(serviceName);
            }
            switch (life)
            {
                case LifeScope.PerDependency:
                    registrationBuilder.InstancePerDependency();
                    break;
                case LifeScope.Singleton:
                    registrationBuilder.SingleInstance();
                    break;
            }
        }

        public void Register<TService, TImplementer>(string serviceName = null, LifeScope life = LifeScope.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            var registrationBuilder = _containerBuilder.RegisterType<TImplementer>().As<TService>();
            if (serviceName != null)
            {
                registrationBuilder.Named<TService>(serviceName);
            }
            switch (life)
            {
                case LifeScope.PerDependency:
                    registrationBuilder.InstancePerDependency();
                    break;
                case LifeScope.Singleton:
                    registrationBuilder.SingleInstance();
                    break;
            }
        }

        public void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null)
              where TService : class
              where TImplementer : class, TService
        {
            var registrationBuilder = _containerBuilder.RegisterType<TImplementer>().As<TService>().SingleInstance();
            if (serviceName != null)
            {
                registrationBuilder.Named<TService>(serviceName);
            }
        }

        public TService Resolve<TService>() where TService : class
        {
            return Container.Resolve<TService>();
        }

        public object Resolve(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }

        public object ResolveNamed(Type serviceType, string serviceName)
        {
            return Container.ResolveNamed(serviceName, serviceType);
        }

        public TService ResolveNamed<TService>(string serviceName)
        {
            return Container.ResolveNamed<TService>(serviceName);
        }

        public bool TryResolve(Type serviceType, out object instance)
        {
            return Container.TryResolve(serviceType, out instance);
        }

        public bool TryResolve<TService>(out TService instance)
        {
            return Container.TryResolve<TService>(out instance);
        }

        public bool TryResolveNamed(Type serviceType, string serviceName, out object instance)
        {
            return Container.TryResolveNamed(serviceName, serviceType, out instance);
        }
    }
}
