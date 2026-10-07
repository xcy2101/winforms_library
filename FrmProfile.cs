using System;
using System.Windows.Forms;

namespace WindowsFormsApp_1
{
    /// <summary>修改个人信息（姓名、电话、邮箱）和登录密码</summary>
    public partial class FrmProfile : Form
    {
        private readonly User _user;

        public FrmProfile(User user)
        {
            InitializeComponent();
            _user = user;
            txtUsername.Text = _user.Username;
            txtName.Text = _user.Name;
            txtPhone.Text = _user.Phone;
            txtEmail.Text = _user.Email;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (name.Length == 0)
            {
                MessageBox.Show("姓名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            string pwd1 = txtPwd1.Text;
            string pwd2 = txtPwd2.Text;
            if (pwd1.Length > 0 && pwd1 != pwd2)
            {
                MessageBox.Show("两次输入的新密码不一致！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            _user.Name = name;
            _user.Phone = txtPhone.Text.Trim();
            _user.Email = txtEmail.Text.Trim();
            if (pwd1.Length > 0)
            {
                _user.Password = pwd1;
            }
            LibraryStore.Save();
            MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
