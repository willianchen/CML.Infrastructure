using CML.Infrastructure.Extension;
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
    /// 类名：BaseJsonResult.cs
    /// 类功能描述：BaseJsonResult
    /// 创建标识：cml 2017/9/21 13:46:18
    /// </summary>
    public class BaseJsonResult : JsonResult
    {

        /// <summary>
        /// 仅支持Get请求
        /// </summary>
        public BaseJsonResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="data"></param>
        public BaseJsonResult(object data)
            : this()
        {
            Data = data;
        }

        /// <summary>
        /// Application/Json内容格式返回
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("不支持Get请求");
            }

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrWhiteSpace(ContentType) ? ContentType : "application/json";
            ContentEncoding = Encoding.UTF8;
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                response.Write(Data.ToJson());
            }
        }
    }
}