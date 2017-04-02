using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace RfidMobile.Service.Util
{
    /// <summary>
    /// 配置信息服务
    /// </summary>
    public class ConfigService
    {
        private static readonly string fileName = "config.txt";

        public static Config Read()
        {
            Config config = new Config() { Host = "127.0.0.1", Port = 5084, LogLevel= "debug"};
            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    string json = sr.ReadToEnd();
                    config = JsonConvert.DeserializeObject<Config>(json);
                }
            }
            // 读不到说明没有配置文件，程序会返回默认值的config，所以不处理异常
            catch (Exception ex)
            { 
            }
            return config;
        }

        public static void Save(Config config)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.Write(JsonConvert.SerializeObject(config) + "\r\n");
                }
            }
        }
    }
}
