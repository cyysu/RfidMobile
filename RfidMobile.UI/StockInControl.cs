using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using RfidMobile.Service.StockIn;
using RfidMobile.Service.Product;


namespace RfidMobile.UI
{
    /// <summary>
    /// ���ؼ�
    /// </summary>
    public partial class StockInControl : UserControl
    {
        private StockIn stockIn;

        public event EventHandler<ReadTagsClickedEventArgs> ReadTagsClicked;

        public StockInControl()
        {
            InitializeComponent();
            txtSerialNumber.Focus();
        }

        protected void OnReadTagsClicked(ReadTagsClickedEventArgs e) {
            if (ReadTagsClicked != null) {
                ReadTagsClicked(this, e);
            }
        }

        // ����serial nuber��ȡ�����Ϣ
        private void btnReadStockIn_Click(object sender, EventArgs e)
        {
            string serialNumber = txtSerialNumber.Text;
            stockIn = StockInService.GetBySerialNumber(serialNumber);

            IList<Product> products = ProductService.GetProductsBuStockIn(stockIn);

            dgProducts.DataSource = products;
        }

        // ��reader�ؼ�
        private void btnReadTag_Click(object sender, EventArgs e)
        {
            if (stockIn != null)
            {
                OnReadTagsClicked(new ReadTagsClickedEventArgs() { StockIn = stockIn });
            }
        }
    }
}
