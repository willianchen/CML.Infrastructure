using CML.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Resposity.Test
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：DemoResposity.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：DemoResposity
    /// 创建标识：cml 2017/8/19 15:24:12
    /// </summary>
    public class DemoResposity : BaseRepository<DemoInfo>
    {
        public DemoResposity() : base("Demo", "Demo")
        {

        }
    }
}
