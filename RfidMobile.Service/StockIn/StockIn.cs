using System;
using System.Collections.Generic;
using System.Text;

namespace RfidMobile.Service.StockIn
{
   public class StockIn
    {
       public string SerialNumber;
       public IList<StockInDetail> Details;
    }
}
