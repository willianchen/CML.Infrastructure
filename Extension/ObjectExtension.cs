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
    }
}
