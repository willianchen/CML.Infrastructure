using System;
using System.Collections;
using System.Collections.Generic;

namespace CML.Infrastructure.Result
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：PageResult.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：PageResult
    /// 创建标识：cml 2017/8/20 11:53:56
    /// </summary>
    public class PageResult<T> : IPageResult<T> 
    {
        private int _pageCount;
        private int _pageIndex;
        private int _pageSize;
        private int _totalCount;
        private IEnumerable<T> _data;

        public PageResult(int page, int pageSize, int totalCount, IEnumerable<T> data)
        {
            _pageIndex = page <= 0 ? 1 : page;
            _pageSize = pageSize <= 0 ? 1 : pageSize;
            _totalCount = totalCount;
            double t = _totalCount / _pageSize;
            _pageCount = (int)Math.Ceiling(t);
            _data = data;
        }

        public int PageCount
        {
            get
            {
                return _pageCount;
            }
        }

        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
        }

        public int TotalCount
        {
            get
            {
                return _totalCount;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in Data)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 分页内容
        /// </summary>
        public IEnumerable<T> Data
        {
            get
            {
                return _data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
