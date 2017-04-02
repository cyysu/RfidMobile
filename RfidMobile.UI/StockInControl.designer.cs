namespace RfidMobile.UI
{
    partial class StockInControl
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
            this.btnReadTag = new System.Windows.Forms.Button();
            this.btnReadStockIn = new System.Windows.Forms.Button();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.dgProducts = new System.Windows.Forms.DataGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReadTag);
            this.panel1.Controls.Add(this.btnReadStockIn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 39);
            // 
            // btnReadTag
            // 
            this.btnReadTag.Location = new System.Drawing.Point(94, 3);
            this.btnReadTag.Name = "btnReadTag";
            this.btnReadTag.Size = new System.Drawing.Size(85, 30);
            this.btnReadTag.TabIndex = 2;
            this.btnReadTag.Text = "读标签";
            this.btnReadTag.Click += new System.EventHandler(this.btnReadTag_Click);
            // 
            // btnReadStockIn
            // 
            this.btnReadStockIn.Location = new System.Drawing.Point(3, 3);
            this.btnReadStockIn.Name = "btnReadStockIn";
            this.btnReadStockIn.Size = new System.Drawing.Size(85, 30);
            this.btnReadStockIn.TabIndex = 0;
            this.btnReadStockIn.Text = "读入库";
            this.btnReadStockIn.Click += new System.EventHandler(this.btnReadStockIn_Click);
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSerialNumber.Location = new System.Drawing.Point(0, 0);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(200, 21);
            this.txtSerialNumber.TabIndex = 1;
            // 
            // dgProducts
            // 
            this.dgProducts.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgProducts.Location = new System.Drawing.Point(0, 21);
            this.dgProducts.Name = "dgProducts";
            this.dgProducts.RowHeadersVisible = false;
            this.dgProducts.Size = new System.Drawing.Size(200, 140);
            this.dgProducts.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgProducts);
            this.panel2.Controls.Add(this.txtSerialNumber);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 161);
            // 
            // StockInControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "StockInControl";
            this.Size = new System.Drawing.Size(200, 200);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Button btnReadStockIn;
        private System.Windows.Forms.DataGrid dgProducts;
        private System.Windows.Forms.Button btnReadTag;
        private System.Windows.Forms.Panel panel2;
    }
}
