using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：NLogger.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：NLogger
    /// 创建标识：cml 2017/6/30 14:33:10
    /// </summary>
    public class NLogger : ILog
    {
        public bool IsDebugEnabled { get { return true; } }

        private readonly ILogger _logger;


        public NLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void Debug(object message)
        {
            _logger.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            _logger.Debug(exception, message.ToString());
        }
        public void Debug<T>(T obj)
            where T : class
        {
            _logger.Debug<T>(obj);
        }

        public void DebugFormat(string format, params object[] args)
        {
            _logger.Debug(format, args);
        }

        public void Error(object message)
        {
            _logger.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            _logger.Error(exception, message.ToString());
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _logger.Error(format, args);
        }

        public void Fatal(object message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            _logger.Fatal(exception, message.ToString());
        }

        public void FatalFormat(string format, params object[] args)
        {
            _logger.Fatal(format, args);
        }

        public void Info(object message)
        {
            _logger.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            _logger.Info(exception, message.ToString());
        }

        public void InfoFormat(string format, params object[] args)
        {
            _logger.Info(format, args);
        }

        public void Warn(object message)
        {
            _logger.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            _logger.Warn(exception, message.ToString());
        }

        public void WarnFormat(string format, params object[] args)
        {
            _logger.Warn(format, args);
        }


    }
}
