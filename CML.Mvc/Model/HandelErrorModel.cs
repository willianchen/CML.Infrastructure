using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Mvc.Model
{
    /// <summary>
    /// Copyright (C) cml 版权所有。
    /// 类名：HandelErrorModel.cs
    /// 类功能描述：HandelErrorModel
    /// 创建标识：cml 2017/9/25 11:01:18
    /// </summary>
    public class HandelErrorModel
    {
        public HandelErrorModel()
        {
            CreateTime = DateTime.Now;
        }

        public HandelErrorModel(string errorMsg) : this()
        {
            ErrorMsg = errorMsg;
        }

        public HandelErrorModel(Exception ex) : this(ex.Message)
        {
        }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}
