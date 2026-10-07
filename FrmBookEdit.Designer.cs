namespace WindowsFormsApp_1
{
    partial class FrmBookEdit
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblISBN = new System.Windows.Forms.Label();
            this.txtISBN = new System.Windows.Forms.TextBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.lblPublisher = new System.Windows.Forms.Label();
            this.txtPublisher = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.lblTotal = new System.Windows.Forms.Label();
            this.numTotal = new System.Windows.Forms.NumericUpDown();
            this.lblPublish = new System.Windows.Forms.Label();
            this.dtpPublish = new System.Windows.Forms.DateTimePicker();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(22, 26);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(84, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "书　名：";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(110, 23);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(285, 25);
            this.txtTitle.TabIndex = 0;
            // 
            // lblISBN
            // 
            this.lblISBN.Location = new System.Drawing.Point(22, 64);
            this.lblISBN.Name = "lblISBN";
            this.lblISBN.Size = new System.Drawing.Size(84, 24);
            this.lblISBN.TabIndex = 2;
            this.lblISBN.Text = "ISBN：";
            this.lblISBN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtISBN
            // 
            this.txtISBN.Location = new System.Drawing.Point(110, 61);
            this.txtISBN.Name = "txtISBN";
            this.txtISBN.Size = new System.Drawing.Size(285, 25);
            this.txtISBN.TabIndex = 1;
            // 
            // lblAuthor
            // 
            this.lblAuthor.Location = new System.Drawing.Point(22, 102);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(84, 24);
            this.lblAuthor.TabIndex = 4;
            this.lblAuthor.Text = "作　者：";
            this.lblAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(110, 99);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(285, 25);
            this.txtAuthor.TabIndex = 2;
            // 
            // lblPublisher
            // 
            this.lblPublisher.Location = new System.Drawing.Point(22, 140);
            this.lblPublisher.Name = "lblPublisher";
            this.lblPublisher.Size = new System.Drawing.Size(84, 24);
            this.lblPublisher.TabIndex = 6;
            this.lblPublisher.Text = "出版社：";
            this.lblPublisher.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPublisher
            // 
            this.txtPublisher.Location = new System.Drawing.Point(110, 137);
            this.txtPublisher.Name = "txtPublisher";
            this.txtPublisher.Size = new System.Drawing.Size(285, 25);
            this.txtPublisher.TabIndex = 3;
            // 
            // lblCategory
            // 
            this.lblCategory.Location = new System.Drawing.Point(22, 178);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(84, 24);
            this.lblCategory.TabIndex = 8;
            this.lblCategory.Text = "分　类：";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbCategory.Items.AddRange(new object[] {
            "文学",
            "科幻",
            "计算机",
            "历史",
            "科普",
            "经济管理",
            "教育",
            "艺术",
            "少儿",
            "其他"});
            this.cmbCategory.Location = new System.Drawing.Point(110, 175);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(285, 25);
            this.cmbCategory.TabIndex = 4;
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(22, 216);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(84, 24);
            this.lblPrice.TabIndex = 10;
            this.lblPrice.Text = "价格(元)：";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numPrice
            // 
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Location = new System.Drawing.Point(110, 213);
            this.numPrice.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(130, 25);
            this.numPrice.TabIndex = 5;
            // 
            // lblTotal
            // 
            this.lblTotal.Location = new System.Drawing.Point(22, 254);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(84, 24);
            this.lblTotal.TabIndex = 12;
            this.lblTotal.Text = "总数量(册)：";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numTotal
            // 
            this.numTotal.Location = new System.Drawing.Point(110, 251);
            this.numTotal.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numTotal.Name = "numTotal";
            this.numTotal.Size = new System.Drawing.Size(130, 25);
            this.numTotal.TabIndex = 6;
            this.numTotal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblPublish
            // 
            this.lblPublish.Location = new System.Drawing.Point(22, 292);
            this.lblPublish.Name = "lblPublish";
            this.lblPublish.Size = new System.Drawing.Size(84, 24);
            this.lblPublish.TabIndex = 14;
            this.lblPublish.Text = "出版日期：";
            this.lblPublish.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpPublish
            // 
            this.dtpPublish.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPublish.Location = new System.Drawing.Point(110, 289);
            this.dtpPublish.Name = "dtpPublish";
            this.dtpPublish.Size = new System.Drawing.Size(285, 25);
            this.dtpPublish.TabIndex = 7;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(232, 338);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 30);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "确 定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(322, 338);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(73, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmBookEdit
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(415, 383);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblISBN);
            this.Controls.Add(this.txtISBN);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.lblPublisher);
            this.Controls.Add(this.txtPublisher);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.numPrice);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.numTotal);
            this.Controls.Add(this.lblPublish);
            this.Controls.Add(this.dtpPublish);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBookEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "图书信息";
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotal)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblISBN;
        private System.Windows.Forms.TextBox txtISBN;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label lblPublisher;
        private System.Windows.Forms.TextBox txtPublisher;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.NumericUpDown numTotal;
        private System.Windows.Forms.Label lblPublish;
        private System.Windows.Forms.DateTimePicker dtpPublish;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
