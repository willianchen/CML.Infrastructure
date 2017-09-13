using CML.Infrastructure.Extension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.MongoDB
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：MongoDBAttribute.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：MongoDB实体标签类
    /// 创建标识：cml 2017/5/31 14:12:35
    /// </summary>
    public class MongoDBAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_database"></param>
        /// <param name="_collection"></param>
        public MongoDBAttribute(string _database, string _collection)
        {
            Database = _database;
            Collection = _collection;
        }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 文档名称
        /// </summary>

        public string Collection { get; set; }

        private static ConcurrentDictionary<RuntimeTypeHandle, MongoDBAttribute> _cacheAttribute = new ConcurrentDictionary<RuntimeTypeHandle, MongoDBAttribute>();
        public static MongoDBAttribute GetMongoDBAttribute<T>()
        {
            var typeT = typeof(T);
            var typeHandle = typeT.TypeHandle;
            //return typeT.GetAttribute<MQAttribute>();
            return _cacheAttribute.GetValue(typeHandle, () =>
            {
                return typeT.GetAttribute<MongoDBAttribute>();
            });
        }
    }
}
