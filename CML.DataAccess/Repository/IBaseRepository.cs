using CML.Infrastructure.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.DataAccess
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：IBaseRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：IBaseRepository
    /// 创建标识：cml 2017/7/6 15:32:21
    /// </summary>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="info">实例</param>
        /// <param name="ignoreFields">忽略字段名称</param>
        /// <param name="isIdentity">是否返回自增字段</param>
        /// <returns></returns>
        object Insert(T info, string[] ignoreFields = null, bool isIdentity = false);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="condition">删除条件</param>
        /// <returns>受影响的行数</returns>
        int Delete(object condition);

        /// <summary>
        /// 获取传输对象
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        TDto GetDto<TDto>(object condition, string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <param name="condition">获取条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        T GetInfo(object condition, string[] ignoreFields = null, bool isWrite = false);


        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>范总数量</returns>
        int QueryCount(object condition, bool isWrite = false);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        IEnumerable<T> QueryList(bool isWrite = false);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        IEnumerable<T> QueryList(object condition, int top = 0, bool isWrite = false);


        /// <summary>
        /// 查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        IEnumerable<TDto> QueryList<TDto>(string[] ignoreFields = null, string orderBy = "", int top = 0, bool isWrite = false);


        /// <summary>
        /// 查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        IEnumerable<TDto> QueryList<TDto>(object condition, string[] ignoreFields = null, string orderBy = "", int top = 0, bool isWrite = false);


        /// <summary>
        /// 分页查询（目前只支持MSSQLServer）
        /// </summary>
        /// <typeparam name="TModel">分页结果类型</typeparam>
        /// <param name="selectColumn">查询的列</param>
        /// <param name="selectTable">查询的表</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pageSize">每页个数</param>
        /// <param name="cmdParms">参数值</param>
        /// <returns>分页结果</returns>
        IPageResult<TModel> QueryPageList<TModel>(string selectColumn, string selectTable, string where, string order, int pageIndex, int pageSize, object cmdParms = null);

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="data">要更新的信息</param>
        /// <param name="condition">条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <returns>受影响行数</returns>
        int Update(object data, object condition, string[] ignoreFields = null);
    }
}
