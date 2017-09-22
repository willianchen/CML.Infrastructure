using CML.Infrastructure.Components;
using CML.Mvc.Authorization;
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
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：AuthorizeAttribute.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：身份认证过滤器
    /// 创建标识：cml 2017/9/21 10:23:30
    /// </summary>
    public class AuthorizeAttribute : ActionFilterAttribute
    {
        private ILoginChecker loginCheck;

        /// <summary>
        /// Init
        /// </summary>
        public AuthorizeAttribute()
        {
            loginCheck = ObjectContainer.Resolve<ILoginChecker>();
        }

        /// <summary>
        /// 登录认证
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            //判断是否匿名访问
            bool isAnonymous = ControllerContextUtil.IsDefineAttribute<AllowAnonymousAttribute>(actionContext);

            if (!isAnonymous)
            {
                if (!loginCheck.IsLogined(actionContext))
                {
                    //Ajax请求 返回值
                    if (ControllerContextUtil.IsAjaxRequest(actionContext))
                    {
                        actionContext.Result = ResultUtil.NoLogin(loginCheck.GetRedirectUrl(actionContext));
                    }
                    else
                    {
                        actionContext.HttpContext.Response.Redirect(loginCheck.GetRedirectUrl(actionContext));
                        return;
                    }
                }
            }
            //base.OnActionExecuting(actionContext);
        }
    }
}
