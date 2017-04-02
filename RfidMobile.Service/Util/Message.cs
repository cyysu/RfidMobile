using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RfidMobile.Service.Util
{
   public class Message
    {
       public string Sender { get; set; }
       public string Content { get; set; }

       public override string ToString()
       {
           return Sender+":"+Content;
       }
    }
}
