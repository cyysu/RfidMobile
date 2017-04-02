using System;
using System.Collections.Generic;
using System.Text;
using RfidMobile.Service.Product;

namespace RfidMobile.Service.StockIn
{
    public class StockInDetail
    {
        public Category Category;
        public IList<RfidMobile.Service.Product.Product> Products;
    }
}
