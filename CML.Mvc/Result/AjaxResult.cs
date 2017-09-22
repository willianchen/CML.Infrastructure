using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Mvc.Result
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：AjaxResult.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：AjaxResult
    /// 创建标识：cml 2017/9/21 10:34:58
    /// </summary>
    public class AjaxResult
    {
        public AjaxResult()
        {
        }

        public AjaxResult(AjaxStatus status)
        {
            this.Status = status;
        }

        public AjaxResult(AjaxStatus status, string message) : this(status)
        {
            this.Message = message;
        }

        public AjaxResult(AjaxStatus status, object data) : this(status)
        {
            this.Data = data;
        }

        public AjaxResult(AjaxStatus status, object data, string message) : this(status, message)
        {
            this.Data = data;
        }

        /// <summary>
        /// 状态码
        /// 1成功 0失败
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public AjaxStatus Status { get; set; }

        /// <summary>
        /// 内容说明
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

        /// <summary>
        /// 返回Url
        /// </summary>
        [JsonProperty(PropertyName = "redirecturl")]
        public string RedirectUrl { get; set; }

        public static AjaxResult Success(object data, string message = "")
        {
            return new AjaxResult(AjaxStatus.Success, data, message);
        }

        public static AjaxResult Failed(string message)
        {
            return new AjaxResult(AjaxStatus.Failed, message);
        }

        public static AjaxResult NoLogin(string url)
        {
            return new AjaxResult(AjaxStatus.NoLogin) { RedirectUrl = url };
        }

        public static AjaxResult NoPermission(string message = "")
        {
            return new AjaxResult(AjaxStatus.NoPerssion, message);
        }
    }
}
