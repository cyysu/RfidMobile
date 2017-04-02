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
        private static IRfidReaderService rfidReaderService;
        private IList<ReaderProductViewModel> products;

        public ReaderControl(RfidMobile.Service.Util.Config config, IList<ReaderProductViewModel> products)
        {
            InitializeComponent();

            if (products != null)
            {
                DataGridTableStyle tableStyle = new DataGridTableStyle();
                tableStyle.MappingName = products.GetType().Name;

                DataGridTextBoxColumn isReadColumnStyle = new DataGridTextBoxColumn();
                isReadColumnStyle.Width = 50;
                isReadColumnStyle.MappingName = "IsReadChinese";
                isReadColumnStyle.HeaderText = "��ȡ";
                tableStyle.GridColumnStyles.Add(isReadColumnStyle);

                DataGridTextBoxColumn tagIdColumnStyle = new DataGridTextBoxColumn();
                tagIdColumnStyle.Width = 120;
                tagIdColumnStyle.MappingName = "TagId";
                tagIdColumnStyle.HeaderText = "��ǩ";
                tableStyle.GridColumnStyles.Add(tagIdColumnStyle);

                DataGridTextBoxColumn categoryColumnStyle = new DataGridTextBoxColumn();
                categoryColumnStyle.Width = 50;
                categoryColumnStyle.MappingName = "Category";
                categoryColumnStyle.HeaderText = "����";
                tableStyle.GridColumnStyles.Add(categoryColumnStyle);

                DataGridTextBoxColumn serialNumberColumnStyle = new DataGridTextBoxColumn();
                serialNumberColumnStyle.Width = 80;
                serialNumberColumnStyle.MappingName = "SerialNumber";
                serialNumberColumnStyle.HeaderText = "���";
                tableStyle.GridColumnStyles.Add(serialNumberColumnStyle);

                // ��������data gird����ʽ
                dgProducts.TableStyles.Clear();
                dgProducts.TableStyles.Add(tableStyle);

                dgProducts.SelectionBackColor = Color.Green;

                SetProducts(products);
            }

            // ��ʼ��reader����
            rfidReaderService = new RfidReaderService(config.Host, config.Port);
            rfidReaderService.IsConnectChanged += new EventHandler<IsConnectChangedEventArgs>(readerService_IsConnectChanged);
            rfidReaderService.TagDataReceived += new EventHandler<TagDataReceivedEventArgs>(readerService_TagDataReceived);
            rfidReaderService.MessageReceived += new EventHandler<MessageReceivedEventArgs>(readerService_MessageReceived);

            chkIsConnect.Checked = rfidReaderService.IsConnect;
            chkIsConnect.Text = rfidReaderService.IsConnect ? "��" : "��";
        }

        public void Start() {
            rfidReaderService.Start();
        }

        public void Stop() {
            rfidReaderService.Stop();
        }

        private void SetProducts(IList<ReaderProductViewModel> products)
        {
            dgProducts.DataSource = products;

            // ���Ѿ������ı�ǩ������ѡ��״̬�����Ի���ʾ��ɫ
            for (int i = 0; i < products.Count; i++)
            {
                ReaderProductViewModel product = products[i];
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
                        // tag�Ѿ���ȡ��������isRead����Ϊtrue
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

                // ��û�ж�ȡ����tag����ʾ��ǰ��
                if (products != null)
                {
                    products = products.OrderBy(p => p.IsRead).ToList();
                    SetProducts(products);
                }

                txtMessage.Text = text;
                labTotalRead.Text = "��" + products.Where(p => p.IsRead).Count() + "��" + products.Count();
            });
        }

        private void readerService_IsConnectChanged(object sender, IsConnectChangedEventArgs e)
        {
            this.Invoke((EventHandler)delegate
             {
                 chkIsConnect.Checked = e.IsConnect;
                 chkIsConnect.Text = e.IsConnect ? "��" : "��";

             });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            rfidReaderService.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            rfidReaderService.Stop();
        }

    }
}
