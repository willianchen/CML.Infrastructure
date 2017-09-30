using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CML.Mvc.Utils
{
    /// <summary>
    /// Copyright (C) cml 版权所有。
    /// 类名：ControllerContextUtil.cs
    /// 类功能描述：上下文帮助类
    /// 创建标识：cml 2017/9/22 16:40:49
    /// </summary>
    public class ContextUtil
    {
        /// <summary>
        /// 判断是否是Ajax请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("暂无上下文");
            return context.RequestContext.HttpContext.Request.IsAjaxRequest();
        }

        /// <summary>
        /// 判断是否定义过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsDefineAttribute<T>(ActionExecutingContext context) where T : Attribute
        {
            bool isDefine = context.ActionDescriptor.IsDefined(typeof(T), inherit: true)
                                      || context.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(T), inherit: true);
            return isDefine;
        }

        /// <summary>
        /// 获取当前请求URL
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRequestUrl(ControllerContext context)
        {
            return context.HttpContext.Request.Url.ToString();
        }
    }
}
