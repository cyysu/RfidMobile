using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace RfidMobile.Service.Reader
{
    /// <summary>
    /// 用来告知程序product是否已经读到的视图模型
    /// </summary>
    public class ReaderProductViewModel
    {
        /// <summary>
        /// 是否已经读到
        /// </summary>
        public bool IsRead { get; set; }
        public string IsReadChinese { get {return  IsRead? "是": "否";} }

        /// <summary>
        /// 产品序号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 标签Id
        /// </summary>
        public string TagId { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 将product列表装成reader控件用的，显示用的product列表
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public static IList<ReaderProductViewModel> GetByProducts(IList<RfidMobile.Service.Product.Product> products)
        {
            IList<ReaderProductViewModel> newProducts = new List<ReaderProductViewModel>();
            if (products != null)
            {
                foreach (RfidMobile.Service.Product.Product product in products)
                {
                    ReaderProductViewModel newProduct = new ReaderProductViewModel()
                    {
                        IsRead = false,
                        SerialNumber = product.SerialNumber,
                        TagId = product.TagId,
                        Category = product.Category.Name
                    };
                    newProducts.Add(newProduct);
                }
            }
            return newProducts;
        }
    }
}
