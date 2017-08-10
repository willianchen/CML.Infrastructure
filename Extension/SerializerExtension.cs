namespace CML.Infrastructure.Extension
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：SerializerExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：SerializerExtension
    /// 创建标识：cml 2017/5/24 20:14:43
    /// </summary>
    public static class SerializerExtension
    {
        public static string ToJson(this object o)
        {
            return new NewtonsoftJsonSerializer().Serialize(o);
        }

        public static byte[] ToJsonUTF8(this object o)
        {
            return new NewtonsoftJsonSerializer().SerializeByteUTF8(o);
        }

        public static T FromJson<T>(this string o) where T : class
        {
            return new NewtonsoftJsonSerializer().Deserialize<T>(o);
        }
    }
}
