using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.MongoDB
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：MongoDBUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：MongoDB帮助类
    /// 创建标识：cml 2017/5/31 15:07:48
    /// </summary>
    public static class MongoDBUtil
    {
        /// <summary>
        /// 获取更新参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static UpdateDefinition<T> BuilderDefinition<T>(T entity)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var updateDefinitionList = BuilderDefinitionList<T>(properties, entity);

            var updateDefinitionBuilder = new UpdateDefinitionBuilder<T>().Combine(updateDefinitionList);

            return updateDefinitionBuilder;
        }

        /// <summary>
        /// 获取更新参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="properties"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static List<UpdateDefinition<T>> BuilderDefinitionList<T>(PropertyInfo[] propertyInfos, T entity)
        {
            var updateDefinitionList = new List<UpdateDefinition<T>>();

            propertyInfos = propertyInfos.Where(a => a.Name != "_id").ToArray();

            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType.IsArray || typeof(IList).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    var value = propertyInfo.GetValue(entity) as IList;     

                    var filedName = propertyInfo.Name;

                    updateDefinitionList.Add(Builders<T>.Update.Set(filedName, value));
                }
                else
                {
                    var value = propertyInfo.GetValue(entity);
                    if (propertyInfo.PropertyType == typeof(decimal))
                        value = value.ToString();

                    var filedName = propertyInfo.Name;

                    updateDefinitionList.Add(Builders<T>.Update.Set(filedName, value));
                }
            }

            return updateDefinitionList;
        }
    }
}
