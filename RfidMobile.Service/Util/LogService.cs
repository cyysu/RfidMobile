using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace RfidMobile.Service.Util
{
    /// <summary>
    /// 自己实现的简单日志服务，不知道win mobile有没有可以直接使用的日志库
    /// </summary>
    /// <remarks>
    /// 2017-3-22 wzl
    /// 1.0.0 创建 
    /// </remarks>
    public class LogService
    {
        private static string FileName{get{return "log"+ DateTime.Now.ToString("yyyy-MM-dd") + ".txt";}}

        public static LogType Level = LogType.Info;

        public static void Info(Message message)
        {
            Write(LogType.Info, message);
        }
        public static void Info(string sender, string content)
        {
            Info(new Message() { Sender = sender, Content = content });
        }

        public static void Error(Message message)
        {
            Write(LogType.Error, message);
        }
        public static void Error(string sender, string content)
        {
            Error(new Message() { Sender = sender, Content = content });
        }

        public static void Debug(Message message)
        {
            Write(LogType.Debug, message);
        }
        public static void Debug(string sender, string content)
        {
            Debug(new Message() { Sender = sender, Content = content });
        }

        public static void Warn(Message message)
        {
            Write(LogType.Warn, message);
        }

        public static void Warn(string sender, string content) {
            Warn(new Message() { Sender = sender, Content = content });
        }

        public static void Write(LogType type, Message message)
        {
            using (FileStream fs = new FileStream(FileName, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    Log log = new Log() { Type = LogType.Info, DateTime = DateTime.Now, Message = message };
                    // 正序插入
                    sw.Write(JsonConvert.SerializeObject(log) + "\r\n");
                }
            }
        }

        public static IList<Log> Read(DateTime date) {
            string fileName = "log" + date.ToString("yyyy-MM-dd") + ".txt";
            return Read(fileName);
        }

        public static IList<Log> Read(string fileName) {
            IList<Log> logs = new List<Log>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    string text = "";
                    while((text =sr.ReadLine()) != null){
                        Log log = JsonConvert.DeserializeObject<Log>(text);
                        if (log.Type >= Level)
                        {
                            // 逆序读取
                            logs.Insert(0, log);
                        }
                    }
                }
            }
            // 文件读取失败，则程序返回长度为0的logs，故不处理异常
            catch (Exception ex) { 
            
            }
            return logs;
        }

        public static IList<Log> Read()
        {
            return Read(FileName);
        }
    }
}
