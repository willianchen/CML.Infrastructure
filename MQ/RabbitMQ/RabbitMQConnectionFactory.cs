using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.MQ.RabbitMQ
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：RabbitMQConnectionFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：RabbitMQConnectionFactory 工厂类
    /// 创建标识：cml 2017/5/24 11:47:02
    /// </summary>
    public class RabbitMQConnectionFactory : IDisposable
    {
        private static ConcurrentDictionary<string, IConnection> _connCache = new ConcurrentDictionary<string, IConnection>();
        public RabbitMQConnectionFactory() { }

        public static IConnection CreateConn(MQConfig config)
        {
            if (!IsExistConn(config))
            {
                lock (_connCache)
                {
                    if (!IsExistConn(config))
                    {
                        _connCache[config.ToString()] = new ConnectionFactory
                        {
                            HostName = config.Host,
                            Password = config.Password,
                            NetworkRecoveryInterval = config.NetworkRecoveryInterval,
                            RequestedHeartbeat = config.Heartbeat,
                            UserName = config.UserName,
                           // VirtualHost = config.VirtualHost
                        }.CreateConnection();
                    }
                }
            }
            return _connCache[config.ToString()];
        }

        public static bool IsExistConn(MQConfig config)
        {
            return _connCache.ContainsKey(config.ToString());
        }

        public void Dispose()
        {
            lock (_connCache)
            {
                foreach (var item in _connCache)
                {
                    item.Value?.Close();
                    item.Value?.Dispose();
                }
                _connCache.Clear();
            }
        }
    }
}
