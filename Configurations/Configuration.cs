using CML.Infrastructure.Autofac;
using CML.Infrastructure.Components;
using CML.Infrastructure.Mail;
using CML.Infrastructure.Mail.SendCloud;
using CML.Infrastructure.Mail.Smtp;
using CML.Infrastructure.Serializing;
using CML.Infrastructure.Utils;

namespace CML.Infrastructure.Configurations
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：Configuration.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：初始化配置类
    /// 创建标识：cml 2017/6/30 9:57:32
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static Configuration Instance { get; private set; }

        private Configuration() { }

        public static Configuration CreateInstance()
        {
            Instance = new Configuration();
            return Instance;
        }

        public Configuration UserAutofac()
        {
            ObjectContainer.SetContainer(new AutofacContainer());
            return this;
        }
        public Configuration SetDefault<TService, TImplementer>(string serviceName = null, LifeScope life = LifeScope.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.Register<TService, TImplementer>(serviceName, life);
            return this;
        }

        public Configuration SetDefault<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.RegisterInstance<TService, TImplementer>(instance, serviceName);
            return this;
        }

        public Configuration SetDefault<TService>(string serviceName = null)
        {
            ObjectContainer.RegisterType<TService>(serviceName);
            return this;
        }
        public Configuration RegisterCommonComponents()
        {
            SetDefault<IJsonSerializer, NewtonsoftJsonSerializer>();
            SetDefault<JsonUtil>();
            SetDefault<ILogFactory, NLoggerFactory>();
            //  SetDefault<IEmailSender, SendCloudEmailSender>();
            return this;
        }

        public Configuration RegistMailSender()
        {
            SetDefault<ISendCloudEmailSenderConfiguration, SendCloudEmailSenderConfiguration>();
            SetDefault<IEmailSender, SendCloudEmailSender>();
          //  SetDefault<ILogFactory, NLoggerFactory>();
            return this;
        }

        public Configuration RegistSmtpMailSender()
        {
            SetDefault<ISmtpEmailSenderConfiguration, SmtpEmailSenderConfiguration>();
            SetDefault<IEmailSender, SmtpEmailSender>();
            //  SetDefault<ILogFactory, NLoggerFactory>();
            return this;
        }
    }
}
