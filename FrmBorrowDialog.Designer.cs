namespace WindowsFormsApp_1
{
    partial class FrmBorrowDialog
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
            this.lblReader = new System.Windows.Forms.Label();
            this.cmbReader = new System.Windows.Forms.ComboBox();
            this.lblBook = new System.Windows.Forms.Label();
            this.cmbBook = new System.Windows.Forms.ComboBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblReader
            // 
            this.lblReader.Location = new System.Drawing.Point(25, 27);
            this.lblReader.Name = "lblReader";
            this.lblReader.Size = new System.Drawing.Size(82, 24);
            this.lblReader.TabIndex = 0;
            this.lblReader.Text = "选择读者：";
            this.lblReader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbReader
            // 
            this.cmbReader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReader.Location = new System.Drawing.Point(110, 24);
            this.cmbReader.Name = "cmbReader";
            this.cmbReader.Size = new System.Drawing.Size(285, 25);
            this.cmbReader.TabIndex = 0;
            // 
            // lblBook
            // 
            this.lblBook.Location = new System.Drawing.Point(25, 65);
            this.lblBook.Name = "lblBook";
            this.lblBook.Size = new System.Drawing.Size(82, 24);
            this.lblBook.TabIndex = 3;
            this.lblBook.Text = "选择图书：";
            this.lblBook.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbBook
            // 
            this.cmbBook.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBook.Location = new System.Drawing.Point(110, 62);
            this.cmbBook.Name = "cmbBook";
            this.cmbBook.Size = new System.Drawing.Size(285, 25);
            this.cmbBook.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblInfo.Location = new System.Drawing.Point(25, 102);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Padding = new System.Windows.Forms.Padding(10, 10, 6, 6);
            this.lblInfo.Size = new System.Drawing.Size(370, 88);
            this.lblInfo.TabIndex = 6;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(230, 208);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(84, 30);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确 定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(324, 208);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmBorrowDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(420, 252);
            this.Controls.Add(this.lblReader);
            this.Controls.Add(this.cmbReader);
            this.Controls.Add(this.lblBook);
            this.Controls.Add(this.cmbBook);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBorrowDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "办理借书";
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblReader;
        private System.Windows.Forms.ComboBox cmbReader;
        private System.Windows.Forms.Label lblBook;
        private System.Windows.Forms.ComboBox cmbBook;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
