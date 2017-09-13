namespace CML.Infrastructure.Extension
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：StringExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：StringExtension
    /// 创建标识：cml 2017/5/24 11:15:18
    /// </summary>
    public static class StringExtension
    {
        public static bool IsNullOrWhiteSpace(this string o)
        {
            return string.IsNullOrWhiteSpace(o) ? true : false;
        }

        public static bool IsNullOrEmpty(this string o)
        {
            return string.IsNullOrEmpty(o);
        }
    }
}
