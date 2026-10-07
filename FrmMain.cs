using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp_1
{
    /// <summary>
    /// 图书管理系统主界面。
    /// 管理员端：统计总览、图书管理、读者管理、借阅管理、借阅历史；
    /// 读者端：图书查询、我的借阅、我的借阅历史。
    /// </summary>
    public partial class FrmMain : Form
    {
        // 管理员端控件
        private DataGridView dgvBooks;
        private DataGridView dgvReaders;
        private DataGridView dgvBorrow;
        private DataGridView dgvHistory;
        private DataGridView dgvCatStat;
        private TextBox txtBookSearch;
        private TextBox txtReaderSearch;
        private TextBox txtBorrowSearch;
        private TextBox txtHistorySearch;
        private ComboBox cmbBookCategory;
        private ComboBox cmbHistoryFilter;
        private CheckBox chkShowAll;

        // 读者端控件
        private DataGridView dgvBooks2;
        private DataGridView dgvMine;
        private DataGridView dgvMyHistory;
        private TextBox txtSearch2;
        private ComboBox cmbCategory2;
        private ComboBox cmbMyFilter;
        private Label lblMineCount;

        // 统计卡片数字标签
        private Label lblStatKinds;
        private Label lblStatCopies;
        private Label lblStatAvailable;
        private Label lblStatBorrowed;
        private Label lblStatReaders;
        private Label lblStatActive;
        private Label lblStatOverdue;
        private Label lblStatTotal;

        public FrmMain()
        {
            InitializeComponent();

            User me = LibraryStore.CurrentUser;
            bool admin = IsAdmin();
            this.Text = "图书管理系统 —— " + (admin ? "管理员端" : "读者端");
            lblWelcome.Text = string.Format("欢迎您：{0}（{1}）", me.Name, admin ? "图书管理员" : "借阅人");

            if (admin)
            {
                BuildAdminTabs();
            }
            else
            {
                BuildReaderTabs();
            }

            LoadCategoryFilters();
            RefreshAll();
            tabMain.SelectedIndexChanged += TabMain_SelectedIndexChanged;
        }

        private bool IsAdmin()
        {
            return LibraryStore.CurrentUser != null && LibraryStore.CurrentUser.Role == UserRole.Admin;
        }

        // ==================== 页签构建 ====================

        private void BuildAdminTabs()
        {
            BuildStatsTab();
            BuildBooksTab();
            BuildReadersTab();
            BuildBorrowTab();
            BuildHistoryTab();
        }

        private void BuildReaderTabs()
        {
            BuildSearchTab();
            BuildMineTab();
            BuildMyHistoryTab();
        }

        private TabPage AddTab(string title)
        {
            TabPage page = new TabPage(title);
            page.Padding = new Padding(6);
            tabMain.TabPages.Add(page);
            return page;
        }

        /// <summary>统计总览（管理员）</summary>
        private void BuildStatsTab()
        {
            TabPage page = AddTab("统计总览");

            TableLayoutPanel cards = new TableLayoutPanel();
            cards.Dock = DockStyle.Top;
            cards.Height = 132;
            cards.BackColor = Color.FromArgb(225, 233, 242);
            cards.ColumnCount = 4;
            cards.RowCount = 2;
            for (int i = 0; i < 4; i++) cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            for (int i = 0; i < 2; i++) cards.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));

            lblStatKinds = MakeStatCard(cards, 0, 0, "图书种类");
            lblStatCopies = MakeStatCard(cards, 0, 1, "馆藏总册数");
            lblStatAvailable = MakeStatCard(cards, 0, 2, "在库册数");
            lblStatBorrowed = MakeStatCard(cards, 0, 3, "已借出册数");
            lblStatReaders = MakeStatCard(cards, 1, 0, "读者人数");
            lblStatActive = MakeStatCard(cards, 1, 1, "当前未还记录");
            lblStatOverdue = MakeStatCard(cards, 1, 2, "逾期未还记录");
            lblStatTotal = MakeStatCard(cards, 1, 3, "累计借阅次数");

            dgvCatStat = CreateGrid();
            AddCol(dgvCatStat, "colCatName", "分类", 30);
            AddCol(dgvCatStat, "colCatKinds", "种类数", 15);
            AddCol(dgvCatStat, "colCatTotal", "总册数", 15);
            AddCol(dgvCatStat, "colCatAvailable", "在库册数", 20);
            AddCol(dgvCatStat, "colCatBorrowed", "借出册数", 20);

            page.Controls.Add(dgvCatStat);
            page.Controls.Add(cards);
            dgvCatStat.BringToFront();
        }

        /// <summary>图书管理（管理员）</summary>
        private void BuildBooksTab()
        {
            TabPage page = AddTab("图书管理");
            FlowLayoutPanel toolbar = MakeToolbar();

            txtBookSearch = MakeTextBox(150);
            cmbBookCategory = MakeCombo(110);
            cmbBookCategory.SelectedIndexChanged += delegate { RefreshBooksGrid(); };

            Button btnSearch = MakeButton("查询", 72);
            btnSearch.Click += delegate { RefreshBooksGrid(); };
            Button btnAdd = MakeButton("添加图书", 92);
            btnAdd.Click += BtnBookAdd_Click;
            Button btnEdit = MakeButton("修 改", 72);
            btnEdit.Click += BtnBookEdit_Click;
            Button btnDelete = MakeButton("删 除", 72);
            btnDelete.Click += BtnBookDelete_Click;

            toolbar.Controls.Add(MakeCaption("关键字（书名/作者/ISBN/出版社）："));
            toolbar.Controls.Add(txtBookSearch);
            toolbar.Controls.Add(MakeCaption("分类："));
            toolbar.Controls.Add(cmbBookCategory);
            toolbar.Controls.Add(btnSearch);
            toolbar.Controls.Add(MakeToolbarSpace());
            toolbar.Controls.Add(btnAdd);
            toolbar.Controls.Add(btnEdit);
            toolbar.Controls.Add(btnDelete);

            dgvBooks = CreateGrid();
            AddCol(dgvBooks, "colISBN", "ISBN", 16);
            AddCol(dgvBooks, "colTitle", "书名", 22);
            AddCol(dgvBooks, "colAuthor", "作者", 16);
            AddCol(dgvBooks, "colPublisher", "出版社", 16);
            AddCol(dgvBooks, "colCategory", "分类", 10);
            AddCol(dgvBooks, "colPrice", "价格(元)", 8);
            AddCol(dgvBooks, "colTotal", "总数量", 7);
            AddCol(dgvBooks, "colBorrowed", "已借出", 7);
            AddCol(dgvBooks, "colAvailable", "在库", 7);
            dgvBooks.CellDoubleClick += delegate(object s, DataGridViewCellEventArgs ev)
            {
                if (ev.RowIndex >= 0) BtnBookEdit_Click(null, null);
            };

            page.Controls.Add(toolbar);
            page.Controls.Add(dgvBooks);
            dgvBooks.BringToFront();
            WireEnter(txtBookSearch, RefreshBooksGrid);
        }

        /// <summary>读者管理（管理员）</summary>
        private void BuildReadersTab()
        {
            TabPage page = AddTab("读者管理");
            FlowLayoutPanel toolbar = MakeToolbar();

            txtReaderSearch = MakeTextBox(180);

            Button btnSearch = MakeButton("查询", 72);
            btnSearch.Click += delegate { RefreshReadersGrid(); };
            Button btnAdd = MakeButton("添加读者", 92);
            btnAdd.Click += BtnReaderAdd_Click;
            Button btnEdit = MakeButton("修 改", 72);
            btnEdit.Click += BtnReaderEdit_Click;
            Button btnDelete = MakeButton("删 除", 72);
            btnDelete.Click += BtnReaderDelete_Click;
            Button btnResetPwd = MakeButton("重置密码", 92);
            btnResetPwd.Click += BtnReaderResetPwd_Click;

            toolbar.Controls.Add(MakeCaption("关键字（账号/姓名/电话）："));
            toolbar.Controls.Add(txtReaderSearch);
            toolbar.Controls.Add(btnSearch);
            toolbar.Controls.Add(MakeToolbarSpace());
            toolbar.Controls.Add(btnAdd);
            toolbar.Controls.Add(btnEdit);
            toolbar.Controls.Add(btnDelete);
            toolbar.Controls.Add(btnResetPwd);

            dgvReaders = CreateGrid();
            AddCol(dgvReaders, "colUUsername", "账号", 14);
            AddCol(dgvReaders, "colUName", "姓名", 12);
            AddCol(dgvReaders, "colUPhone", "电话", 16);
            AddCol(dgvReaders, "colUEmail", "邮箱", 22);
            AddCol(dgvReaders, "colUMax", "最大可借", 10);
            AddCol(dgvReaders, "colUActive", "在借数量", 12);
            AddCol(dgvReaders, "colURegister", "注册日期", 14);
            dgvReaders.CellDoubleClick += delegate(object s, DataGridViewCellEventArgs ev)
            {
                if (ev.RowIndex >= 0) BtnReaderEdit_Click(null, null);
            };

            page.Controls.Add(toolbar);
            page.Controls.Add(dgvReaders);
            dgvReaders.BringToFront();
            WireEnter(txtReaderSearch, RefreshReadersGrid);
        }

        /// <summary>借阅管理（管理员）：办理借书、还书、续借</summary>
        private void BuildBorrowTab()
        {
            TabPage page = AddTab("借阅管理");
            FlowLayoutPanel toolbar = MakeToolbar();

            Button btnBorrow = MakeButton("办理借书", 96);
            btnBorrow.Click += BtnDoBorrow_Click;
            Button btnReturn = MakeButton("办理还书", 96);
            btnReturn.Click += BtnDoReturn_Click;
            Button btnRenew = MakeButton("续 借", 72);
            btnRenew.Click += BtnAdminRenew_Click;
            Button btnDetail = MakeButton("详 情", 72);
            btnDetail.Click += delegate { ShowRecordDetail(GetSelectedRecord(dgvBorrow)); };

            chkShowAll = new CheckBox();
            chkShowAll.Text = "显示全部记录（含已归还）";
            chkShowAll.AutoSize = true;
            chkShowAll.Margin = new Padding(10, 12, 0, 0);
            chkShowAll.CheckedChanged += delegate { RefreshBorrowGrid(); };

            txtBorrowSearch = MakeTextBox(150);

            toolbar.Controls.Add(btnBorrow);
            toolbar.Controls.Add(btnReturn);
            toolbar.Controls.Add(btnRenew);
            toolbar.Controls.Add(btnDetail);
            toolbar.Controls.Add(MakeToolbarSpace());
            toolbar.Controls.Add(chkShowAll);
            toolbar.Controls.Add(MakeCaption("搜索（读者/图书）："));
            toolbar.Controls.Add(txtBorrowSearch);

            dgvBorrow = CreateGrid();
            AddCol(dgvBorrow, "colRId", "借阅编号", 9);
            AddCol(dgvBorrow, "colRUser", "读者", 13);
            AddCol(dgvBorrow, "colRBook", "图书", 28);
            AddCol(dgvBorrow, "colRBorrow", "借出日期", 13);
            AddCol(dgvBorrow, "colRDue", "应还日期", 13);
            AddCol(dgvBorrow, "colRStatus", "状态", 15);
            AddCol(dgvBorrow, "colRRenew", "已续借", 9);
            dgvBorrow.CellDoubleClick += delegate(object s, DataGridViewCellEventArgs ev)
            {
                if (ev.RowIndex >= 0) ShowRecordDetail(GetSelectedRecord(dgvBorrow));
            };

            page.Controls.Add(toolbar);
            page.Controls.Add(dgvBorrow);
            dgvBorrow.BringToFront();
            WireEnter(txtBorrowSearch, RefreshBorrowGrid);
        }

        /// <summary>借阅历史（管理员）：全部读者的借阅记录</summary>
        private void BuildHistoryTab()
        {
            TabPage page = AddTab("借阅历史");
            FlowLayoutPanel toolbar = MakeToolbar();

            cmbHistoryFilter = MakeCombo(100);
            FillFilterCombo(cmbHistoryFilter);
            cmbHistoryFilter.SelectedIndexChanged += delegate { RefreshHistoryGrid(); };

            txtHistorySearch = MakeTextBox(180);

            Button btnSearch = MakeButton("查询", 72);
            btnSearch.Click += delegate { RefreshHistoryGrid(); };

            toolbar.Controls.Add(MakeCaption("记录筛选："));
            toolbar.Controls.Add(cmbHistoryFilter);
            toolbar.Controls.Add(MakeCaption("搜索（读者/图书）："));
            toolbar.Controls.Add(txtHistorySearch);
            toolbar.Controls.Add(btnSearch);

            dgvHistory = CreateGrid();
            AddCol(dgvHistory, "colHId", "借阅编号", 9);
            AddCol(dgvHistory, "colHUser", "读者", 13);
            AddCol(dgvHistory, "colHBook", "图书", 26);
            AddCol(dgvHistory, "colHBorrow", "借出日期", 13);
            AddCol(dgvHistory, "colHDue", "应还日期", 13);
            AddCol(dgvHistory, "colHReturn", "归还日期", 13);
            AddCol(dgvHistory, "colHStatus", "状态", 13);

            page.Controls.Add(toolbar);
            page.Controls.Add(dgvHistory);
            dgvHistory.BringToFront();
            WireEnter(txtHistorySearch, RefreshHistoryGrid);
        }

        /// <summary>图书查询与借阅（读者）</summary>
        private void BuildSearchTab()
        {
            TabPage page = AddTab("图书查询");
            FlowLayoutPanel toolbar = MakeToolbar();

            txtSearch2 = MakeTextBox(170);
            cmbCategory2 = MakeCombo(110);
            cmbCategory2.SelectedIndexChanged += delegate { RefreshBooks2Grid(); };

            Button btnSearch = MakeButton("查询", 72);
            btnSearch.Click += delegate { RefreshBooks2Grid(); };
            Button btnBorrow = MakeButton("借阅此书", 96);
            btnBorrow.Click += BtnBorrow2_Click;

            toolbar.Controls.Add(MakeCaption("关键字（书名/作者/ISBN/出版社）："));
            toolbar.Controls.Add(txtSearch2);
            toolbar.Controls.Add(MakeCaption("分类："));
            toolbar.Controls.Add(cmbCategory2);
            toolbar.Controls.Add(btnSearch);
            toolbar.Controls.Add(MakeToolbarSpace());
            toolbar.Controls.Add(btnBorrow);

            dgvBooks2 = CreateGrid();
            AddCol(dgvBooks2, "col2ISBN", "ISBN", 16);
            AddCol(dgvBooks2, "col2Title", "书名", 22);
            AddCol(dgvBooks2, "col2Author", "作者", 16);
            AddCol(dgvBooks2, "col2Publisher", "出版社", 16);
            AddCol(dgvBooks2, "col2Category", "分类", 10);
            AddCol(dgvBooks2, "col2Price", "价格(元)", 9);
            AddCol(dgvBooks2, "col2Available", "在库", 9);
            dgvBooks2.CellDoubleClick += delegate(object s, DataGridViewCellEventArgs ev)
            {
                if (ev.RowIndex >= 0) BtnBorrow2_Click(null, null);
            };

            page.Controls.Add(toolbar);
            page.Controls.Add(dgvBooks2);
            dgvBooks2.BringToFront();
            WireEnter(txtSearch2, RefreshBooks2Grid);
        }

        /// <summary>我的借阅（读者）：当前在借、续借、归还</summary>
        private void BuildMineTab()
        {
            TabPage page = AddTab("我的借阅");
            FlowLayoutPanel toolbar = MakeToolbar();

            Button btnRenew = MakeButton("续 借", 80);
            btnRenew.Click += BtnRenewMine_Click;
            Button btnReturn = MakeButton("归 还", 80);
            btnReturn.Click += BtnReturnMine_Click;
            Button btnDetail = MakeButton("详 情", 80);
            btnDetail.Click += delegate { ShowRecordDetail(GetSelectedRecord(dgvMine)); };

            lblMineCount = MakeCaption("");
            lblMineCount.ForeColor = Color.FromArgb(180, 60, 40);
            lblMineCount.Font = new Font("微软雅黑", 9F, FontStyle.Bold);

            toolbar.Controls.Add(btnRenew);
            toolbar.Controls.Add(btnReturn);
            toolbar.Controls.Add(btnDetail);
            toolbar.Controls.Add(MakeToolbarSpace());
            toolbar.Controls.Add(lblMineCount);

            dgvMine = CreateGrid();
            AddCol(dgvMine, "colMBook", "图书", 34);
            AddCol(dgvMine, "colMBorrow", "借出日期", 16);
            AddCol(dgvMine, "colMDue", "应还日期", 16);
            AddCol(dgvMine, "colMStatus", "状态", 20);
            AddCol(dgvMine, "colMRenew", "已续借", 14);
            dgvMine.CellDoubleClick += delegate(object s, DataGridViewCellEventArgs ev)
            {
                if (ev.RowIndex >= 0) ShowRecordDetail(GetSelectedRecord(dgvMine));
            };

            page.Controls.Add(toolbar);
            page.Controls.Add(dgvMine);
            dgvMine.BringToFront();
        }

        /// <summary>我的借阅历史（读者）</summary>
        private void BuildMyHistoryTab()
        {
            TabPage page = AddTab("借阅历史");
            FlowLayoutPanel toolbar = MakeToolbar();

            cmbMyFilter = MakeCombo(100);
            FillFilterCombo(cmbMyFilter);
            cmbMyFilter.SelectedIndexChanged += delegate { RefreshMyHistoryGrid(); };

            toolbar.Controls.Add(MakeCaption("记录筛选："));
            toolbar.Controls.Add(cmbMyFilter);

            dgvMyHistory = CreateGrid();
            AddCol(dgvMyHistory, "colMHId", "借阅编号", 10);
            AddCol(dgvMyHistory, "colMHBook", "图书", 30);
            AddCol(dgvMyHistory, "colMHBorrow", "借出日期", 15);
            AddCol(dgvMyHistory, "colMHDue", "应还日期", 15);
            AddCol(dgvMyHistory, "colMHReturn", "归还日期", 15);
            AddCol(dgvMyHistory, "colMHStatus", "状态", 15);

            page.Controls.Add(toolbar);
            page.Controls.Add(dgvMyHistory);
            dgvMyHistory.BringToFront();
        }

        // ==================== 数据刷新 ====================

        private void TabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void RefreshAll()
        {
            if (IsAdmin())
            {
                RefreshStats();
                RefreshBooksGrid();
                RefreshReadersGrid();
                RefreshBorrowGrid();
                RefreshHistoryGrid();
            }
            else
            {
                RefreshBooks2Grid();
                RefreshMineGrid();
                RefreshMyHistoryGrid();
            }
        }

        private void RefreshStats()
        {
            List<Book> books = LibraryStore.Data.Books;
            int totalCopies = books.Sum(b => b.TotalCount);
            int borrowedCopies = books.Sum(b => b.BorrowedCount);
            int readerCount = LibraryStore.Data.Users.Count(u => u.Role == UserRole.Borrower);
            int activeCount = LibraryStore.Data.Records.Count(r => r.ReturnDate == DateTime.MinValue);
            int overdueCount = LibraryStore.Data.Records.Count(r => LibraryStore.IsOverdue(r));

            lblStatKinds.Text = books.Count.ToString();
            lblStatCopies.Text = totalCopies.ToString();
            lblStatAvailable.Text = (totalCopies - borrowedCopies).ToString();
            lblStatBorrowed.Text = borrowedCopies.ToString();
            lblStatReaders.Text = readerCount.ToString();
            lblStatActive.Text = activeCount.ToString();
            lblStatOverdue.Text = overdueCount.ToString();
            lblStatTotal.Text = LibraryStore.Data.Records.Count.ToString();

            if (lblStatOverdue.Text != "0")
            {
                lblStatOverdue.ForeColor = Color.Firebrick;
            }

            if (dgvCatStat != null)
            {
                dgvCatStat.Rows.Clear();
                var groups = books
                    .GroupBy(b => string.IsNullOrEmpty(b.Category) ? "未分类" : b.Category)
                    .OrderBy(g => g.Key);
                foreach (var g in groups)
                {
                    int total = g.Sum(b => b.TotalCount);
                    int borrowed = g.Sum(b => b.BorrowedCount);
                    dgvCatStat.Rows.Add(g.Key, g.Count(), total, total - borrowed, borrowed);
                }
            }
        }

        private List<Book> FilterBooks(string keyword, string category)
        {
            List<Book> result = new List<Book>();
            foreach (Book b in LibraryStore.Data.Books)
            {
                if (keyword.Length > 0)
                {
                    bool hit = b.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                        || b.Author.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                        || b.ISBN.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                        || b.Publisher.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
                    if (!hit) continue;
                }
                string cat = string.IsNullOrEmpty(b.Category) ? "未分类" : b.Category;
                if (category != null && cat != category) continue;
                result.Add(b);
            }
            return result;
        }

        private void RefreshBooksGrid()
        {
            if (dgvBooks == null || txtBookSearch == null || cmbBookCategory == null) return;
            string keyword = txtBookSearch.Text.Trim();
            string category = cmbBookCategory.SelectedIndex > 0 ? cmbBookCategory.Text : null;
            dgvBooks.Rows.Clear();
            foreach (Book b in FilterBooks(keyword, category))
            {
                int available = LibraryStore.GetAvailableCount(b);
                int row = dgvBooks.Rows.Add(b.ISBN, b.Title, b.Author, b.Publisher, b.Category,
                    b.Price.ToString("F2"), b.TotalCount, b.BorrowedCount, available);
                dgvBooks.Rows[row].Tag = b;
                if (available <= 0)
                {
                    dgvBooks.Rows[row].Cells["colAvailable"].Style.ForeColor = Color.Firebrick;
                }
            }
        }

        private void RefreshReadersGrid()
        {
            if (dgvReaders == null || txtReaderSearch == null) return;
            string keyword = txtReaderSearch.Text.Trim();
            dgvReaders.Rows.Clear();
            foreach (User u in LibraryStore.Data.Users.Where(x => x.Role == UserRole.Borrower).OrderBy(x => x.Id))
            {
                if (keyword.Length > 0)
                {
                    bool hit = u.Username.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                        || u.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                        || (u.Phone ?? "").IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
                    if (!hit) continue;
                }
                int active = LibraryStore.GetActiveBorrowCount(u.Id);
                int row = dgvReaders.Rows.Add(u.Username, u.Name, u.Phone, u.Email,
                    u.MaxBorrowCount, active, FormatDate(u.RegisterDate));
                dgvReaders.Rows[row].Tag = u;
                if (active >= u.MaxBorrowCount)
                {
                    dgvReaders.Rows[row].Cells["colUActive"].Style.ForeColor = Color.Firebrick;
                }
            }
        }

        private void RefreshBorrowGrid()
        {
            if (dgvBorrow == null || txtBorrowSearch == null || chkShowAll == null) return;
            string keyword = txtBorrowSearch.Text.Trim();
            dgvBorrow.Rows.Clear();
            List<BorrowRecord> list = LibraryStore.Data.Records;
            if (!chkShowAll.Checked)
            {
                list = list.Where(r => r.ReturnDate == DateTime.MinValue).ToList();
            }
            foreach (BorrowRecord r in list.OrderByDescending(r => r.BorrowDate))
            {
                string userName = LibraryStore.GetUserDisplayName(r.UserId);
                string bookTitle = LibraryStore.GetBookDisplayName(r.BookId);
                if (keyword.Length > 0
                    && userName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) < 0
                    && bookTitle.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    continue;
                }
                int row = dgvBorrow.Rows.Add(r.Id, userName, bookTitle,
                    FormatDate(r.BorrowDate), FormatDate(r.DueDate),
                    LibraryStore.GetStatusText(r), r.RenewCount);
                dgvBorrow.Rows[row].Tag = r;
                if (LibraryStore.IsOverdue(r))
                {
                    dgvBorrow.Rows[row].Cells["colRStatus"].Style.ForeColor = Color.Firebrick;
                }
            }
        }

        private void RefreshHistoryGrid()
        {
            if (dgvHistory == null || txtHistorySearch == null || cmbHistoryFilter == null) return;
            string keyword = txtHistorySearch.Text.Trim();
            int filter = cmbHistoryFilter.SelectedIndex;
            dgvHistory.Rows.Clear();
            foreach (BorrowRecord r in LibraryStore.Data.Records.OrderByDescending(r => r.BorrowDate))
            {
                if (filter == 1 && r.ReturnDate != DateTime.MinValue) continue;
                if (filter == 2 && r.ReturnDate == DateTime.MinValue) continue;
                if (filter == 3 && !LibraryStore.IsOverdue(r)) continue;

                string userName = LibraryStore.GetUserDisplayName(r.UserId);
                string bookTitle = LibraryStore.GetBookDisplayName(r.BookId);
                if (keyword.Length > 0
                    && userName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) < 0
                    && bookTitle.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    continue;
                }
                int row = dgvHistory.Rows.Add(r.Id, userName, bookTitle,
                    FormatDate(r.BorrowDate), FormatDate(r.DueDate), FormatDate(r.ReturnDate),
                    LibraryStore.GetStatusText(r));
                dgvHistory.Rows[row].Tag = r;
                if (LibraryStore.IsOverdue(r))
                {
                    dgvHistory.Rows[row].Cells["colHStatus"].Style.ForeColor = Color.Firebrick;
                }
            }
        }

        private void RefreshBooks2Grid()
        {
            if (dgvBooks2 == null || txtSearch2 == null || cmbCategory2 == null) return;
            string keyword = txtSearch2.Text.Trim();
            string category = cmbCategory2.SelectedIndex > 0 ? cmbCategory2.Text : null;
            dgvBooks2.Rows.Clear();
            foreach (Book b in FilterBooks(keyword, category))
            {
                int available = LibraryStore.GetAvailableCount(b);
                int row = dgvBooks2.Rows.Add(b.ISBN, b.Title, b.Author, b.Publisher, b.Category,
                    b.Price.ToString("F2"), available);
                dgvBooks2.Rows[row].Tag = b;
                if (available <= 0)
                {
                    dgvBooks2.Rows[row].Cells["col2Available"].Style.ForeColor = Color.Firebrick;
                }
            }
        }

        private void RefreshMineGrid()
        {
            if (dgvMine == null || lblMineCount == null) return;
            User me = LibraryStore.CurrentUser;
            dgvMine.Rows.Clear();
            List<BorrowRecord> list = LibraryStore.GetActiveRecords(me.Id).OrderBy(r => r.DueDate).ToList();
            foreach (BorrowRecord r in list)
            {
                int row = dgvMine.Rows.Add(LibraryStore.GetBookDisplayName(r.BookId),
                    FormatDate(r.BorrowDate), FormatDate(r.DueDate),
                    LibraryStore.GetStatusText(r), r.RenewCount);
                dgvMine.Rows[row].Tag = r;
                if (LibraryStore.IsOverdue(r))
                {
                    dgvMine.Rows[row].Cells["colMStatus"].Style.ForeColor = Color.Firebrick;
                }
            }
            lblMineCount.Text = string.Format("当前在借 {0} 本，最多可借 {1} 本，还可借 {2} 本",
                list.Count, me.MaxBorrowCount, Math.Max(0, me.MaxBorrowCount - list.Count));
        }

        private void RefreshMyHistoryGrid()
        {
            if (dgvMyHistory == null || cmbMyFilter == null) return;
            int filter = cmbMyFilter.SelectedIndex;
            dgvMyHistory.Rows.Clear();
            List<BorrowRecord> list = LibraryStore.Data.Records
                .Where(r => r.UserId == LibraryStore.CurrentUser.Id)
                .OrderByDescending(r => r.BorrowDate)
                .ToList();
            foreach (BorrowRecord r in list)
            {
                if (filter == 1 && r.ReturnDate != DateTime.MinValue) continue;
                if (filter == 2 && r.ReturnDate == DateTime.MinValue) continue;
                if (filter == 3 && !LibraryStore.IsOverdue(r)) continue;

                int row = dgvMyHistory.Rows.Add(r.Id, LibraryStore.GetBookDisplayName(r.BookId),
                    FormatDate(r.BorrowDate), FormatDate(r.DueDate), FormatDate(r.ReturnDate),
                    LibraryStore.GetStatusText(r));
                dgvMyHistory.Rows[row].Tag = r;
                if (LibraryStore.IsOverdue(r))
                {
                    dgvMyHistory.Rows[row].Cells["colMHStatus"].Style.ForeColor = Color.Firebrick;
                }
            }
        }

        private void LoadCategoryFilters()
        {
            List<string> categories = LibraryStore.Data.Books
                .Select(b => string.IsNullOrEmpty(b.Category) ? "未分类" : b.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToList();
            FillCategoryCombo(cmbBookCategory, categories);
            FillCategoryCombo(cmbCategory2, categories);
        }

        private void FillCategoryCombo(ComboBox cmb, List<string> categories)
        {
            if (cmb == null) return;
            string selected = cmb.SelectedIndex > 0 ? cmb.Text : null;
            cmb.Items.Clear();
            cmb.Items.Add("全部分类");
            foreach (string c in categories)
            {
                cmb.Items.Add(c);
            }
            int index = 0;
            if (selected != null) index = cmb.Items.IndexOf(selected);
            cmb.SelectedIndex = index < 0 ? 0 : index;
        }

        private static void FillFilterCombo(ComboBox cmb)
        {
            cmb.Items.Clear();
            cmb.Items.Add("全部记录");
            cmb.Items.Add("未归还");
            cmb.Items.Add("已归还");
            cmb.Items.Add("逾期未还");
            cmb.SelectedIndex = 0;
        }

        // ==================== 管理员操作 ====================

        private void BtnBookAdd_Click(object sender, EventArgs e)
        {
            using (FrmBookEdit dlg = new FrmBookEdit())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK && dlg.ResultBook != null)
                {
                    LibraryStore.Data.Books.Add(dlg.ResultBook);
                    LibraryStore.Save();
                    LoadCategoryFilters();
                    RefreshAll();
                    MessageBox.Show("图书添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnBookEdit_Click(object sender, EventArgs e)
        {
            Book book = GetSelectedBook(dgvBooks);
            if (book == null)
            {
                MessageBox.Show("请先选择要修改的图书！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (FrmBookEdit dlg = new FrmBookEdit(book))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LibraryStore.Save();
                    LoadCategoryFilters();
                    RefreshAll();
                }
            }
        }

        private void BtnBookDelete_Click(object sender, EventArgs e)
        {
            Book book = GetSelectedBook(dgvBooks);
            if (book == null)
            {
                MessageBox.Show("请先选择要删除的图书！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (book.BorrowedCount > 0)
            {
                MessageBox.Show(string.Format("《{0}》尚有 {1} 册未归还，不能删除！", book.Title, book.BorrowedCount),
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("确定要删除《{0}》吗？", book.Title), "确认删除",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            LibraryStore.Data.Books.Remove(book);
            LibraryStore.Save();
            LoadCategoryFilters();
            RefreshAll();
            MessageBox.Show("图书删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnReaderAdd_Click(object sender, EventArgs e)
        {
            using (FrmReaderEdit dlg = new FrmReaderEdit(null, false))
            {
                dlg.Text = "添加读者";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    RefreshAll();
                    MessageBox.Show("读者添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnReaderEdit_Click(object sender, EventArgs e)
        {
            User user = GetSelectedUser(dgvReaders);
            if (user == null)
            {
                MessageBox.Show("请先选择要修改的读者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (FrmReaderEdit dlg = new FrmReaderEdit(user, false))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    RefreshAll();
                }
            }
        }

        private void BtnReaderDelete_Click(object sender, EventArgs e)
        {
            User user = GetSelectedUser(dgvReaders);
            if (user == null)
            {
                MessageBox.Show("请先选择要删除的读者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int active = LibraryStore.GetActiveBorrowCount(user.Id);
            if (active > 0)
            {
                MessageBox.Show(string.Format("读者【{0}】还有 {1} 本图书未归还，不能删除！", user.Name, active),
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("确定要删除读者【{0}（{1}）】吗？\n其历史借阅记录将保留。",
                user.Name, user.Username), "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            LibraryStore.Data.Users.Remove(user);
            LibraryStore.Save();
            RefreshAll();
            MessageBox.Show("读者删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnReaderResetPwd_Click(object sender, EventArgs e)
        {
            User user = GetSelectedUser(dgvReaders);
            if (user == null)
            {
                MessageBox.Show("请先选择要重置密码的读者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("确定将【{0}】的密码重置为 {1} 吗？", user.Name, LibraryStore.DefaultPassword),
                "确认操作", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            user.Password = LibraryStore.DefaultPassword;
            LibraryStore.Save();
            MessageBox.Show("密码已重置为 " + LibraryStore.DefaultPassword + "。", "提示",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDoBorrow_Click(object sender, EventArgs e)
        {
            using (FrmBorrowDialog dlg = new FrmBorrowDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    RefreshAll();
                }
            }
        }

        private void BtnDoReturn_Click(object sender, EventArgs e)
        {
            BorrowRecord record = GetSelectedRecord(dgvBorrow);
            if (record == null)
            {
                MessageBox.Show("请先选择一条借阅记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (record.ReturnDate != DateTime.MinValue)
            {
                MessageBox.Show("该记录已归还，无需重复操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("确认为读者【{0}】归还《{1}》吗？",
                LibraryStore.GetUserDisplayName(record.UserId), LibraryStore.GetBookDisplayName(record.BookId)),
                "确认归还", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            int overdueDays = LibraryStore.Return(record);
            string tip = overdueDays > 0
                ? string.Format("归还成功！该书已逾期 {0} 天。", overdueDays)
                : "归还成功！";
            MessageBox.Show(tip, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshAll();
        }

        private void BtnAdminRenew_Click(object sender, EventArgs e)
        {
            BorrowRecord record = GetSelectedRecord(dgvBorrow);
            if (record == null)
            {
                MessageBox.Show("请先选择一条借阅记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string err = LibraryStore.TryRenew(record);
            if (err != null)
            {
                MessageBox.Show(err, "无法续借", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show(string.Format("续借成功！新的应还日期：{0}", FormatDate(record.DueDate)),
                "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshAll();
        }

        // ==================== 读者操作 ====================

        private void BtnBorrow2_Click(object sender, EventArgs e)
        {
            Book book = GetSelectedBook(dgvBooks2);
            if (book == null)
            {
                MessageBox.Show("请先选择要借阅的图书！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (LibraryStore.GetAvailableCount(book) <= 0)
            {
                MessageBox.Show(string.Format("《{0}》当前已全部借出，请选择其他图书！", book.Title),
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("确定要借阅《{0}》吗？\n借期 {1} 天，到期请按时归还或续借。",
                book.Title, LibraryStore.BorrowDays), "借阅确认",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            string err = LibraryStore.TryBorrow(LibraryStore.CurrentUser, book);
            if (err != null)
            {
                MessageBox.Show(err, "无法借阅", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RefreshAll();
                return;
            }
            MessageBox.Show(string.Format("借阅成功！\n《{0}》应还日期：{1}", book.Title,
                DateTime.Now.AddDays(LibraryStore.BorrowDays).ToString("yyyy-MM-dd")),
                "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshAll();
        }

        private void BtnRenewMine_Click(object sender, EventArgs e)
        {
            BorrowRecord record = GetSelectedRecord(dgvMine);
            if (record == null)
            {
                MessageBox.Show("请先选择要续借的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string err = LibraryStore.TryRenew(record);
            if (err != null)
            {
                MessageBox.Show(err, "无法续借", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show(string.Format("续借成功！新的应还日期：{0}", FormatDate(record.DueDate)),
                "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshAll();
        }

        private void BtnReturnMine_Click(object sender, EventArgs e)
        {
            BorrowRecord record = GetSelectedRecord(dgvMine);
            if (record == null)
            {
                MessageBox.Show("请先选择要归还的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("确定要归还《{0}》吗？", LibraryStore.GetBookDisplayName(record.BookId)),
                "确认归还", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            int overdueDays = LibraryStore.Return(record);
            string tip = overdueDays > 0
                ? string.Format("归还成功！该书已逾期 {0} 天。", overdueDays)
                : "归还成功！感谢您的使用。";
            MessageBox.Show(tip, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshAll();
        }

        // ==================== 顶部按钮 ====================

        private void btnProfile_Click(object sender, EventArgs e)
        {
            using (FrmProfile dlg = new FrmProfile(LibraryStore.CurrentUser))
            {
                dlg.ShowDialog(this);
            }
            lblWelcome.Text = string.Format("欢迎您：{0}（{1}）",
                LibraryStore.CurrentUser.Name, IsAdmin() ? "图书管理员" : "借阅人");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出登录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            this.DialogResult = DialogResult.OK; // 关闭主界面，返回登录窗体
        }

        // ==================== 工具方法 ====================

        private void ShowRecordDetail(BorrowRecord record)
        {
            if (record == null)
            {
                MessageBox.Show("请先选择一条借阅记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string msg = string.Format(
                "借阅编号：{0}\n读者：{1}（{2}）\n图书：《{3}》\n借出日期：{4}\n应还日期：{5}\n归还日期：{6}\n续借次数：{7}\n当前状态：{8}",
                record.Id,
                LibraryStore.GetUserDisplayName(record.UserId),
                LibraryStore.GetUsernameById(record.UserId),
                LibraryStore.GetBookDisplayName(record.BookId),
                FormatDate(record.BorrowDate),
                FormatDate(record.DueDate),
                FormatDate(record.ReturnDate),
                record.RenewCount,
                LibraryStore.GetStatusText(record));
            MessageBox.Show(msg, "借阅记录详情", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private Book GetSelectedBook(DataGridView grid)
        {
            if (grid == null || grid.CurrentRow == null) return null;
            return grid.CurrentRow.Tag as Book;
        }

        private User GetSelectedUser(DataGridView grid)
        {
            if (grid == null || grid.CurrentRow == null) return null;
            return grid.CurrentRow.Tag as User;
        }

        private BorrowRecord GetSelectedRecord(DataGridView grid)
        {
            if (grid == null || grid.CurrentRow == null) return null;
            return grid.CurrentRow.Tag as BorrowRecord;
        }

        private static string FormatDate(DateTime date)
        {
            if (date == DateTime.MinValue) return "—";
            return date.ToString("yyyy-MM-dd");
        }

        private FlowLayoutPanel MakeToolbar()
        {
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Top;
            panel.Height = 48;
            panel.Padding = new Padding(8, 4, 8, 0);
            panel.BackColor = Color.FromArgb(244, 247, 251);
            return panel;
        }

        private Label MakeCaption(string text)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.AutoSize = true;
            lbl.Margin = new Padding(6, 15, 0, 0);
            return lbl;
        }

        private Button MakeButton(string text, int width)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(width, 28);
            btn.Margin = new Padding(6, 9, 0, 0);
            return btn;
        }

        private TextBox MakeTextBox(int width)
        {
            TextBox txt = new TextBox();
            txt.Width = width;
            txt.Margin = new Padding(2, 11, 0, 0);
            return txt;
        }

        private ComboBox MakeCombo(int width)
        {
            ComboBox cmb = new ComboBox();
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb.Width = width;
            cmb.Margin = new Padding(2, 11, 0, 0);
            return cmb;
        }

        private Control MakeToolbarSpace()
        {
            Label lbl = new Label();
            lbl.Size = new Size(8, 1);
            lbl.Margin = new Padding(0);
            return lbl;
        }

        private DataGridView CreateGrid()
        {
            DataGridView grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AllowUserToOrderColumns = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.GridColor = Color.Gainsboro;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 250, 253);
            return grid;
        }

        private void AddCol(DataGridView grid, string name, string header, int weight)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = name;
            col.HeaderText = header;
            col.FillWeight = weight;
            col.ReadOnly = true;
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            grid.Columns.Add(col);
        }

        private void WireEnter(TextBox box, Action refresh)
        {
            box.KeyDown += delegate(object s, KeyEventArgs ev)
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    refresh();
                    ev.SuppressKeyPress = true;
                }
            };
        }

        private Label MakeStatCard(TableLayoutPanel parent, int row, int col, string caption)
        {
            Panel card = new Panel();
            card.Dock = DockStyle.Fill;
            card.Margin = new Padding(6);
            card.BackColor = Color.White;

            Label lblNum = new Label();
            lblNum.Location = new Point(0, 6);
            lblNum.Size = new Size(120, 30);
            lblNum.TextAlign = ContentAlignment.MiddleCenter;
            lblNum.Font = new Font("微软雅黑", 14F, FontStyle.Bold);
            lblNum.ForeColor = Color.FromArgb(40, 90, 170);
            lblNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblNum.Text = "0";

            Label lblCap = new Label();
            lblCap.Location = new Point(0, 38);
            lblCap.Size = new Size(120, 18);
            lblCap.TextAlign = ContentAlignment.MiddleCenter;
            lblCap.ForeColor = Color.DimGray;
            lblCap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblCap.Text = caption;

            card.Controls.Add(lblNum);
            card.Controls.Add(lblCap);
            parent.Controls.Add(card, col, row);
            return lblNum;
        }
    }
}
