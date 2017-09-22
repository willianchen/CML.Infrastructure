using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CML.Mvc.Authorization
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ILoginChecker.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ILoginChecker
    /// 创建标识：cml 2017/9/21 9:45:19
    /// </summary>
    public interface ILoginChecker
    {
        /// <summary>
        /// 验证是否登录
        /// </summary>
        /// <returns></returns>
        bool IsLogined(ActionExecutingContext filterContext);

        /// <summary>
        /// 获取跳转地址
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        string GetRedirectUrl(ActionExecutingContext filterContext);
    }
}
