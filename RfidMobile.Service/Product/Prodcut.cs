using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RfidMobile.Service.Product
{
    /// <summary>
    /// 要扫描的产品
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        /// <summary>
        /// rfid标签的id
        /// </summary>
        public string TagId { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public Category Category { get; set; }
    }
}
