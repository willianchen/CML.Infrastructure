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
        private readonly ILog _logger;

        public LogUtil(ILogFactory loggerFactory)
        {
            _logger = loggerFactory.Create(GetType().FullName);
        }

        public  void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        public void Info(string msg)
        {
            _logger.Info(msg);
        }

        public void Error(string msg)
        {
            _logger.Error(msg);
        }
    }
}
