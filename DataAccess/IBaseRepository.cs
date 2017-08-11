using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.DataAccess
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：IBaseRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：IBaseRepository
    /// 创建标识：cml 2017/7/6 15:32:21
    /// </summary>
   public interface IBaseRepository<T> where T:class
    {
        long Insert(T info);
    }
}
