using System;
using System.Windows.Forms;

namespace WindowsFormsApp_1
{
    /// <summary>图书添加 / 修改对话框</summary>
    public partial class FrmBookEdit : Form
    {
        private readonly Book _book;

        /// <summary>添加模式下生成的新图书</summary>
        public Book ResultBook;

        public FrmBookEdit()
            : this(null)
        {
        }

        public FrmBookEdit(Book book)
        {
            InitializeComponent();
            _book = book;
            if (_book != null)
            {
                this.Text = "修改图书";
                txtTitle.Text = _book.Title;
                txtISBN.Text = _book.ISBN;
                txtAuthor.Text = _book.Author;
                txtPublisher.Text = _book.Publisher;
                cmbCategory.Text = _book.Category;
                numPrice.Value = _book.Price;
                numTotal.Value = _book.TotalCount;
                dtpPublish.Value = _book.PublishDate < dtpPublish.MinDate ? dtpPublish.MinDate : _book.PublishDate;
            }
            else
            {
                this.Text = "添加图书";
                dtpPublish.Value = DateTime.Now.AddYears(-1);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            if (title.Length == 0)
            {
                MessageBox.Show("请输入书名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            int total = (int)numTotal.Value;
            if (_book != null && total < _book.BorrowedCount)
            {
                MessageBox.Show(string.Format("总数量不能小于已借出数量（当前已借出 {0} 册）！", _book.BorrowedCount),
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            if (_book != null)
            {
                _book.Title = title;
                _book.ISBN = txtISBN.Text.Trim();
                _book.Author = txtAuthor.Text.Trim();
                _book.Publisher = txtPublisher.Text.Trim();
                _book.Category = cmbCategory.Text.Trim();
                _book.Price = numPrice.Value;
                _book.TotalCount = total;
                _book.PublishDate = dtpPublish.Value.Date;
            }
            else
            {
                ResultBook = new Book();
                ResultBook.Id = LibraryStore.Data.NextBookId++;
                ResultBook.Title = title;
                ResultBook.ISBN = txtISBN.Text.Trim();
                ResultBook.Author = txtAuthor.Text.Trim();
                ResultBook.Publisher = txtPublisher.Text.Trim();
                ResultBook.Category = cmbCategory.Text.Trim();
                ResultBook.Price = numPrice.Value;
                ResultBook.TotalCount = total;
                ResultBook.BorrowedCount = 0;
                ResultBook.PublishDate = dtpPublish.Value.Date;
            }
            LibraryStore.Save();
        }
    }
}
