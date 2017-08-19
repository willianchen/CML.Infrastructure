using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.DataAccess.Extensions
{
    /// <summary>
    /// Copyright (C) 阿礼 版权所有。
    /// 类名：DynamicParametersExtension.cs
    /// 类功能描述： DynamicParameters拓展方法
    /// 创建标识：阿礼 2017/8/14 14:58:58
    /// </summary>
    public static class DynamicParametersExtension
    {

        public static DynamicParameters AddParam(this DynamicParameters obj, object param)
        {
            obj.AddDynamicParams(param);
            return obj;
        }
    }
}
