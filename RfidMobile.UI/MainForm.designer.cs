namespace RfidMobile.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuMain = new System.Windows.Forms.MenuItem();
            this.menuReader = new System.Windows.Forms.MenuItem();
            this.menuStockIn = new System.Windows.Forms.MenuItem();
            this.menuDebug = new System.Windows.Forms.MenuItem();
            this.menuLog = new System.Windows.Forms.MenuItem();
            this.menuConfig = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuMain);
            this.mainMenu1.MenuItems.Add(this.menuDebug);
            // 
            // menuMain
            // 
            this.menuMain.MenuItems.Add(this.menuReader);
            this.menuMain.MenuItems.Add(this.menuStockIn);
            this.menuMain.Text = "�˵�";
            // 
            // menuReader
            // 
            this.menuReader.Text = "��ȡ";
            this.menuReader.Click += new System.EventHandler(this.menuReader_Click);
            // 
            // menuStockIn
            // 
            this.menuStockIn.Text = "���";
            this.menuStockIn.Click += new System.EventHandler(this.menuStockIn_Click);
            // 
            // menuDebug
            // 
            this.menuDebug.MenuItems.Add(this.menuLog);
            this.menuDebug.MenuItems.Add(this.menuConfig);
            this.menuDebug.Text = "����";
            // 
            // menuLog
            // 
            this.menuLog.Text = "��־";
            this.menuLog.Click += new System.EventHandler(this.menuLog_Click);
            // 
            // menuConfig
            // 
            this.menuConfig.Text = "����";
            this.menuConfig.Click += new System.EventHandler(this.menuConfig_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "����";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuMain;
        private System.Windows.Forms.MenuItem menuDebug;
        private System.Windows.Forms.MenuItem menuReader;
        private System.Windows.Forms.MenuItem menuStockIn;
        private System.Windows.Forms.MenuItem menuLog;
        private System.Windows.Forms.MenuItem menuConfig;

    }
}

