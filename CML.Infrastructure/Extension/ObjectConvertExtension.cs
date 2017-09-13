using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Extension
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：ObjectConvertExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ObjectExtension
    /// 创建标识：cml 2017/5/24 11:15:59
    /// </summary>
    public static class ObjectConvertExtension
    {
        /// <summary>
        /// Int 转换 默认为0
        /// </summary>
        /// <param name="o"></param>
        /// <param name="defaultRet"></param>
        /// <returns></returns>
        public static int ToInt32(this object o, int defaultRet = 0)
        {
            if (o != null && !string.IsNullOrWhiteSpace(o.ToString()))
            {
                string s = o.ToString().ToLower();
                int num;
                if (int.TryParse(s, out  num))
                {
                    return num;
                }
            }
            return defaultRet;
        }

        /// <summary>
        /// Decimal转换 默认0
        /// </summary>
        /// <param name="o"></param>
        /// <param name="defaultRet"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object o, decimal defaultRet = 0)
        {
            if (o != null && !string.IsNullOrWhiteSpace(o.ToString()))
            {
                string s = o.ToString().ToLower();
                decimal num;
                if (decimal.TryParse(s, out  num))
                {
                    return num;
                }
            }
            return defaultRet;
        }

        /// <summary>
        /// Bool转换 默认false
        /// </summary>
        /// <param name="o"></param>
        /// <param name="defaultRet"></param>
        /// <returns></returns>
        public static bool ToBool(this object o, bool defaultRet = false)
        {
            if (o != null && !string.IsNullOrWhiteSpace(o.ToString()))
            {
                string s = o.ToString().ToLower();
                switch (s)
                {
                    case "1":
                        return true;
                    case "0":
                        return false;
                }
                bool num;
                if (bool.TryParse(s, out  num))
                {
                    return num;
                }
            }
            return defaultRet;
        }
    }
}
