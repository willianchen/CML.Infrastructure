using CML.Infrastructure.Serializing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;
using System.Text;


namespace CML.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：NewtonsoftJsonSerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：NewtonsoftJsonSerializer
    /// 创建标识：cml 2017/5/24 19:09:35
    /// </summary>
    public sealed class NewtonsoftJsonSerializer : IJsonSerializer
    {
        public JsonSerializerSettings Settings { get; private set; }

        public NewtonsoftJsonSerializer()
        {
            Settings = new JsonSerializerSettings
            {
                ContractResolver = new CustomContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
            };

        }
        public object Deserialize(string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type, Settings);
        }

        public T Deserialize<T>(string value) where T : class
        {
            return JsonConvert.DeserializeObject<T>(JObject.Parse(value).ToString(), Settings);
        }



        public string Serialize(object obj)
        {
            return obj == null ? null : JsonConvert.SerializeObject(obj, Settings);
        }

        public byte[] SerializeByteUTF8(object obj)
        {
            var value = Serialize(obj);
            return Encoding.UTF8.GetBytes(value);
        }

    }
    class CustomContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);
            if (jsonProperty.Writable) return jsonProperty;
            var property = member as PropertyInfo;
            if (property == null) return jsonProperty;
            var hasPrivateSetter = property.GetSetMethod(true) != null;
            jsonProperty.Writable = hasPrivateSetter;

            return jsonProperty;
        }
    }
}