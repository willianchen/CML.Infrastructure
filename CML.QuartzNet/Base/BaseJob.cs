using CML.Infrastructure.Utils;
using CML.QuartzNet.Model;
using CML.QuartzNet.Utils;
using Quartz;
using System;

namespace CML.QuartzNet.Base
{
    /// <summary>
    /// Copyright (C) cml 版权所有。
    /// 类名：BaseJob.cs
    /// 类功能描述：基础任务类
    /// 创建标识：cml 2017/9/14 14:51:44
    /// </summary>
    public abstract class BaseJob : IJob, IDisposable
    {
        /// <summary>
        /// 子类约定的运行接口方法
        /// </summary>
        public abstract void Run();


        /// <summary>
        /// 系统级稀有资源释放接口,及卸载回调接口
        /// </summary>
        public virtual void Dispose()
        {
            LogUtil.Info("任务结束，资源释放");
        }

        public virtual void Execute(IJobExecutionContext context)
        {
            try
            {
                // 1. job 基本信息及 job 配置的其他参数,任务启动时会读取配置文件节点的值传递过来
                JobModel job = QuartzUtil.GetJobDetail(context);
                // 2. 记录Task 运行状态
                LogUtil.Info(String.Format("任务 {0} {1} 开始", job.TaskID.ToString(), job.TaskName));

                // 3. 开始执行相关任务
                PerformanceTracer.Invoke(() =>
                {
                    this.Run();
                }, job.TaskName);//监控并执行任务

                // 4. 记录Task 运行状态数据库
                LogUtil.Info(String.Format("任务 {0} {1} 结束", job.TaskID, job.TaskName));

            }
            catch (Exception ex)
            {
                JobExecutionException e2 = new JobExecutionException(ex);
                //true  是立即重新执行任务 
                e2.RefireImmediately = true;

                // 记录异常到 log 文件中。
                LogUtil.Error(ex);

            }
        }
    }
}
