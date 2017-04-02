using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RfidMobile.Service.Util
{
  public  class Log
    {
      public LogType Type { get; set; }
      public DateTime DateTime { get; set; }
      public Message Message { get; set; }
    }
}
