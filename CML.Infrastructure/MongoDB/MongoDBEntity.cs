using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.MongoDB
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：MongoDBEntity.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：MongoDB实体类
    /// 创建标识：cml 2017/5/31 14:26:56
    /// </summary>
    public class MongoDBEntity
    {
        public MongoDBEntity()
        {
            _id = ObjectId.GenerateNewId().ToString();
        }
        public string _id { get; set; }



    }
}
