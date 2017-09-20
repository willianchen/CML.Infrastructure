using CML.Infrastructure.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Utils
{
    /// <summary>
    /// Copyright (C) cml 版权所有。
    /// 类名：HttpClientUtil.cs
    /// 类功能描述：HttpClientUtil
    /// 创建标识：cml 2017/9/19 15:20:25
    /// </summary>
    public class HttpClientUtil
    {
        private static readonly object _lockObj = new object();

        private static HttpClient _client;

        /// <summary>
        /// 初始化
        /// </summary>
        public static HttpClient HttpClient
        {
            get
            {
                if (_client == null)
                {
                    lock (_lockObj)
                    {
                        if (_client == null)
                        {
                            _client = new HttpClient();
                        }
                    }
                }

                return _client;
            }
        }

        /// <summary>
        /// Json 格式发送请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static async Task<string> PostRequestASJsonAsync<T>(string url, T info) where T : class
        {
            LogUtil.Debug(string.Format("发起请求 Url:{0} 参数：{1}", url, info.ToJson()));
            var json = info.ToJson();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(url, content);
            var resJson = await response.Content.ReadAsStringAsync();
            LogUtil.Debug(string.Format("结束请求 Url:{0} 参数：{1}", url, resJson));
            return resJson;
        }

        /// <summary>
        /// Form格式发送请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static async Task<string> PostRequestAsync<T>(string url, T info)
        {
            LogUtil.Debug(string.Format("发起请求 Url:{0} 参数：{1}", url, info.ToJson()));
            var paraList = info.ToDictionary().Select(m => new KeyValuePair<string, string>(m.Key, m.Value.ToString()));
            var content = new FormUrlEncodedContent(paraList);
            var response = await HttpClient.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();
            LogUtil.Debug(string.Format("结束请求 Url:{0} 参数：{1}", url, result));
            return result;
        }
    }
}
