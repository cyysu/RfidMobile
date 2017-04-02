using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RfidMobile.Service.StockIn
{
   public class MessageReceivedEventArgs: EventArgs
    {
       public string Message { get; set; }
    }
}
