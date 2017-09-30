using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CML.Mvc.Result
{
    /// <summary>
    /// Copyright (C) cml 版权所有。
    /// 类名：JavascriptContentUitl.cs
    /// 类功能描述：Javascript执行类
    /// 创建标识：cml 2017/9/25 16:52:03
    /// </summary>
    public class JavascriptContentUitl
    {
        private static string javascriptContent = "<script>{0}</script>";
        /// <summary>
        /// Alert消息
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static ContentResult MessageAlert(string content)
        {
            string scriptContent = string.Format(javascriptContent, string.Format("alert('{0}')", content.Replace("'", @"\'")));
            return new ContentResult() { Content = scriptContent };
        }

        /// <summary>
        /// Alert消息并返回前一页
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static ContentResult MessageAlertAndReturn(string content)
        {
            string scriptContent = string.Format(javascriptContent, string.Format("alert('{0}');window.history.back();</script", content.Replace("'", @"\'")));
            return new ContentResult() { Content = scriptContent };
        }

        /// <summary>
        /// 弹出消息并跳转
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="redirectUrl">跳转地址</param>
        /// <returns></returns>
        public static ContentResult MessageAlertAndRedirect(string content, string redirectUrl)
        {
            string scriptContent = string.Format(javascriptContent, string.Format("alert('{0}');window.location.href='{1}'</script", content.Replace("'", @"\'")));
            return new ContentResult() { Content = scriptContent };
        }
    }
}
