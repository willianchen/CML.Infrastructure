using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Common
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：PageList.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：分页公共类
    /// 创建标识：cml 2017/6/1 14:58:05
    /// </summary>
    public class PageList<T> where T : class
    {
        private readonly long _totalCnt;
        private readonly int _pageSize;
        private readonly int _pageIndex;
        private readonly List<T> _items;
        private readonly long _totalPage;
        public PageList(int pageIndex, int pageSize, long totalCnt, List<T> items)
        {
            _pageIndex = pageIndex;
            _pageSize = pageSize;
            _totalCnt = totalCnt;
            _items = items;
            _totalPage = totalCnt % pageSize == 0 ? _totalCnt / _pageSize : _totalCnt / _pageSize + 1;
        }

        public long TotalCnt
        {
            get
            {
                return _totalCnt;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
        }

        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
        }

        public List<T> Items
        {
            get
            {
                return _items;
            }
        }

        public long TotalPage
        {
            get
            {
                return _totalPage;
            }
        }

    }
}
