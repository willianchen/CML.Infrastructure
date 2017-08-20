using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Result
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：IPageResult.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：分页结果类
    /// 创建标识：cml 2017/8/20 11:43:33
    /// </summary>
    public interface IPageResult
    {
        /// <summary>
        /// 总数量
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// 总页数
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// 当前页面
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// 页长
        /// </summary>
        int PageSize { get; }
    }

    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public interface IPageResult<T> : IPageResult, IEnumerable<T>
    {
    }
}
