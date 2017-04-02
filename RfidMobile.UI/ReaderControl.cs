using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID3;
using System.Linq;
using System.Net;
using System.Threading;
using RfidMobile.Service.Reader;
using RfidMobile.Service.Util;
using RfidMobile.Service.Product;

namespace RfidMobile.UI
{
    public partial class ReaderControl : UserControl
    {
        private static ReaderService readerService;
        private IList<ReaderProductViewModel> products;

        public ReaderControl(RfidMobile.Service.Util.Config config, IList<Product> products)
        {
            InitializeComponent();

            if (products != null)
            {
                // 重新配置data gird的样式
                DataGridTableStyle tableStyle = new DataGridTableStyle();
                tableStyle.MappingName = products.GetType().Name;

                DataGridTextBoxColumn isReadColumnStyle = new DataGridTextBoxColumn();
                isReadColumnStyle.Width = 50;
                isReadColumnStyle.MappingName = "IsReadChinese";
                isReadColumnStyle.HeaderText = "读取";
                tableStyle.GridColumnStyles.Add(isReadColumnStyle);

                DataGridTextBoxColumn tagIdColumnStyle = new DataGridTextBoxColumn();
                tagIdColumnStyle.Width = 120;
                tagIdColumnStyle.MappingName = "TagId";
                tagIdColumnStyle.HeaderText = "标签";
                tableStyle.GridColumnStyles.Add(tagIdColumnStyle);

                DataGridTextBoxColumn categoryColumnStyle = new DataGridTextBoxColumn();
                categoryColumnStyle.Width = 50;
                categoryColumnStyle.MappingName = "Category";
                categoryColumnStyle.HeaderText = "类型";
                tableStyle.GridColumnStyles.Add(categoryColumnStyle);

                DataGridTextBoxColumn serialNumberColumnStyle = new DataGridTextBoxColumn();
                serialNumberColumnStyle.Width = 80;
                serialNumberColumnStyle.MappingName = "SerialNumber";
                serialNumberColumnStyle.HeaderText = "序号";
                tableStyle.GridColumnStyles.Add(serialNumberColumnStyle);

                dgProducts.TableStyles.Clear();
                dgProducts.TableStyles.Add(tableStyle);

                dgProducts.SelectionBackColor = Color.Green;

                SetProducts(products);
            }

            // 初始化reader服务

            readerService = new ReaderService(config.Host, config.Port);
           
            readerService.IsConnectChanged += new EventHandler<IsConnectChangedEventArgs>(readerService_IsConnectChanged);
            readerService.TagDataReceived += new EventHandler<TagDataReceivedEventArgs>(readerService_TagDataReceived);
            readerService.MessageReceived += new EventHandler<MessageReceivedEventArgs>(readerService_MessageReceived);

            chkIsConnect.Checked = readerService.IsConnect;
            chkIsConnect.Text = readerService.IsConnect ? "开" : "关";
        }

        public void Start() {
            readerService.Start();
        }

        public void Stop() {
            readerService.Stop();
        }

        private void UpdateDgProducts(IList<ReaderProductViewModel> products)
        {
            dgProducts.DataSource = products;

            dgProducts.DataSource = this.products;

            // 让已经读到的标签，处于选中状态，所以会显示绿色
            for (int i = 0; i < this.products.Count; i++)
            {
                ReaderProductViewModel product = this.products[i];
                if (product.IsRead == true)
                {
                    dgProducts.Select(i);
                }
                else
                {
                    dgProducts.UnSelect(i);
                }
            }
        }

        private void SetProducts(IList<Product> products)
        {
            this.products = ReaderProductViewModel.GetProducts(products);
            UpdateDgProducts(this.products);
        }

        private void readerService_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            this.Invoke((EventHandler)delegate
            {
                MessageBox.Show(e.Message);
            });
        }

        private void readerService_TagDataReceived(object sender, TagDataReceivedEventArgs e)
        {
            this.Invoke((EventHandler)delegate
            {
                string text = "";

                foreach (TagData item in e.TagData)
                {
                    text += item.TagID + "\r\n";
                    if (products != null)
                    {
                        // tag已经读取到，配置isRead属性为true
                        IList<ReaderProductViewModel> readProducts = products.Where(p => p.TagId == item.TagID).ToList();
                        if (readProducts != null && readProducts.Count != 0)
                        {
                            ReaderProductViewModel product = readProducts.First();
                            if (product != null)
                            {
                                product.IsRead = true;
                            }
                        }
                    }
                }
                // 让没有读取到的tag，显示在前面
                if (products != null)
                {
                    products = products.OrderBy(p => p.IsRead).ToList();
                    UpdateDgProducts(products);
                }


                txtMessage.Text = text;
                labTotalRead.Text = "读" + products.Where(p => p.IsRead).Count() + "共" + products.Count();
            });
        }

        private void readerService_IsConnectChanged(object sender, IsConnectChangedEventArgs e)
        {
            this.Invoke((EventHandler)delegate
             {
                 chkIsConnect.Checked = e.IsConnect;
                 chkIsConnect.Text = e.IsConnect ? "开" : "关";

             });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            readerService.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            readerService.Stop();
        }

    }
}
