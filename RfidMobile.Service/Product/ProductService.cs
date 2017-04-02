using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using RfidMobile.Service.StockIn;

namespace RfidMobile.Service.Product
{
    /// <summary>
    /// 产品服务
    /// </summary>
    public class ProductService
    {
        /// <summary>
        /// 由入库信息获取产品列表
        /// </summary>
        /// <param name="stockIn"></param>
        /// <returns></returns>
        public static IList<Product> GetProductsBuStockIn(RfidMobile.Service.StockIn.StockIn stockIn)
        {
            IList<Product> products = new List<Product>();
            if (stockIn != null && stockIn.Details != null)
            {
                foreach (StockInDetail detail in stockIn.Details)
                {
                    foreach (Product product in detail.Products)
                    {
                        products.Add(product);
                    }
                }
            }
            return products;
        }
    }
}
