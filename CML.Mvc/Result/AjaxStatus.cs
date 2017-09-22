using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Mvc.Result
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：AjaxStatus.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：AjaxStatus
    /// 创建标识：cml 2017/9/21 10:33:27
    /// </summary>
    public enum AjaxStatus
    {
        /// <summary>
        /// 失败
        /// </summary>
        Failed = 0,

        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 未登录
        /// </summary>
        NoLogin = 1000,

        /// <summary>
        /// 未授权
        /// </summary>
        NoPerssion = 2000
    }
}
