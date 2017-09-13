using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.MQ
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：MQConfig.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：MQConfig
    /// 创建标识：cml 2017/5/23 22:35:00
    /// </summary>
    public class MQConfig
    {
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="_host"></param>
        /// <param name="_userName"></param>
        /// <param name="_password"></param>
        public MQConfig(string _host, string _userName, string _password)
        {
            Host = _host;
            UserName = _userName;
            Password = _password;
        }
        /// <summary>
        /// 连接地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 用户名 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 心跳时间
        /// </summary>
        public ushort Heartbeat { get; set; } = 60;

        /// <summary>
        /// 重连时间
        /// </summary>
        public TimeSpan NetworkRecoveryInterval { get; set; } = TimeSpan.FromSeconds(60);

        public override string ToString()
        {
            return $"{ Host}_{UserName}";
        }
    }


    public static class ExchangeType
    {
        public const string Direct = "direct";
        public const string Fanout = "fanout";
        public const string Topic = "topic";
    }
}
