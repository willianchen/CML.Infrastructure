using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CML.QuartzNet.Model;
using Quartz;
using Quartz.Impl;
using CML.Infrastructure.Utils;
using System.Collections.Specialized;
using System.Reflection;
using Quartz.Impl.Triggers;
using Quartz.Spi;
using System.Collections.Concurrent;

namespace CML.QuartzNet.Utils
{
    /// <summary>
    /// Copyright (C) cml 版权所有。
    /// 类名：QuartzUtil.cs
    /// 类功能描述：QuartzUtil
    /// 创建标识：cml 2017/9/14 15:36:33
    /// </summary>
    public static class QuartzUtil
    {
        /// <summary>
        /// 配置文件地址
        /// </summary>
        private static readonly string TaskPath = FileUtil.GetAbsolutePath("/App_Data/config/TaskConfig.xml");

        /// <summary>
        /// 缓存任务所在程序集信息
        /// </summary>
        private static ConcurrentDictionary<string, Assembly> AssemblyDict = new ConcurrentDictionary<string, Assembly>();
        private static IScheduler scheduler = null;
        private static object obj = new object();



        /// <summary>
        /// 初始化任务调度对象
        /// </summary>
        public static void InitScheduler()
        {
            try
            {
                if (scheduler == null)
                    lock (obj)
                    {
                        if (scheduler == null)
                        {
                            //// 配置文件的方式，配置quartz实例
                            //var properties = new NameValueCollection();
                            //properties["quartz.scheduler.instanceName"] = "EmailSenderQuartzScheduler";

                            //// 设置线程池
                            //properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
                            //properties["quartz.threadPool.threadCount"] = "10";
                            //properties["quartz.threadPool.threadPriority"] = "2";

                            //// 远程输出配置
                            //properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
                            //properties["quartz.scheduler.exporter.port"] = "556";
                            //properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
                            //properties["quartz.scheduler.exporter.channelType"] = "tcp";
                            //ISchedulerFactory schedulerFactory = new StdSchedulerFactory(properties);
                            scheduler = StdSchedulerFactory.GetDefaultScheduler();
                            LogUtil.Info("任务调度初始化成功！");
                        }
                    }
            }
            catch (Exception ex)
            {
                LogUtil.Error($"任务调度初始化失败{ex.ToString()}");
            }
        }

        public static void StartScheduler()
        {
            try
            {
                if (!scheduler.IsStarted)
                {
                    scheduler.Start();

                    ///获取所有执行中的任务
                    List<JobModel> listTask = ReadTaskConfig().ToList();

                    if (listTask != null && listTask.Count > 0)
                    {
                        foreach (JobModel taskUtil in listTask)
                        {
                            try
                            {
                                ScheduleJob(taskUtil);
                            }
                            catch (Exception e)
                            {
                                LogUtil.Error(string.Format("任务“{0}”启动失败！", taskUtil.TaskName) + e.ToString());
                            }
                        }
                    }

                    LogUtil.Info("任务调度启动成功");
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error($"任务调度初始化失败{ex.ToString()}");
            }
        }

        /// <summary>
        /// 停止任务调度
        /// </summary>
        public static void StopSchedule()
        {
            try
            {
                //判断调度是否已经关闭
                if (!scheduler.IsShutdown)
                {
                    //等待任务运行完成
                    scheduler.Shutdown(true);
                    LogUtil.Info("任务调度停止！");
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error($"任务调度停止失败！{ex.ToString()}");
            }
        }

        /// <summary>
        /// 启用任务
        /// <param name="task">任务信息</param>
        /// <returns>返回任务trigger</returns>
        /// </summary>
        public static void ScheduleJob(JobModel task)
        {
            //验证是否正确的Cron表达式
            if (ValidExpression(task.CronExpressionString))
            {
                IJobDetail job = null;
                job = JobBuilder.Create(GetClassInfo(task.AssemblyName, task.ClassName))
                         .WithIdentity(task.TaskID, "group1")
                        .UsingJobData("TaskParam", task.TaskParam)
                      .Build();

                CronTriggerImpl trigger = new CronTriggerImpl();
                trigger.CronExpressionString = task.CronExpressionString;
                trigger.Name = task.TaskID.ToString();
                trigger.Description = task.TaskName;
                scheduler.ScheduleJob(job, trigger);
                if (task.Status == 0)
                {
                    JobKey jk = new JobKey(task.TaskID.ToString());
                    scheduler.PauseJob(jk);
                }
                else
                {
                    LogUtil.Info(string.Format("任务“{0}”启动成功,未来3次运行时间如下:", task.TaskName));
                    List<DateTime> list = GetNextFireTime(task.CronExpressionString, 3);
                    foreach (var time in list)
                    {
                        LogUtil.Info(time.ToString());
                    }
                }
            }
            else
            {
                throw new Exception(task.CronExpressionString + "不是正确的Cron表达式,无法启动该任务!");
            }
        }

        /// <summary>
        /// 读取任务配置节点列表
        /// </summary>
        /// <returns></returns>
        public static IList<JobModel> ReadTaskConfig()
        {
            return XmlUtil.XmlToList<JobModel>(TaskPath, "Task");
        }

        /// <summary>
        /// 获取任务在未来周期内哪些时间会运行
        /// </summary>
        /// <param name="CronExpressionString">Cron表达式</param>
        /// <param name="numTimes">运行次数</param>
        /// <returns>运行时间段</returns>
        public static List<DateTime> GetNextFireTime(string CronExpressionString, int numTimes)
        {
            if (numTimes < 0)
            {
                throw new Exception("参数numTimes值大于等于0");
            }
            //时间表达式
            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(CronExpressionString).Build();
            IList<DateTimeOffset> dates = TriggerUtils.ComputeFireTimes(trigger as IOperableTrigger, null, numTimes);
            List<DateTime> list = new List<DateTime>();
            foreach (DateTimeOffset dtf in dates)
            {
                list.Add(TimeZoneInfo.ConvertTimeFromUtc(dtf.DateTime, TimeZoneInfo.Local));
            }
            return list;
        }


        /// <summary>
        /// 获取任务详细
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static JobModel GetJobDetail(IJobExecutionContext context)
        {
            JobModel job = new JobModel();
            if (context != null)
            {
                job.TaskID = context.Trigger.Key.Name;
                job.TaskName = context.Trigger.Description;
                job.TaskParam = context.JobDetail.JobDataMap.Get("TaskParam") != null ? context.JobDetail.JobDataMap.Get("TaskParam").ToString() : "";
            }
            return job;
        }

        /// <summary>
        /// 校验字符串是否为正确的Cron表达式
        /// </summary>
        /// <param name="cronExpression">带校验表达式</param>
        /// <returns></returns>
        public static bool ValidExpression(string cronExpression)
        {
            return CronExpression.IsValidExpression(cronExpression);
        }

        /// <summary>
        /// 获取类的属性、方法  
        /// </summary>  
        /// <param name="assemblyName">程序集</param>  
        /// <param name="className">类名</param>  
        private static Type GetClassInfo(string assemblyName, string className)
        {
            try
            {
                assemblyName = AppDomain.CurrentDomain.BaseDirectory + @"\" + assemblyName + ".dll";
                Assembly assembly = null;
                if (!AssemblyDict.TryGetValue(assemblyName, out assembly))
                {
                    assembly = Assembly.LoadFrom(assemblyName);
                    AssemblyDict[assemblyName] = assembly;
                }
                Type type = assembly.GetType(className, true, true);
                return type;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
