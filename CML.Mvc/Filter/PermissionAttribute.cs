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
    /// Copyright (C) cml 版权所有。
    /// 类名：PermissionAttribute.cs
    /// 类功能描述：权限控制过滤器
    /// 创建标识：cml 2017/9/21 14:07:55
    /// </summary>
    public class PermissionAttribute : ActionFilterAttribute
    {
        private IPermissionChecker _permissionChecker;
        private string _roleEvent;
        private string _parentPageUrl;
        private string _currentPageUrl;
        private string _pageParams;

        /// <summary>
        /// Init
        /// </summary>
        public PermissionAttribute()
        {
            _permissionChecker = ObjectContainer.Resolve<IPermissionChecker>();
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="roleEvent">权限</param>
        public PermissionAttribute(string roleEvent) : this()
        {
            _roleEvent = roleEvent;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="parentPageUrl">父页面URL</param>
        /// <param name="roleEvent">权限</param>
        public PermissionAttribute(string parentPageUrl, string roleEvent) : this(roleEvent)
        {
            _parentPageUrl = parentPageUrl;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="parentPageUrl"></param>
        /// <param name="pageParams"></param>
        public PermissionAttribute(string parentPageUrl, string roleEvent, string pageParams) : this(parentPageUrl, roleEvent)
        {
            _parentPageUrl = parentPageUrl;
            _pageParams = pageParams;
        }




        #region Get Set

        /// <summary>
        /// 事件,如果是列表打开查看的方法，则可传空
        /// </summary>
        public string RoleEvent
        {
            get
            {
                return _roleEvent;
            }
        }

        /// <summary>
        /// 父页面URL
        /// </summary>
        public string ParentPageUrl
        {
            get { return _parentPageUrl; }
        }

        /// <summary>
        /// 页面参数 验证父页面参数时，子页面必须带相同参数，否则权限不通过
        /// </summary>
        public string PageParams
        {
            get { return _pageParams; }
        }

        /// <summary>
        /// 当前页面Url
        /// </summary>
        public string CurrentPageUrl
        {
            get { return _currentPageUrl; }
        }

        #endregion

        /// <summary>
        /// 权限认证
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            //判断是否跳过权限验证
            bool isAnonymous = ContextUtil.IsDefineAttribute<PassPermissionAttribute>(actionContext);

            if (!isAnonymous)
            {
                string currentPageUrl = ContextUtil.GetRequestUrl(actionContext);
                string parentPageUrl = currentPageUrl;
                if (!string.IsNullOrWhiteSpace(ParentPageUrl))
                    parentPageUrl = ParentPageUrl;

                //拼接Url 前缀
                if (!parentPageUrl.StartsWith("http://") && !parentPageUrl.StartsWith("https://"))
                    parentPageUrl = actionContext.HttpContext.Request.Url.Scheme + "://" + actionContext.HttpContext.Request.Url.Authority + (parentPageUrl.StartsWith("/") ? parentPageUrl : "/" + parentPageUrl);

                var isGranted = _permissionChecker.IsGranted(parentPageUrl, currentPageUrl, RoleEvent, PageParams);
                var isAjax = ContextUtil.IsAjaxRequest(actionContext);

                if (!isGranted)
                    if (isAjax)
                    {
                        actionContext.Result = ResultUtil.NoPermission();
                    }
                    else
                    {
                        actionContext.Result = new ViewResult() { ViewName = "~/Views/Shared/NoPermission.cshtml" };
                    }
            }
            base.OnActionExecuting(actionContext);
        }
    }

    /// <summary>
    /// 跳过权限验证Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PassPermissionAttribute : Attribute
    {
    }
}
