using CML.Mvc.Result;
using CML.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CML.Mvc.Filter
{
    /// <summary>
    /// Copyright (C) cml 版权所有。
    /// 类名：ModelStateAttribute.cs
    /// 类功能描述：字段验证
    /// 创建标识：cml 2017/9/25 11:19:34
    /// </summary>
    public class ModelStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 验证参数
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            ModelStateDictionary modelState = actionContext.Controller.ViewData.ModelState;
            if (!modelState.IsValid)
            {
                var error = modelState.GetFirstErrorMsg();
                actionContext.Result = ResultUtil.Failed(error);
            }
        }
    }
}
