using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using RfidMobile.Service.Util;

namespace RfidMobile.UI
{
    public partial class ConfigControl : UserControl
    {
        public ConfigControl()
        {
            InitializeComponent();

            RfidMobile.Service.Util.Config config = ConfigService.Read();
            txtHost.Text = config.Host;
            txtPort.Text = config.Port.ToString();
            txtLogLevel.Text = config.LogLevel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string host = txtHost.Text;
            int port = int.Parse( txtPort.Text);
            string logLevel = txtLogLevel.Text;
            RfidMobile.Service.Util.Config config = new RfidMobile.Service.Util.Config() { Host = host, Port = port , LogLevel = logLevel};
            ConfigService.Save(config);
        }
    }
}
