using CML.Infrastructure.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Utils
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：LogUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：LogUtil
    /// 创建标识：cml 2017/6/30 16:36:44
    /// </summary>
    public class LogUtil
    {

        /// <summary>
        /// 获取创建ILogger工厂
        /// </summary>
        /// <returns>ILogger工厂</returns>
        private static ILogFactory GetLoggerFactory()
        {
            return new NLoggerFactory();
        }


        /// <summary>
        /// 获取ILogger
        /// </summary>
        /// <param name="loggerName">记录器名字</param>
        /// <returns>ILogger</returns>
        private static ILog GetLogger(string loggerName = null)
        {
            if (string.IsNullOrWhiteSpace(loggerName))
            {
                loggerName = "CML.Log.*";
            }
            return GetLoggerFactory().Create(loggerName);
        }


        /// <summary>
        /// 输出调试日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Debug(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Debug(msg);
        }

        /// <summary>
        /// 输出普通日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Info(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Info(msg);
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="loggerName"></param>
        public static void Warn(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Warn(msg);
        }

        /// <summary>
        /// 输出警告日志信息
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="memberName"></param>
        /// <param name="loggerName"></param>
        public static void Warn(Exception ex, string memberName = null, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Warn(ex.ToErrMsg(memberName: memberName));
        }

        /// <summary>
        /// 输出错误日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Error(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Error(msg);
        }

        /// <summary>
        /// 输出错误日志信息
        /// </summary>
        /// <param name="ex">异常信息</param>
        public static void Error(Exception ex, string memberName = null, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Error(ex.ToErrMsg(memberName: memberName));
        }

        /// <summary>
        /// 输出严重错误日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Fatal(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Fatal(msg);
        }

        /// <summary>
        /// 输出严重错误日志信息
        /// </summary>
        /// <param name="ex">异常信息</param>
        public static void Fatal(Exception ex, string memberName = null, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).Fatal(ex.ToErrMsg(memberName: memberName));
        }
    }
}