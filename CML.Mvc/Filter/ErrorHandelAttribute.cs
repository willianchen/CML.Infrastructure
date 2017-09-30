using CML.Infrastructure.Utils;
using CML.Mvc.Model;
using CML.Mvc.Result;
using CML.Mvc.Utils;
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
    /// 类名：ErrorHandelAttribute.cs
    /// 类功能描述：ErrorHandelAttribute
    /// 创建标识：cml 2017/9/25 11:10:19
    /// </summary>
    public class ErrorHandelAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            LogUtil.Error(filterContext.Exception, memberName: "ErrorHandelAttribute-OnException");
            filterContext.ExceptionHandled = true;
            var isAjax = ContextUtil.IsAjaxRequest(filterContext);
            if (isAjax)
            {
                filterContext.Result = ResultUtil.Failed("服务器异常,请联系管理员");
            }
            else
            {
                UrlHelper url = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new ViewResult() { ViewName = "~/Views/Error/Error.cshtml", ViewData = new ViewDataDictionary<HandelErrorModel>(new HandelErrorModel(filterContext.Exception)) };
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }
        }
    }
}
