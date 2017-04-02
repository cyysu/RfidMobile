using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using RfidMobile.Service.Reader;

namespace RfidMobile.Service.Util
{
    public class Config
    {
        public string Version { get { return "1.0.0"; } }
        public string Host { get; set; }
        public int Port { get; set; }
        public string LogLevel { get; set; }
    }
}
