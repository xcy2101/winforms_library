namespace WindowsFormsApp_1
{
    partial class FrmProfile
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
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPwd1 = new System.Windows.Forms.Label();
            this.txtPwd1 = new System.Windows.Forms.TextBox();
            this.lblPwd2 = new System.Windows.Forms.Label();
            this.txtPwd2 = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.Location = new System.Drawing.Point(22, 26);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(96, 24);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "账　号：";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtUsername.Location = new System.Drawing.Point(122, 23);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.ReadOnly = true;
            this.txtUsername.Size = new System.Drawing.Size(273, 25);
            this.txtUsername.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(22, 64);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(96, 24);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "姓　名：";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(122, 61);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(273, 25);
            this.txtName.TabIndex = 1;
            // 
            // lblPhone
            // 
            this.lblPhone.Location = new System.Drawing.Point(22, 102);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(96, 24);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "电　话：";
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(122, 99);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(273, 25);
            this.txtPhone.TabIndex = 2;
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(22, 140);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(96, 24);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "邮　箱：";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(122, 137);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(273, 25);
            this.txtEmail.TabIndex = 3;
            // 
            // lblPwd1
            // 
            this.lblPwd1.Location = new System.Drawing.Point(22, 178);
            this.lblPwd1.Name = "lblPwd1";
            this.lblPwd1.Size = new System.Drawing.Size(96, 24);
            this.lblPwd1.TabIndex = 8;
            this.lblPwd1.Text = "新密码：";
            this.lblPwd1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPwd1
            // 
            this.txtPwd1.Location = new System.Drawing.Point(122, 175);
            this.txtPwd1.Name = "txtPwd1";
            this.txtPwd1.PasswordChar = '●';
            this.txtPwd1.Size = new System.Drawing.Size(273, 25);
            this.txtPwd1.TabIndex = 4;
            // 
            // lblPwd2
            // 
            this.lblPwd2.Location = new System.Drawing.Point(22, 216);
            this.lblPwd2.Name = "lblPwd2";
            this.lblPwd2.Size = new System.Drawing.Size(96, 24);
            this.lblPwd2.TabIndex = 10;
            this.lblPwd2.Text = "确认密码：";
            this.lblPwd2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPwd2
            // 
            this.txtPwd2.Location = new System.Drawing.Point(122, 213);
            this.txtPwd2.Name = "txtPwd2";
            this.txtPwd2.PasswordChar = '●';
            this.txtPwd2.Size = new System.Drawing.Size(273, 25);
            this.txtPwd2.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(232, 262);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 30);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "保 存";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(322, 262);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(73, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmProfile
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(415, 307);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPwd1);
            this.Controls.Add(this.txtPwd1);
            this.Controls.Add(this.lblPwd2);
            this.Controls.Add(this.txtPwd2);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改个人信息";
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPwd1;
        private System.Windows.Forms.TextBox txtPwd1;
        private System.Windows.Forms.Label lblPwd2;
        private System.Windows.Forms.TextBox txtPwd2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
