using CML.Infrastructure.Extension;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Utils
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：HttpUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：HttpUtil
    /// 创建标识：cml 2017/7/4 11:31:24
    /// </summary>
    public static class HttpUtil
    {
        /// <summary>
        ///  请求Url string 返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string RequestStr(string url, NameValueCollection parameters, string[] files = null)
        {
            var contentType = "application/x-www-form-urlencoded";
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");

            if (files.IsNotNull())
                contentType = "multipart/form-data; boundary=" + boundary;

            HttpWebRequest request = CreateHttpWebRequest(url, "POST", contentType);


            string paramFormat = boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";

            var sb = new StringBuilder();
            foreach (var key in parameters.AllKeys)
                sb.AppendFormat(paramFormat, key, parameters[key]);
            //   sb.Append(key + "=" + parameters[key] + "&");
            sb.Length = sb.Length - 1;
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());

            //           byte[] bytes = GetRequestBytes(parameters);
            //写数据
            // request.ContentLength = bytes.Length;
            Stream reqstream = request.GetRequestStream();
            reqstream.Write(bytes, 0, bytes.Length);
            if (files.IsNotNull() && files.Length > 0)
            {

                byte[] boundarybytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
                byte[] endbytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

                //1.2 file
                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                byte[] buffer = new byte[4096];
                int bytesRead = 0;
                for (int i = 0; i < files.Length; i++)
                {
                    reqstream.Write(boundarybytes, 0, boundarybytes.Length);
                    string header = string.Format(headerTemplate, "file" + i, Path.GetFileName(files[i]));
                    byte[] headerbytes = Encoding.UTF8.GetBytes(header);
                    reqstream.Write(headerbytes, 0, headerbytes.Length);
                    using (FileStream fileStream = new FileStream(files[i], FileMode.Open, FileAccess.Read))
                    {
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            reqstream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
                reqstream.Write(endbytes, 0, endbytes.Length);
            }

            // request.ContentLength = reqstream.Length;
            HttpWebResponse response = null;
            string strResult = string.Empty;
            try
            {
                //请求数据
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    strResult = GetResponseText(response.GetResponseStream());
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (response = (HttpWebResponse)ex.Response)
                    {
                        strResult = GetResponseText(response.GetResponseStream());
                    }
                }
                else
                {
                    strResult = ex.Message;
                }
            }

            //关闭流
            reqstream.Close();
            request.Abort();
            response.Close();
            return (strResult);
        }


        public static string RequestTest(string url, NameValueCollection param, string[] files = null)
        {
            //边界

            string boundary = DateTime.Now.Ticks.ToString("x");

            HttpWebRequest uploadRequest = (HttpWebRequest)WebRequest.Create(url);//url为上传的地址
            uploadRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            uploadRequest.Method = "POST";
            uploadRequest.Accept = "*/*";
            uploadRequest.KeepAlive = true;
            uploadRequest.Headers.Add("Accept-Language", "zh-cn");
            uploadRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            uploadRequest.Credentials = CredentialCache.DefaultCredentials;
            uploadRequest.Headers["Cookie"] = "";//上传文件时需要的cookies

            WebResponse reponse;
            //创建一个内存流
            Stream memStream = new MemoryStream();
            //确定上传的文件路径
            if (files.IsNotNull() && files.Length > 0)
            {
                var file = files[0];
                boundary = "--" + boundary;

                //添加上传文件参数格式边界

                string paramFormat = boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}\r\n";

                //NameValueCollection param = new NameValueCollection();

                param.Add("attachments", Guid.NewGuid().ToString() + Path.GetExtension(file));

                //写上参数
                foreach (string key in param.Keys)
                {
                    string formitem = string.Format(paramFormat, key, param[key]);

                    byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);

                    memStream.Write(formitembytes, 0, formitembytes.Length);

                }



                //添加上传文件数据格式边界

                string dataFormat = boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\nContent-Type:application/octet-stream\r\n\r\n";

                string header = string.Format(dataFormat, "Filedata", Path.GetFileName(file));

                byte[] headerbytes = Encoding.UTF8.GetBytes(header);

                memStream.Write(headerbytes, 0, headerbytes.Length);



                //获取文件内容

                FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);

                byte[] buffer = new byte[1024];

                int bytesRead = 0;



                //将文件内容写进内存流

                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)

                {

                    memStream.Write(buffer, 0, bytesRead);

                }

                fileStream.Close();



                //添加文件结束边界

                byte[] boundarybytes = Encoding.UTF8.GetBytes("\r\n\n" + boundary + "\r\nContent-Disposition: form-data; name=\"Upload\"\r\n\nSubmit Query\r\n" + boundary + "--");

                memStream.Write(boundarybytes, 0, boundarybytes.Length);



                //设置请求长度

                uploadRequest.ContentLength = memStream.Length;

                //获取请求写入流

                Stream requestStream = uploadRequest.GetRequestStream();





                //将内存流数据读取位置归零

                memStream.Position = 0;

                byte[] tempBuffer = new byte[memStream.Length];

                memStream.Read(tempBuffer, 0, tempBuffer.Length);

                memStream.Close();



                //将内存流中的buffer写入到请求写入流

                requestStream.Write(tempBuffer, 0, tempBuffer.Length);

                requestStream.Close();

            }



            //获取到上传请求的响应

            //     reponse = uploadRequest.GetResponse();
            string strResult = string.Empty;
            try
            {
                //请求数据
                using (reponse = (HttpWebResponse)uploadRequest.GetResponse())
                {
                    strResult = GetResponseText(reponse.GetResponseStream());
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (reponse = (HttpWebResponse)ex.Response)
                    {
                        strResult = GetResponseText(reponse.GetResponseStream());
                    }
                }
                else
                {
                    strResult = ex.Message;
                }
            }
            return strResult;
        }
        /// <summary>
        /// 创建HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="contentType"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static HttpWebRequest CreateHttpWebRequest(string url, string httpMethod = "POST", string contentType = "application/x-www-form-urlencoded", int timeout = 300000)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = httpMethod;
            //读数据
            httpWebRequest.Timeout = timeout;
            httpWebRequest.Headers.Set("Pragma", "no-cache");
            return httpWebRequest;
        }

        /// <summary>
        /// 获取参数内容
        /// </summary>
        /// <param name="postParameters"></param>
        /// <returns></returns>
        public static byte[] GetRequestBytes(NameValueCollection postParameters)
        {
            if (postParameters == null || postParameters.Count == 0)
                return new byte[0];
            var sb = new StringBuilder();
            foreach (var key in postParameters.AllKeys)
                sb.Append(key + "=" + postParameters[key] + "&");
            sb.Length = sb.Length - 1;
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        /// <summary>
        /// 获取响应值
        /// </summary>
        /// <param name="responseStream"></param>
        /// <returns></returns>
        public static string GetResponseText(Stream responseStream)
        {
            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }

        public static T DeSerializeToJson<T>(Stream stream)
        {
            using (stream)
            {
                throw new Exception("方法暂未实现");
            }
        }

    }
}
