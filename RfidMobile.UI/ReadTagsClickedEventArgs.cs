using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using RfidMobile.Service.StockIn;

namespace RfidMobile.UI
{
  public  class ReadTagsClickedEventArgs : EventArgs
    {
      public StockIn StockIn { get; set; }
    }
}
