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
    /// 程序主窗体
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class MainForm : Form
    {
        private ReaderControl readerControl;
        private LogControl logControl;
        private StockInControl stockInControl;
        private ConfigControl configControl;

        // 当前程序配置
        private RfidMobile.Service.Util.Config config;

        public MainForm()
        {
            InitializeComponent();

            try
            {
                config = ConfigService.Read();
                // 设置log显示的等级
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

            // 尝试注销config control的read tags监听事件
            try
            {
                stockInControl.ReadTagsClicked -= stockInControl_ReadTagsClicked;
            }
            // 但是如果没有注册过，注销可能失败，不影响程序正常使用，故不处理异常
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