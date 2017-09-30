using CML.Infrastructure.Extension;
using CML.Infrastructure.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace CML.Mvc.Utils
{
    /// <summary>
    /// Copyright (C) cml 版权所有。
    /// 类名：PageUtil.cs
    /// 类功能描述：PageUtil
    /// 创建标识：cml 2017/9/25 16:56:57
    /// </summary>
    public  static class PageHtmlUtil
    {
        /// <summary>
        /// 页面索引参数名
        /// </summary>
        private static string _pageIndexUrlPara = "PageIndex";

        /// <summary>
        /// 最新样式的分页格式
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        public static MvcHtmlString ShowPage(this HtmlHelper htmlHelper, IPageResult pageResult)
        {
            int pageSize = pageResult?.PageSize ?? 30;
            int currentPage = pageResult?.PageIndex ?? 1;
            int totalCount = pageResult?.TotalCount ?? 0;
            int pageCount = pageResult?.PageCount ?? 0;
            int prevPage = currentPage - 1;
            prevPage = prevPage > 0 ? prevPage : 1;
            int nextPage = currentPage + 1;
            nextPage = nextPage > pageCount ? pageCount : nextPage;
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<ul class='paging-box'>");
            //前一页
            htmlBuilder.AppendFormat(CreatePageHtml(htmlHelper, prevPage, "<"));
            //首页
            htmlBuilder.Append(CreatePageHtml(htmlHelper, 1, "<<"));
            int startPage = currentPage - 3 < 1 ? 1 : currentPage - 3;
            int endPage = currentPage + 4 > pageCount ? pageCount : currentPage + 4;

            for (int i = startPage; i <= endPage; i++)
            {
                bool isCurrent = (i == currentPage);
                htmlBuilder.Append(CreatePageHtml(htmlHelper, i, i.ToString(), isCurrent));
            }
            //末页
            htmlBuilder.Append(CreatePageHtml(htmlHelper, pageCount, ">>"));
            //后一页
            htmlBuilder.AppendFormat(CreatePageHtml(htmlHelper, nextPage, ">"));

            htmlBuilder.Append("</ul>");
            return MvcHtmlString.Create(htmlBuilder.ToString());
        }

        private static string CreatePageHtml(this HtmlHelper htmlHelper, int pageIndex, string pageTitle, bool isCurrent = false)
        {
            if (isCurrent)
                return string.Format("<li class='page-item {1}' disabled><a>{0}</a></li>", pageTitle, "page-item-active");
            else
                return string.Format("<li class='page-item'><a title='转到第{0}页' href='{1}'>{2}</a></li>", pageIndex.ToString(), GenerateUrl(htmlHelper, pageIndex), pageTitle);
        }

        /// <summary>
        /// 旧版本的分页样式
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        public static MvcHtmlString SelfPager(this HtmlHelper htmlHelper, IPageResult pageResult)
        {
            int pageSize = pageResult?.PageSize ?? 30;
            int currentPage = pageResult?.PageIndex ?? 1;
            int totalCount = pageResult?.TotalCount ?? 0;
            int pageCount = pageResult?.PageCount ?? 0;
            int prevPage = currentPage - 1;
            prevPage = prevPage > 0 ? prevPage : 1;
            int nextPage = currentPage + 1;
            nextPage = nextPage > pageCount ? pageCount : nextPage;

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<div class='page-info' style='width:95%;text-align:right;'>");
            htmlBuilder.Append("<table width='100%' border='0' cellpadding='0' cellspacing='0'>");
            htmlBuilder.Append("<tbody>");
            htmlBuilder.Append("<tr>");
            htmlBuilder.AppendFormat("<td valign='bottom' align='left' nowrap='true' style='width:40%;'>共 {0} 条记录,<font color='red'><b>{1}</b></font>/{2}页</td>", totalCount.ToString(), currentPage.ToString(), pageCount.ToString());//显示记录数
            htmlBuilder.Append("<td valign='bottom' align='right' nowrap='true' class='' style='width:60%;'>");

            //显示页码
            if (pageCount >= 1)
            {
                string routeUrl = htmlHelper.GetUrlFormat();

                htmlBuilder.Append(GetHrefInfo(1, "首页", currentPage == 1, routeUrl));//首页
                //上一页
                htmlBuilder.Append(GetHrefInfo(prevPage, "上一页", currentPage == 1, routeUrl));//上一页

                //中间页码条 前4后5
                int startPageIndex = currentPage - 4;
                int endPageIndex = currentPage + 5;
                if (startPageIndex > 1)
                {
                    htmlBuilder.Append(GetHrefInfo(1, "...", true, routeUrl));
                }
                for (int i = startPageIndex; i < endPageIndex; i++)
                {
                    if (i > 0 && i <= pageCount)
                    {
                        htmlBuilder.Append(GetHrefInfo(i, i.ToString(), i == currentPage, routeUrl));
                    }
                }
                if (endPageIndex < pageCount)
                {
                    htmlBuilder.Append(GetHrefInfo(1, "...", true, routeUrl));
                }

                //下一页
                htmlBuilder.Append(GetHrefInfo(nextPage, "下一页", nextPage <= currentPage, routeUrl));//下一页
                //尾页
                htmlBuilder.Append(GetHrefInfo(pageCount, "尾页", currentPage == pageCount, routeUrl));//尾页
            }

            htmlBuilder.Append("</td>");
            htmlBuilder.Append("</tr>");
            htmlBuilder.Append("</tbody>");
            htmlBuilder.Append("</table>");

            htmlBuilder.Append("</div>");

            return MvcHtmlString.Create(htmlBuilder.ToString());
        }

        /// <summary>
        /// 获取地址参数信息
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns>地址参数信息</returns>
        public static string GetUrlFormat(this HtmlHelper htmlHelper)
        {
            RouteValueDictionary routeValueDic;
            if (htmlHelper.ViewContext.RouteData.Values != null)
            {
                routeValueDic = new RouteValueDictionary(htmlHelper.ViewContext.RouteData.Values);
            }
            else
            {
                routeValueDic = new RouteValueDictionary();
            }
            string pageIndexUrlPara = _pageIndexUrlPara;
            var queryUrlString = htmlHelper.ViewContext.HttpContext.Request.QueryString;
            if (queryUrlString.IsNotNull())
            {
                foreach (string urlParamKey in queryUrlString.Keys)
                {
                    if (urlParamKey.IsNotNullAndNotEmptyWhiteSpace())
                    {
                        routeValueDic[urlParamKey] = queryUrlString[urlParamKey];
                    }
                }
            }
            var formData = htmlHelper.ViewContext.HttpContext.Request.Form;
            if (formData != null && formData.Count > 0)
            {
                foreach (string key in formData.Keys)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        routeValueDic[key] = formData[key];
                    }
                }
            }
            routeValueDic[pageIndexUrlPara] = "99919";//先给个默认页面，后面好批量替换
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var routeUrl = urlHelper.RouteUrl(routeValueDic);
            return routeUrl.Replace("99919", "{0}"); ;
        }

        /// <summary>
        /// 获取链接信息
        /// </summary>
        /// <param name="pageIndex">当前页面id</param>
        /// <param name="desc">显示内容</param>
        /// <param name="isDisabled">是否无效(不能点击)</param>
        /// <param name="routeUrlFormat">路由信息</param>
        /// <returns>链接信息</returns>
        private static string GetHrefInfo(int pageIndex, string desc, bool isDisabled, string routeUrlFormat)
        {
            if (isDisabled)
            {
                return string.Format("<a disabled='true' style='margin-right:5px;'>{0}</a>", desc);
            }
            else
            {
                string url = string.Format(routeUrlFormat, pageIndex.ToString());
                return string.Format("<a title='转到第{0}页' href='{1}' style='margin-right:5px;'>{2}</a>", pageIndex.ToString(), url, desc);
            }
        }

        private static string GenerateUrl(this HtmlHelper htmlHelper, int pageIndex)
        {
            ViewContext viewContext = htmlHelper.ViewContext;//上下文

            var routeValues = new RouteValueDictionary(viewContext.RouteData.Values);
            AddQueryStringToRouteValues(routeValues, viewContext);
            routeValues[_pageIndexUrlPara] = pageIndex;
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var routeUrl = urlHelper.RouteUrl(routeValues);
            return routeUrl;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="routeValues"></param>
        /// <param name="viewContext"></param>
        private static void AddQueryStringToRouteValues(RouteValueDictionary routeValues, ViewContext viewContext)
        {
            if (routeValues == null)
                routeValues = new RouteValueDictionary();
            var rq = viewContext.HttpContext.Request.QueryString;
            if (rq != null && rq.Count > 0)
            {
                foreach (string key in rq.Keys)
                {
                    //Add url parameter to route values
                    if (!string.IsNullOrEmpty(key))//&& Array.IndexOf(invalidParams, key.ToLower()) < 0
                    {
                        var kv = rq[key];
                        routeValues[key] = kv;
                    }
                }
            }
            var formData = viewContext.HttpContext.Request.Form;
            if (formData != null && formData.Count > 0)
            {
                foreach (string key in formData.Keys)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        var kv = formData[key];
                        routeValues[key] = kv;
                    }
                }
            }
        }
    }
}