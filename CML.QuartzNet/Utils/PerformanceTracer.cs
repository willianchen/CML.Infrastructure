using CML.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.QuartzNet.Utils
{
    /// <summary>
    /// Copyright (C) cml 版权所有。
    /// 类名：PerformanceTracer.cs
    /// 类功能描述：运行监控类
    /// 创建标识：cml 2017/9/14 15:42:29
    /// </summary>
    public class PerformanceTracer
    {
        private static int performanceTracer;

        static PerformanceTracer()
        {

            if (!string.IsNullOrWhiteSpace(ConfigUtil.GetValue("PERFORMANCE_TRACE")))
            {
                int.TryParse(ConfigUtil.GetValue("PERFORMANCE_TRACE"), out performanceTracer);
            }
            else
            {
                performanceTracer = -1;
            }
        }

        /// <summary>
        /// 调用并且跟踪指定代码块的执行时间。
        /// </summary>
        /// <param name="action"></param>
        /// <param name="traceName">跟踪名称。</param>
        /// <param name="enableThrow">指定是否允许抛出异常。</param>
        public static void Invoke(Action action, string traceName = null, bool enableThrow = true)
        {
            if (performanceTracer >= 0)
            {
                if (string.IsNullOrWhiteSpace(traceName))
                {
                    var method = new StackTrace().GetFrames()[1].GetMethod();

                    traceName = string.Format("{0}.{1}", method.ReflectedType.FullName, method.Name);
                }

                try
                {
                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();

                    action();

                    stopwatch.Stop();

                    LogUtil.Info("(" + traceName + ")运行总时间：" + stopwatch.Elapsed.TotalMilliseconds + "ms");
                    if (stopwatch.Elapsed.TotalMilliseconds > performanceTracer)
                    {
                        LogUtil.Warn(string.Format("性能问题({0})，耗时{1}毫秒。", traceName, stopwatch.Elapsed.TotalMilliseconds));
                    }
                }
                catch (Exception ex)
                {
                    if (enableThrow)
                    {
                        throw;
                    }
                    LogUtil.Error(ex);
                }
            }
            else
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    if (enableThrow)
                    {
                        throw;
                    }

                    LogUtil.Error(ex);
                }
            }
        }
    }
}
