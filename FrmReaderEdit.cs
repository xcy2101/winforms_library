using System;
using System.Windows.Forms;

namespace WindowsFormsApp_1
{
    /// <summary>
    /// 读者添加 / 修改 / 注册对话框。
    /// user 为 null 时表示添加（或注册）新读者，否则为修改。
    /// </summary>
    public partial class FrmReaderEdit : Form
    {
        private readonly User _user;
        private readonly bool _lockMaxBorrow;

        /// <summary>添加模式下生成的新用户</summary>
        public User NewUser;

        public FrmReaderEdit(User user, bool lockMaxBorrow)
        {
            InitializeComponent();
            _user = user;
            _lockMaxBorrow = lockMaxBorrow;
            if (_user != null)
            {
                this.Text = "修改读者信息";
                txtUsername.Text = _user.Username;
                txtName.Text = _user.Name;
                txtPassword.Text = _user.Password;
                txtPhone.Text = _user.Phone;
                txtEmail.Text = _user.Email;
                numMax.Value = _user.MaxBorrowCount;
            }
            else
            {
                numMax.Value = 5;
                if (_lockMaxBorrow)
                {
                    numMax.Enabled = false;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string name = txtName.Text.Trim();
            string password = txtPassword.Text;
            if (username.Length == 0 || name.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("账号、姓名和密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            User existing = LibraryStore.FindUser(username);
            if (existing != null && existing != _user)
            {
                MessageBox.Show("账号【" + username + "】已存在，请更换！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            int maxBorrow = _lockMaxBorrow ? 5 : (int)numMax.Value;
            if (_user != null)
            {
                _user.Username = username;
                _user.Name = name;
                _user.Password = password;
                _user.Phone = txtPhone.Text.Trim();
                _user.Email = txtEmail.Text.Trim();
                _user.MaxBorrowCount = maxBorrow;
            }
            else
            {
                NewUser = new User();
                NewUser.Id = LibraryStore.Data.NextUserId++;
                NewUser.Username = username;
                NewUser.Name = name;
                NewUser.Password = password;
                NewUser.Role = UserRole.Borrower;
                NewUser.Phone = txtPhone.Text.Trim();
                NewUser.Email = txtEmail.Text.Trim();
                NewUser.MaxBorrowCount = maxBorrow;
                NewUser.RegisterDate = DateTime.Now;
                LibraryStore.Data.Users.Add(NewUser);
            }
            LibraryStore.Save();
        }
    }
}
