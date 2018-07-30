using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace WXSDK.Common
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public class CommonHelper
    {
        HttpHelper _HttpHelper = new HttpHelper();

        #region 获取UTC标准时差毫秒
        /// <summary>
        /// 获取UTC标准时差毫秒
        /// </summary>
        /// <returns>返回标准时间字符串</returns>
        public string GetUTCMillTime(long n = 10000)
        {
            DateTime dt = new DateTime(1970, 1, 1);
            return ((DateTime.UtcNow - dt).Ticks / n).ToString();
        }
        #endregion

        #region 根据图形获取图形的扩展名
        /// <summary>
        /// 根据图形获取图形的扩展名 
        /// </summary>
        /// <param name="p_Image">图形</param>
        /// <returns>扩展名</returns>
        public string GetImageExtension(Image p_Image)
        {
            Type Type = typeof(System.Drawing.Imaging.ImageFormat);
            System.Reflection.PropertyInfo[] _ImageFormatList = Type.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            for (int i = 0; i != _ImageFormatList.Length; i++)
            {
                System.Drawing.Imaging.ImageFormat _FormatClass = (System.Drawing.Imaging.ImageFormat)_ImageFormatList[i].GetValue(null, null);
                if (_FormatClass.Guid.Equals(p_Image.RawFormat.Guid))
                {
                    return _ImageFormatList[i].Name;
                }
            }
            return "";
        }
        #endregion

        #region 通过网络地址获取图片
        /// <summary>
        /// 通过网络地址获取图片
        /// </summary>
        /// <param name="url">图片地址</param>
        /// <returns>图片对象</returns>
        public Image GetImage(string url,CookieContainer _CookieContainer=null)
        {
            Image image = null;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
                HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(url);
                if (_CookieContainer != null)
                {
                    webreq.CookieContainer = _CookieContainer;
                }
                WebResponse webres = webreq.GetResponse();
                Stream stream = webres.GetResponseStream();
                image = System.Drawing.Image.FromStream(stream);
                stream.Close();
                stream.Dispose();
            }
            catch (Exception e)
            {
                return null;
            }
            return image;
        }

        private bool ValidateServerCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        #endregion

        #region 通过GET获取返回的HTML内容
        /// <summary>
        /// 通过GET获取返回的HTML内容
        /// </summary>
        /// <param name="urlBase">基础连接地址</param>
        /// <param name="parms">要传递的参数</param>
        /// <param name="_CookieCollection">cookie</param>
        /// <returns>HTML内容</returns>
        public HttpResult HttpGet(string urlBase, Dictionary<string, string> parms, ref  CookieCollection _CookieCollection)
        {
            ///通过参数列表和基础地址构造请求地址
            var result = new HttpResult();
            StringBuilder builder = new StringBuilder();
            builder.Append(urlBase);
            if (parms != null && parms.Count > 0)
            {
                builder.Append("?");
                int i = 0;
                foreach (var item in parms)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", Uri.EscapeDataString(item.Key), Uri.EscapeDataString(item.Value));
                    i++;
                }
            }
            string strUlr = builder.ToString();
            if (string.IsNullOrEmpty(strUlr))
            {
                return result;
            }
            else
            {
                HttpItem newItem = new HttpItem();
                newItem.URL = strUlr;
                newItem.Method = "GET";
                newItem.ResultType = ResultType.String;
                newItem.Encoding = Encoding.UTF8;
                newItem.UserAgent = "Mozilla/5.0 (X11; Linux i686; U;) Gecko/20070322 Kazehakase/0.4.5";
                newItem.CookieCollection = _CookieCollection;
                result = _HttpHelper.GetHtml(newItem);
                if (result.CookieCollection != null && result.CookieCollection.Count != 0)
                {
                    newItem.CookieCollection.Add(result.CookieCollection);
                    _CookieCollection = newItem.CookieCollection;
                }
                return result;
            }
        }

        /// <summary>
        /// 通过GET获取返回的HTML内容
        /// </summary>
        /// <param name="urlBase">基础连接地址</param>
        /// <param name="parms">要传递的参数</param>
        /// <param name="_CookieCollection">cookie</param>
        /// <returns>HTML内容</returns>
        public HttpResult HttpGet(string urlBase, Dictionary<string, string> parms)
        {
            ///通过参数列表和基础地址构造请求地址
            var result = new HttpResult();
            StringBuilder builder = new StringBuilder();
            builder.Append(urlBase);
            if (parms != null && parms.Count > 0)
            {
                builder.Append("?");
                int i = 0;
                foreach (var item in parms)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", Uri.EscapeDataString(item.Key), Uri.EscapeDataString(item.Value));
                    i++;
                }
            }
            string strUlr = builder.ToString();
            if (string.IsNullOrEmpty(strUlr))
            {
                return result;
            }
            else
            {
                HttpItem newItem = new HttpItem();
                newItem.URL = strUlr;
                newItem.Method = "GET";
                newItem.ResultType = ResultType.String;
                newItem.Encoding = Encoding.UTF8;
                newItem.UserAgent = "Mozilla/5.0 (X11; Linux i686; U;) Gecko/20070322 Kazehakase/0.4.5";
                newItem.CookieCollection = null;
                result = _HttpHelper.GetHtml(newItem);
                return result;
            }
        }
        #endregion

        #region 通过POST获取返回的html内容
        /// <summary>
        /// 通过POST获取返回的html内容
        /// </summary>
        /// <param name="urlBase">基础连接地址</param>
        /// <param name="parms">要传递的参数</param>
        /// <param name="_CookieCollection">cookie</param>
        /// <returns>HTML内容</returns>
        public HttpResult HttpPost(string urlBase, Dictionary<string, object> parms, ref CookieCollection _CookieCollection)
        {
            var result = new HttpResult();
            if (string.IsNullOrEmpty(urlBase))
            {
                return result;
            }
            else
            {
                HttpItem newItem = new HttpItem();
                newItem.URL = urlBase;
                newItem.Method = "post";
                newItem.Postdata = Newtonsoft.Json.JsonConvert.SerializeObject(parms).ToStr();
                newItem.ResultType = ResultType.String;
                newItem.Encoding = Encoding.UTF8;
                newItem.CookieCollection = _CookieCollection;
                newItem.PostEncoding = Encoding.UTF8;
                result = _HttpHelper.GetHtml(newItem);
                if (result != null && result.CookieCollection != null && result.CookieCollection.Count != 0)
                {
                    newItem.CookieCollection.Add(result.CookieCollection);
                    _CookieCollection = newItem.CookieCollection;
                }
                return result;
            }
        }

        /// <summary>
        /// 通过POST获取返回的html内容
        /// </summary>
        /// <param name="urlBase">基础连接地址</param>
        /// <param name="parms">要传递的参数</param>
        /// <returns>HTML内容</returns>
        public HttpResult HttpPost(string urlBase, Dictionary<string, object> parms,string type =null)
        {
            var result = new HttpResult();
            if (string.IsNullOrEmpty(urlBase))
            {
                return result;
            }
            else
            {
                HttpItem newItem = new HttpItem();
                newItem.URL = urlBase;
                newItem.Method = "post";
                newItem.Postdata = Newtonsoft.Json.JsonConvert.SerializeObject(parms).ToStr();
                newItem.ResultType = ResultType.String;
                newItem.Encoding = Encoding.UTF8;
                if (type != null)
                {
                    newItem.ContentType = type;
                }
                newItem.UserAgent = "Mozilla/5.0 (X11; Linux i686; U;) Gecko/20070322 Kazehakase/0.4.5";
                newItem.CookieCollection = null;
                result = _HttpHelper.GetHtml(newItem);
                return result;
            }
        }
        #endregion

        #region 多媒体获取
        /// <summary>
        /// 根据类别保存连接对象内容到本地
        /// </summary>
        /// <param name="urlBase">请求连接</param>
        /// <param name="msgType">类型</param>
        /// <returns>保存地址</returns>
        public HttpResult GetFile(string url, CookieCollection _CookieCollection = null)
        {
            var result = new HttpResult();
            if (string.IsNullOrEmpty(url))
            {
                return result;
            }
            else
            {
                HttpItem newItem = new HttpItem();
                newItem.Accept = "*/*";
                newItem.URL = url;
                newItem.Method = "GET";
                newItem.ContentType = "video/mp4";
                newItem.ResultType = ResultType.Byte;
                newItem.CookieCollection = _CookieCollection;
                result = _HttpHelper.GetHtml(newItem);
                return result;
            }
        }
        #endregion

        #region 附件上传
        public string PostMutipart(string url, List<FormDataItem> dataList)
        {
            string result = string.Empty;
            var boundary = "----WebKitFormBoundary" + Guid.NewGuid().ToString("N").Substring(0, 16);
            Stream stream = Retry<Stream>(() =>
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.Accept = "*/*";
                request = PretendWechat(request);
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                var sw = new StreamWriter(request.GetRequestStream());
                foreach (var item in dataList)
                {
                    if (item.isFile)
                    {
                        var str = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; {2}=\"{3}\"\r\nContent-Type: {4}\r\n\r\n", boundary, item.Name, item.Name, item.FileName, MimeMapping.GetMimeMapping(item.FileName));
                        sw.Write(str);
                        sw.Flush();
                        sw.BaseStream.Write(item.Content, 0, item.ContentLength);
                        sw.Write("\r\n");
                    }
                    else
                    {
                        sw.Write(string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n", boundary, item.Name, item.Value));
                    }
                    sw.Flush();
                }
                sw.Write("--" + boundary + "--\r\n");
                sw.Flush();
                sw.Close();
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                return response.GetResponseStream();
            });
            using (stream)
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
            }
            return result;
        }

        public HttpWebRequest PretendWechat(HttpWebRequest request)
        {
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            request.Headers.Add("Accept-Encoding", "gzip,deflate,br");
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6,zh-TW;q=0.4");
            request.AllowAutoRedirect = false;
            request.ServicePoint.Expect100Continue = false;
            request.Timeout = 35000;
            request.Referer = "https://wx2.qq.com";
            request.Headers.Add("Origin", "https://wx2.qq.com");
            return request;
        }

        public string GetFileMD5Hash(FileInfo fileInfo)
        {
            try
            {
                FileStream file = fileInfo.OpenRead();
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5Hash() fail,error:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取上传文件时的MediaType
        /// </summary>
        /// <param name="ext">文件扩展名</param>
        /// <returns></returns>
        public string GetMediaType(string ext)
        {
            //貌似除了图片视频，其他都是doc
            switch (ext)
            {
                case ".jpg":
                    return "pic";
                case ".jpeg":
                    return "pic";
                case ".png":
                    return "pic";
                case ".mp4":
                    return "video";
                default:
                    return "doc";
            }
        }

        /// <summary>
        /// 三次重试机制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        private T Retry<T>(Func<T> func)
        {
            int err = 0;
            while (err < 3)
            {
                try
                {
                    return func();
                }
                catch (WebException webExp)
                {
                    err++;
                    Thread.Sleep(5000);
                    if (err > 2)
                    {
                        throw webExp;
                    }
                }
            }
            return func();
        }
        #endregion
    }
}
