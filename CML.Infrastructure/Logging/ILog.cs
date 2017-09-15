using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILog
    {
        bool IsDebugEnabled { get; }

        void Debug(object message);

        void Debug<T>(T obj)
            where T : class;

        void DebugFormat(string format, params object[] args);
            
        void Debug(object message, Exception exception);

        void Info(object message);

        void InfoFormat(string format, params object[] args);

        void Info(object message, Exception exception);

        void Error(object message);

        void ErrorFormat(string format, params object[] args);

        void Error(object message, Exception exception);

        void Warn(object message);

        void WarnFormat(string format, params object[] args);

        void Warn(object message, Exception exception);

        void Fatal(object message);

        void FatalFormat(string format, params object[] args);

        void Fatal(object message, Exception exception);
    }
}
