using CML.Infrastructure.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CML.Infrastructure.MQ.RabbitMQ
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：RabbitMQService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：RabbitMQService
    /// 创建标识：cml 2017/5/24 11:42:47
    /// </summary>
    public sealed class RabbitMQService : IDisposable
    {
        private readonly MQConfig config;
        private IModel channel;

        private static readonly ConcurrentDictionary<string, IModel> ModelDic =
          new ConcurrentDictionary<string, IModel>();
        public RabbitMQService(MQConfig _config)
        {
            config = _config;
        }

        private IModel Channel
        {
            get
            {
                return channel ?? (channel = RabbitMQConnectionFactory.CreateConn(config).CreateModel());
            }
        }

        /// <summary>
        /// 声明交换机
        /// </summary>
        /// <param name="exchange">交换机名称</param>
        /// <param name="exchangType">交换机类型</param>
        /// <param name="exchangType">交换机类型
        /// Fanout Exchange – 不处理路由键。你只需要简单的将队列绑定到交换机上。一个发送到交换机的消息都会被转发到与该交换机绑定的所有队列上。很像子网广播，每台子网内的主机都获得了一份复制的消息。Fanout交换机转发消息是最快的。
        /// Direct Exchange：处理路由键。需要将一个队列绑定到交换机上，要求该消息与一个特定的路由键完全匹配。这是一个完整的匹配。如果一个队列绑定到该交换机上要求路由键 “dog”，则只有被标记为“dog”的消息才被转发，不会转发dog.puppy，也不会转发dog.guard，只会转发dog。
        /// Topic Exchange – 将路由键和某模式进行匹配。此时队列需要绑定要一个模式上。符号“#”匹配一个或多个词，符号“*”匹配不多不少一个词。因此“audit.#”能够匹配到“audit.irs.corporate”，但是“audit.*” 只会匹配到“audit.irs”。
        /// </param>
        /// <param name="durable">是否持久化</param>
        /// <param name="aotuDelete">是否自动删除</param>
        /// <param name="arguments">参数</param>
        public void ExchangeDeclare(string exchange, string exchangType = ExchangeType.Direct,
            bool durable = true,
            bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            exchange = exchange.IsNullOrWhiteSpace() ? "" : exchange.Trim();
            Channel.ExchangeDeclare(exchange.Trim(), exchangType, durable, autoDelete, arguments);
        }

        /// <summary>
        /// 声明队列
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="durable"></param>
        /// <param name="exclusive"></param>
        /// <param name="autoDelete"></param>
        /// <param name="arguments"></param>
        public void QueueDeclare(string queue, bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            queue = queue.IsNullOrWhiteSpace() ? "" : queue.Trim();
            lock (channel)
                Channel.QueueDeclare(queue, durable, exclusive, autoDelete, arguments);
        }

        /// <summary>
        /// 绑定队列
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="routingKey"></param>
        /// <param name="arguments"></param>
        public void BindQueue(string exchange, string queue, string routingKey, IDictionary<string, object> arguments = null)
        {
            lock (channel)
                Channel.QueueBind(queue, exchange, routingKey, arguments);
        }
        /// <summary>
        /// 声明交换机和队列
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="routingKey"></param>
        /// <param name="exchangeType"></param>
        /// <param name="durable"></param>
        /// <param name="exclusive"></param>
        /// <param name="autoDelete"></param>
        /// <param name="arguments"></param>
        /// <param name="isConsumer"></param>
        private void CreateExchangeAndQueue(string exchange, string queue, string routingKey, string exchangeType = ExchangeType.Fanout, bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null, bool isConsumer = false)
        {
            ExchangeDeclare(exchange, exchangeType, durable, autoDelete, arguments);
            QueueDeclare(queue, durable, exclusive, autoDelete, arguments);
            BindQueue(exchange, queue, routingKey, arguments);
            if (isConsumer)
            {
                //每次消费的消息数
                Channel.BasicQos(0, 1, false);
            }
        }

        /// <summary>
        /// 声明交换机和队列
        /// </summary>
        /// <param name="mqAttribute"></param>
        /// <param name="arguments"></param>
        /// <param name="isConsumer"></param>
        private void CreateExchangeAndQueue(MQAttribute mqAttribute, IDictionary<string, object> arguments = null, bool isConsumer = false)
        {
            CreateExchangeAndQueue(mqAttribute.ExchangeName, mqAttribute.QueueName, mqAttribute.RoutingKey, mqAttribute.MQExchangeType, mqAttribute.Durable, mqAttribute.Exclusive, mqAttribute.AutoDelete, arguments, isConsumer);
        }

    
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        public void Pubish<T>(T command)
        {
            var mqAttribute = MQAttribute.GetMQAttribute<T>();
            if (mqAttribute.IsNull())
                throw new ArgumentException();

            CreateExchangeAndQueue(mqAttribute);
            var properties = Channel.CreateBasicProperties();
            properties.Persistent = true;
            Channel.BasicPublish(mqAttribute.ExchangeName, mqAttribute.RoutingKey, properties, command.ToJsonUTF8());
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actionHandle"></param>
        public void Subscribe<T>(Action<T> actionHandle) where T : class
        {
            var mqAttribute = MQAttribute.GetMQAttribute<T>();
            CreateExchangeAndQueue(mqAttribute, isConsumer: true);
            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var messageStr = Encoding.UTF8.GetString(body);

                var message = messageStr.FromJson<T>();
                try
                {
                    actionHandle(message);
                }
                finally
                {
                    Channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            Channel.BasicConsume(mqAttribute.QueueName, false, consumer);
        }

        /// <summary>
        /// 拉取消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actionHandle"></param>
        public void Pull<T>(Action<T> actionHandle) where T : class
        {
            var mqAttribute = MQAttribute.GetMQAttribute<T>();
            CreateExchangeAndQueue(mqAttribute, isConsumer: true);
            var result = Channel.BasicGet(mqAttribute.QueueName, false);
            if (result.IsNull())
            {
                return;
            }
            var body = result.Body;
            var messageStr = Encoding.UTF8.GetString(body);
            var message = messageStr.FromJson<T>();
            try
            {
                actionHandle(message);
            }
            finally
            {
                Channel.BasicAck(result.DeliveryTag, false);
            }
        }
        public void Dispose()
        {
            channel?.Close();
            channel?.Dispose();
        }
    }
}
