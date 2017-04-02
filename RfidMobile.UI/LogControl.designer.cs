namespace RfidMobile.UI
{
    partial class LogControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpLogDate = new System.Windows.Forms.DateTimePicker();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgLogs = new System.Windows.Forms.DataGrid();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpLogDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 28);
            // 
            // dtpLogDate
            // 
            this.dtpLogDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtpLogDate.Location = new System.Drawing.Point(0, 0);
            this.dtpLogDate.Name = "dtpLogDate";
            this.dtpLogDate.Size = new System.Drawing.Size(220, 22);
            this.dtpLogDate.TabIndex = 0;
            this.dtpLogDate.ValueChanged += new System.EventHandler(this.dtpLogDate_ValueChanged);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLog.Location = new System.Drawing.Point(0, 144);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(220, 48);
            this.txtLog.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgLogs);
            this.panel2.Controls.Add(this.txtLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 192);
            // 
            // dgLogs
            // 
            this.dgLogs.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLogs.HeaderBackColor = System.Drawing.SystemColors.ControlDark;
            this.dgLogs.Location = new System.Drawing.Point(0, 0);
            this.dgLogs.Name = "dgLogs";
            this.dgLogs.RowHeadersVisible = false;
            this.dgLogs.Size = new System.Drawing.Size(220, 144);
            this.dgLogs.TabIndex = 1;
            this.dgLogs.CurrentCellChanged += new System.EventHandler(this.dgLogs_CurrentCellChanged);
            // 
            // LogControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "LogControl";
            this.Size = new System.Drawing.Size(220, 220);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.DateTimePicker dtpLogDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGrid dgLogs;
    }
}
