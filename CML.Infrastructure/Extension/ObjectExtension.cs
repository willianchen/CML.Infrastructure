namespace CML.Infrastructure.Extension
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：ObjectExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ObjectExtension
    /// 创建标识：cml 2017/5/24 18:27:57
    /// </summary>
    public static class ObjectExtension
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return !IsNull(obj);
        }

        /// <summary>
        /// 判断字符串不为Null并且不为空且不是由空白字符组成
        /// </summary>
        /// <param name="input">要判断的信息</param>
        /// <returns>如果不为Null且不为空且不是由空白字符组成则返回true</returns>
        public static bool IsNotNullAndNotEmptyWhiteSpace(this object input)
        {
            return !IsNullOrEmptyWhiteSpace(input);
        }

        /// <summary>
        /// 判断字符串为Null或者为空
        /// </summary>
        /// <param name="input">要判断的信息</param>
        /// <returns>如果为Null或者为空则返回true</returns>
        public static bool IsNullOrEmpty(this object input)
        {
            if (input == null)
            {
                return true;
            }
            return string.IsNullOrEmpty(input.ToString());
        }

        /// <summary>
        /// 判断字符串不为Null并且不为空
        /// </summary>
        /// <param name="input">要判断的信息</param>
        /// <returns>如果不为Null且不为空则返回true</returns>
        public static bool IsNotNullAndNotEmpty(this object input)
        {
            return !IsNullOrEmpty(input);
        }

        /// <summary>
        /// 判断字符串为Null或者为空且或者由空白字符组成
        /// </summary>
        /// <param name="input">要判断的信息</param>
        /// <returns>如果为Null且或者为空且或者由空白字符组成则返回true</returns>
        public static bool IsNullOrEmptyWhiteSpace(this object input)
        {
            if (input == null)
            {
                return true;
            }
            return string.IsNullOrWhiteSpace(input.ToString());
        }
    }
}
