using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace RfidMobile.Service.Util
{
    /// <summary>
    /// http服务
    /// </summary>
    public class HttpService
    {
        public static readonly string TAG = "http";

        /// <summary>
        /// 根据url，进行网络通讯，获取json格式返回数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetJsonByUrl(string url) 
        {
            LogService.Info(TAG, "request:" + url );

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            string json = "";
            using (StreamReader sr = new StreamReader(responseStream, Encoding.UTF8))
            {
                json = sr.ReadToEnd();
            }

            LogService.Info(TAG, "resopnse:" + json);

            return json;
        }
    }
}
