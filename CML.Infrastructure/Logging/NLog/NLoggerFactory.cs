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
    /// 类名：NLoggerFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：NLoggerFactory
    /// 创建标识：cml 2017/6/30 15:27:31
    /// </summary>
    public class NLoggerFactory : ILogFactory
    {
        public ILog Create(string name)
        {
            return new NLogger(LogManager.GetLogger(name));
        }

        public ILog Create(Type type)
        {
            return new NLogger(LogManager.GetLogger(type.Name, type));
        }
    }
}
