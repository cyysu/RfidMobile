using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RfidMobile.Service.Product
{
    /// <summary>
    /// 产品的种类
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}