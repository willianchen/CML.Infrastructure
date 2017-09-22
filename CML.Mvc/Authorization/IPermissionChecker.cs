using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Mvc.Authorization
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：IPermissionChecker.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：IPermissionChecker
    /// 创建标识：cml 2017/9/21 9:47:28
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        /// 是否通过权限验证
        /// </summary>
        /// <param name="parentPageUrl"></param>
        /// <param name="currentPageUrl"></param>
        /// <param name="moduleNames"></param>
        /// <param name="urlParams"></param>
        /// <returns></returns>
        bool IsGranted(string parentPageUrl, string currentPageUrl, string moduleNames = "", string urlParams = "");
    }
}
