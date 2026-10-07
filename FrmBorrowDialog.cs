using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp_1
{
    /// <summary>管理员办理借书对话框：选择读者和图书后完成借阅</summary>
    public partial class FrmBorrowDialog : Form
    {
        private class ComboItem
        {
            public string Text;
            public object Value;

            public override string ToString()
            {
                return Text;
            }
        }

        public FrmBorrowDialog()
        {
            InitializeComponent();

            foreach (User u in LibraryStore.Data.Users.Where(x => x.Role == UserRole.Borrower).OrderBy(x => x.Id))
            {
                cmbReader.Items.Add(new ComboItem
                {
                    Text = string.Format("{0}（{1}）", u.Name, u.Username),
                    Value = u
                });
            }
            foreach (Book b in LibraryStore.Data.Books
                .Where(x => LibraryStore.GetAvailableCount(x) > 0)
                .OrderBy(x => x.Title))
            {
                cmbBook.Items.Add(new ComboItem
                {
                    Text = string.Format("《{0}》（在库 {1} 册）", b.Title, LibraryStore.GetAvailableCount(b)),
                    Value = b
                });
            }

            cmbReader.SelectedIndexChanged += delegate { ShowInfo(); };
            cmbBook.SelectedIndexChanged += delegate { ShowInfo(); };
            if (cmbReader.Items.Count > 0) cmbReader.SelectedIndex = 0;
            if (cmbBook.Items.Count > 0) cmbBook.SelectedIndex = 0;
            ShowInfo();
        }

        private User SelectedReader
        {
            get { return GetSelected<User>(cmbReader); }
        }

        private Book SelectedBook
        {
            get { return GetSelected<Book>(cmbBook); }
        }

        private static T GetSelected<T>(ComboBox cmb) where T : class
        {
            ComboItem item = cmb.SelectedItem as ComboItem;
            return item == null ? null : item.Value as T;
        }

        private void ShowInfo()
        {
            User reader = SelectedReader;
            Book book = SelectedBook;
            if (reader == null || book == null)
            {
                lblInfo.Text = reader == null ? "暂无借阅人，请先添加读者。" : "暂无可借图书（全部借出）。";
                return;
            }
            int active = LibraryStore.GetActiveBorrowCount(reader.Id);
            int remain = Math.Max(0, reader.MaxBorrowCount - active);
            lblInfo.Text = string.Format(
                "读者：{0}（{1}）　在借 {2} 本，还可借 {3} 本\r\n图书：《{4}》　{5} 著　在库 {6} 册\r\n借期 {7} 天，预计应还日期：{8}",
                reader.Name, reader.Username, active, remain,
                book.Title, book.Author, LibraryStore.GetAvailableCount(book),
                LibraryStore.BorrowDays,
                DateTime.Now.AddDays(LibraryStore.BorrowDays).ToString("yyyy-MM-dd"));
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            User reader = SelectedReader;
            Book book = SelectedBook;
            if (reader == null || book == null)
            {
                MessageBox.Show("请选择读者和图书！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            string err = LibraryStore.TryBorrow(reader, book);
            if (err != null)
            {
                MessageBox.Show(err, "无法办理借书", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowInfo();
                this.DialogResult = DialogResult.None;
                return;
            }
            MessageBox.Show(string.Format("借书成功！\n《{0}》→ {1}\n应还日期：{2}",
                book.Title, reader.Name,
                DateTime.Now.AddDays(LibraryStore.BorrowDays).ToString("yyyy-MM-dd")),
                "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
