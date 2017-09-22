using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Mvc.Result
{
    /// <summary>
    /// Copyright (C) cml 版权所有。
    /// 类名：ResultUtil.cs
    /// 类功能描述：ResultUtil
    /// 创建标识：cml 2017/9/21 13:52:25
    /// </summary>
    public class ResultUtil
    {
        /// <summary>
        /// 返回未登录Json结果
        /// </summary>
        /// <returns></returns>
        public static BaseJsonResult NoLogin(string url)
        {
            return new BaseJsonResult(AjaxResult.NoLogin(url));
        }

        /// <summary>
        /// 返回无权限结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static BaseJsonResult NoPermission(string msg = "")
        {
            return new BaseJsonResult(AjaxResult.NoPermission(msg));
        }

        /// <summary>
        /// 返回失败结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static BaseJsonResult Failed(string msg)
        {
            return new BaseJsonResult(AjaxResult.Failed(msg));
        }

        /// <summary>
        /// 返回成功结果
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static BaseJsonResult Success(object data, string msg)
        {
            return new BaseJsonResult(AjaxResult.Success(data, msg));
        }
    }
}
