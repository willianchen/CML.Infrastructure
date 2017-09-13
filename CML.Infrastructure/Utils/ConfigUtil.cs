using CML.Infrastructure.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CML.Infrastructure.Utils
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：ConfigUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：配置文件读取帮助类
    /// 创建标识：cml 2017/5/31 14:00:47
    /// </summary>
    public static class ConfigUtil
    {

        #region 根据Key获取app配置文件的设置的值

        /// <summary>
        /// 根据Key获取app配置文件的设置的值
        /// </summary>
        /// <param name="key">获取的Key</param>
        /// <returns>app配置文件的设置的值</returns>
        public static string GetValue(string key)
        {
            XmlDocument xmlDoc = LoadAppXml();
            var xmlNode = xmlDoc.SelectSingleNode("//appSettings");
            if (xmlNode != null)
            {
                XmlElement xmlElement = xmlNode.SelectSingleNode("//add[@key='" + key + "']") as XmlElement;
                if (xmlElement != null)
                {
                    return xmlElement.GetAttribute("value");
                }
            }
            return null;
        }

        #endregion 根据Key获取app配置文件的设置的值

        #region 加载App配置文件

        /// <summary>
        /// 加载App配置文件
        /// </summary>
        /// <returns>App配置文件</returns>
        private static XmlDocument LoadAppXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(GetAppConfigPath());
            return xmlDoc;
        }

        #endregion 加载App配置文件

        #region 获取AppConfig的配置路径

        /// <summary>
        /// 获取AppConfig的配置路径
        /// </summary>
        /// <returns></returns>
        public static string GetAppConfigPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SystemConstant.AppConfig);
        }

        #endregion 获取AppConfig的配置路径
    }
}
