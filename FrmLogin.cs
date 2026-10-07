using System;
using System.Windows.Forms;

namespace WindowsFormsApp_1
{
    /// <summary>登录窗体：支持管理员和借阅人登录，以及借阅人自助注册</summary>
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            if (username.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("请输入账号和密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User user = LibraryStore.ValidateUser(username, password);
            if (user == null)
            {
                MessageBox.Show("账号或密码错误！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            LibraryStore.CurrentUser = user;
            this.DialogResult = DialogResult.OK;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (FrmReaderEdit dlg = new FrmReaderEdit(null, true))
            {
                dlg.Text = "读者注册";
                if (dlg.ShowDialog() == DialogResult.OK && dlg.NewUser != null)
                {
                    MessageBox.Show("注册成功，请使用新账号登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsername.Text = dlg.NewUser.Username;
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
        }
    }
}
