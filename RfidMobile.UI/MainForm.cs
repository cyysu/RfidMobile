using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Symbol.RFID3;
using System.Net;
using RfidMobile.Service.Reader;
using RfidMobile.Service.Util;
using RfidMobile.Service.Product;
using RfidMobile.Service.StockIn;

namespace RfidMobile.UI
{
    /// <summary>
    /// ����������
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class MainForm : Form
    {
        private ReaderControl readerControl;
        private LogControl logControl;
        private StockInControl stockInControl;
        private ConfigControl configControl;

        // ��ǰ��������
        private RfidMobile.Service.Util.Config config;

        public MainForm()
        {
            InitializeComponent();

            try
            {
                config = ConfigService.Read();
                // ����log��ʾ�ĵȼ�
                LogService.Level = (LogType)Enum.Parse(typeof(LogType), config.LogLevel, true);
            }
            catch (Exception ex) {
                LogService.Error("main", ex.Message);
            }
        }

        #region show user control

        private void AddReaderControl(IList<Product> products)
        {
            if (readerControl == null)
            {
                readerControl = new ReaderControl(config, products);
                readerControl.Dock = System.Windows.Forms.DockStyle.Fill;
                readerControl.Location = new System.Drawing.Point(0, 0);
                readerControl.Name = "reader";
                Controls.Add(readerControl);
                readerControl.Start();
            }
        }

        private void AddReaderControl() {
            AddReaderControl(null);
        }

        private void AddLogControl() {
            if (logControl == null)
            {
                logControl = new LogControl();
                logControl.Dock = DockStyle.Fill;
                logControl.Name = "logger";
                Controls.Add(logControl);
            }
        }



        private void AddStockInControl() {
            if (stockInControl == null)
            {
                stockInControl = new StockInControl();
                stockInControl.Dock = DockStyle.Fill;
                stockInControl.Name = "stockIn";
                Controls.Add(stockInControl);

                stockInControl.ReadTagsClicked += new EventHandler<ReadTagsClickedEventArgs>(stockInControl_ReadTagsClicked);
            }
        }

        private void stockInControl_ReadTagsClicked(object sender, ReadTagsClickedEventArgs e)
        {
            RemoveAllControls();

            StockIn stockIn = e.StockIn;
            IList<Product> products = ProductService.GetProductsBuStockIn(stockIn);

            AddReaderControl(products);
        }

        private void AddConfigControl() {
            if (configControl == null) {
                configControl = new ConfigControl();
                configControl.Dock = DockStyle.Fill;
                configControl.Name = "config";
                Controls.Add(configControl);
            }
        }

        private void RemoveAllControls()
        {
            Controls.Remove(readerControl);
            if (readerControl != null) {
                readerControl.Stop();
            }
            readerControl = null;
            
            Controls.Remove(logControl);
            logControl = null;

            Controls.Remove(stockInControl);

            // ����ע��config control��read tags�����¼�
            try
            {
                stockInControl.ReadTagsClicked -= stockInControl_ReadTagsClicked;
            }
            // �������û��ע�����ע������ʧ�ܣ���Ӱ���������ʹ�ã��ʲ������쳣
            catch (Exception ex) { }

            stockInControl = null;

            Controls.Remove(configControl);
            configControl = null;
        }

        #endregion

        #region UI event

        private void menuReader_Click(object sender, EventArgs e)
        {
            RemoveAllControls();
            AddReaderControl();
        }

        private void menuStockIn_Click(object sender, EventArgs e)
        {
            RemoveAllControls();
            AddStockInControl();
        }

        private void menuLog_Click(object sender, EventArgs e)
        {
            RemoveAllControls();
            AddLogControl();
        }

        private void menuConfig_Click(object sender, EventArgs e)
        {
            RemoveAllControls();
            AddConfigControl();
        }

        #endregion
    }
}