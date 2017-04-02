using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using RfidMobile.Service.Util;

namespace RfidMobile.UI
{
    public partial class LogControl : UserControl
    {
        private IList<RfidMobile.Service.Util.Log> logs;

        public LogControl()
        {
            InitializeComponent();
            getLogs();
        }

        private void dtpLogDate_ValueChanged(object sender, EventArgs e)
        {
            getLogs();
        }

        private void dgLogs_CurrentCellChanged(object sender, EventArgs e)
        {
            int row = dgLogs.CurrentCell.RowNumber;
            
            txtLog.Text = logs[row].DateTime.ToString("HH:mm:ss")+ "\r\n"+ logs[row].Message.ToString();
        }

        private void getLogs()
        {
            IList<RfidMobile.Service.Util.Log> logs = LogService.Read(dtpLogDate.Value);
            this.logs = logs;
            dgLogs.DataSource = logs;
        }
    }
}
