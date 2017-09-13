using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Resposity.Test
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：Demo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：AdminInfo
    /// 创建标识：cml 2017/8/19 15:26:02
    /// </summary>
    public sealed class DemoInfo
    {
        /// <summary>
        /// 管理员ID,主键、自增
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string FName { get; set; }

        public int FAge { get; set; }

        public DateTime FBirthDay { get; set; }
    }
}