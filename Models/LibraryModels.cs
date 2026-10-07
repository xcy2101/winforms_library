using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsApp_1
{
    /// <summary>用户角色</summary>
    public enum UserRole
    {
        /// <summary>图书管理员</summary>
        Admin = 0,
        /// <summary>借阅人</summary>
        Borrower = 1
    }

    /// <summary>系统用户（图书管理员 / 借阅人）</summary>
    public class User
    {
        public int Id;
        public string Username;
        public string Password;
        public string Name;
        public UserRole Role;
        public string Phone;
        public string Email;
        /// <summary>借阅人最大可借数量（本）</summary>
        public int MaxBorrowCount;
        public DateTime RegisterDate;
    }

    /// <summary>图书信息</summary>
    public class Book
    {
        public int Id;
        public string ISBN;
        public string Title;
        public string Author;
        public string Publisher;
        public string Category;
        public decimal Price;
        /// <summary>馆藏总数量（册）</summary>
        public int TotalCount;
        /// <summary>已借出数量（册）</summary>
        public int BorrowedCount;
        public DateTime PublishDate;
    }

    /// <summary>借阅记录。ReturnDate 等于 DateTime.MinValue 表示尚未归还。</summary>
    public class BorrowRecord
    {
        public int Id;
        public int BookId;
        public int UserId;
        public DateTime BorrowDate;
        public DateTime DueDate;
        public DateTime ReturnDate;
        public int RenewCount;
    }

    /// <summary>系统全部数据（持久化到 XML 文件）</summary>
    public class LibraryData
    {
        public List<User> Users = new List<User>();
        public List<Book> Books = new List<Book>();
        public List<BorrowRecord> Records = new List<BorrowRecord>();
        public int NextUserId = 1;
        public int NextBookId = 1;
        public int NextRecordId = 1;
    }

    /// <summary>图书数据存取与业务规则（XML 文件存储，无需数据库）</summary>
    public static class LibraryStore
    {
        /// <summary>借期（天）</summary>
        public const int BorrowDays = 30;
        /// <summary>每本书最多续借次数</summary>
        public const int MaxRenewTimes = 2;
        /// <summary>密码重置后的默认密码</summary>
        public const string DefaultPassword = "123456";

        public static LibraryData Data = new LibraryData();

        /// <summary>当前登录用户</summary>
        public static User CurrentUser;

        public static string FilePath
        {
            get { return Path.Combine(Application.StartupPath, "library_data.xml"); }
        }

        public static void Load()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(LibraryData));
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        LibraryData data = (LibraryData)serializer.Deserialize(reader);
                        if (data != null && data.Users != null && data.Books != null && data.Records != null)
                        {
                            Data = data;
                            return;
                        }
                    }
                }
            }
            catch
            {
                // 数据文件损坏或格式不兼容时重建示例数据
            }
            Data = CreateSeedData();
            Save();
        }

        public static void Save()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LibraryData));
                using (StreamWriter writer = new StreamWriter(FilePath, false, System.Text.Encoding.UTF8))
                {
                    serializer.Serialize(writer, Data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据保存失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>首次运行时生成的示例数据</summary>
        private static LibraryData CreateSeedData()
        {
            LibraryData data = new LibraryData();
            DateTime now = DateTime.Now;

            data.Users.Add(new User { Id = 1, Username = "admin", Password = "123456", Name = "系统管理员", Role = UserRole.Admin, Phone = "13800000001", Email = "admin@library.com", MaxBorrowCount = 0, RegisterDate = now.AddYears(-2) });
            data.Users.Add(new User { Id = 2, Username = "reader1", Password = "123456", Name = "张三", Role = UserRole.Borrower, Phone = "13900000002", Email = "zhangsan@example.com", MaxBorrowCount = 5, RegisterDate = now.AddYears(-1) });
            data.Users.Add(new User { Id = 3, Username = "reader2", Password = "123456", Name = "李四", Role = UserRole.Borrower, Phone = "13900000003", Email = "lisi@example.com", MaxBorrowCount = 5, RegisterDate = now.AddMonths(-8) });
            data.Users.Add(new User { Id = 4, Username = "reader3", Password = "123456", Name = "王五", Role = UserRole.Borrower, Phone = "13900000004", Email = "wangwu@example.com", MaxBorrowCount = 3, RegisterDate = now.AddMonths(-3) });
            data.NextUserId = 5;

            data.Books.Add(new Book { Id = 1, ISBN = "978-7-5366-9293-0", Title = "三体", Author = "刘慈欣", Publisher = "重庆出版社", Category = "科幻", Price = 23.00m, TotalCount = 3, BorrowedCount = 1, PublishDate = new DateTime(2008, 1, 1) });
            data.Books.Add(new Book { Id = 2, ISBN = "978-7-02-000220-7", Title = "红楼梦", Author = "曹雪芹", Publisher = "人民文学出版社", Category = "文学", Price = 59.70m, TotalCount = 5, BorrowedCount = 1, PublishDate = new DateTime(1996, 12, 1) });
            data.Books.Add(new Book { Id = 3, ISBN = "978-7-5442-7661-8", Title = "百年孤独", Author = "加西亚·马尔克斯", Publisher = "南海出版公司", Category = "文学", Price = 39.50m, TotalCount = 2, BorrowedCount = 0, PublishDate = new DateTime(2011, 6, 1) });
            data.Books.Add(new Book { Id = 4, ISBN = "978-7-302-32984-4", Title = "C#入门经典（第6版）", Author = "Karli Watson", Publisher = "清华大学出版社", Category = "计算机", Price = 108.00m, TotalCount = 4, BorrowedCount = 0, PublishDate = new DateTime(2014, 5, 1) });
            data.Books.Add(new Book { Id = 5, ISBN = "978-7-111-40701-0", Title = "算法导论", Author = "Thomas H.Cormen", Publisher = "机械工业出版社", Category = "计算机", Price = 128.00m, TotalCount = 3, BorrowedCount = 0, PublishDate = new DateTime(2013, 1, 1) });
            data.Books.Add(new Book { Id = 6, ISBN = "978-7-111-28067-2", Title = "数据库系统概念", Author = "Abraham Silberschatz", Publisher = "机械工业出版社", Category = "计算机", Price = 79.00m, TotalCount = 2, BorrowedCount = 0, PublishDate = new DateTime(2012, 3, 1) });
            data.Books.Add(new Book { Id = 7, ISBN = "978-7-5086-6314-7", Title = "人类简史", Author = "尤瓦尔·赫拉利", Publisher = "中信出版社", Category = "历史", Price = 68.00m, TotalCount = 3, BorrowedCount = 0, PublishDate = new DateTime(2014, 11, 1) });
            data.Books.Add(new Book { Id = 8, ISBN = "978-7-5063-5487-8", Title = "活着", Author = "余华", Publisher = "作家出版社", Category = "文学", Price = 28.00m, TotalCount = 4, BorrowedCount = 0, PublishDate = new DateTime(2012, 8, 1) });
            data.Books.Add(new Book { Id = 9, ISBN = "978-7-5357-3359-7", Title = "时间简史", Author = "史蒂芬·霍金", Publisher = "湖南科学技术出版社", Category = "科普", Price = 45.00m, TotalCount = 2, BorrowedCount = 0, PublishDate = new DateTime(2002, 1, 1) });
            data.Books.Add(new Book { Id = 10, ISBN = "978-7-02-002475-9", Title = "围城", Author = "钱钟书", Publisher = "人民文学出版社", Category = "文学", Price = 36.00m, TotalCount = 3, BorrowedCount = 0, PublishDate = new DateTime(1991, 2, 1) });
            data.NextBookId = 11;

            data.Records.Add(new BorrowRecord { Id = 1, BookId = 1, UserId = 2, BorrowDate = now.AddDays(-10), DueDate = now.AddDays(20), ReturnDate = DateTime.MinValue, RenewCount = 0 });
            data.Records.Add(new BorrowRecord { Id = 2, BookId = 2, UserId = 3, BorrowDate = now.AddDays(-40), DueDate = now.AddDays(-10), ReturnDate = DateTime.MinValue, RenewCount = 1 });
            data.Records.Add(new BorrowRecord { Id = 3, BookId = 8, UserId = 4, BorrowDate = now.AddDays(-60), DueDate = now.AddDays(-30), ReturnDate = now.AddDays(-30), RenewCount = 0 });
            data.Records.Add(new BorrowRecord { Id = 4, BookId = 9, UserId = 2, BorrowDate = now.AddDays(-90), DueDate = now.AddDays(-60), ReturnDate = now.AddDays(-58), RenewCount = 1 });
            data.NextRecordId = 5;

            return data;
        }

        // ==================== 查询辅助 ====================

        public static User FindUser(string username)
        {
            foreach (User u in Data.Users)
            {
                if (string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase))
                {
                    return u;
                }
            }
            return null;
        }

        public static User GetUserById(int id)
        {
            foreach (User u in Data.Users)
            {
                if (u.Id == id) return u;
            }
            return null;
        }

        public static Book FindBook(int id)
        {
            foreach (Book b in Data.Books)
            {
                if (b.Id == id) return b;
            }
            return null;
        }

        public static string GetUsernameById(int userId)
        {
            User u = GetUserById(userId);
            return u == null ? "（已注销）" : u.Username;
        }

        public static string GetUserDisplayName(int userId)
        {
            User u = GetUserById(userId);
            return u == null ? "（已注销用户）" : u.Name;
        }

        public static string GetBookDisplayName(int bookId)
        {
            Book b = FindBook(bookId);
            return b == null ? "（已删除图书）" : b.Title;
        }

        /// <summary>校验账号密码，成功返回用户，失败返回 null</summary>
        public static User ValidateUser(string username, string password)
        {
            User u = FindUser(username);
            if (u == null) return null;
            if (u.Password != password) return null;
            return u;
        }

        // ==================== 业务规则 ====================

        /// <summary>在库数量 = 总数量 - 已借出数量</summary>
        public static int GetAvailableCount(Book book)
        {
            return book.TotalCount - book.BorrowedCount;
        }

        public static List<BorrowRecord> GetActiveRecords(int userId)
        {
            List<BorrowRecord> list = new List<BorrowRecord>();
            foreach (BorrowRecord r in Data.Records)
            {
                if (r.UserId == userId && r.ReturnDate == DateTime.MinValue)
                {
                    list.Add(r);
                }
            }
            return list;
        }

        public static int GetActiveBorrowCount(int userId)
        {
            return GetActiveRecords(userId).Count;
        }

        public static bool HasActiveBorrow(int userId, int bookId)
        {
            foreach (BorrowRecord r in Data.Records)
            {
                if (r.UserId == userId && r.BookId == bookId && r.ReturnDate == DateTime.MinValue)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsOverdue(BorrowRecord record)
        {
            return record.ReturnDate == DateTime.MinValue && DateTime.Now > record.DueDate;
        }

        /// <summary>逾期天数（按自然日计算）</summary>
        public static int GetOverdueDays(BorrowRecord record)
        {
            DateTime endDate = record.ReturnDate == DateTime.MinValue ? DateTime.Now : record.ReturnDate;
            int days = (int)(endDate.Date - record.DueDate.Date).TotalDays;
            return days > 0 ? days : 0;
        }

        public static string GetStatusText(BorrowRecord record)
        {
            if (record.ReturnDate != DateTime.MinValue)
            {
                return GetOverdueDays(record) > 0 ? "已归还（曾逾期）" : "已归还";
            }
            int days = GetOverdueDays(record);
            if (days > 0)
            {
                return string.Format("逾期{0}天", days);
            }
            return "在借";
        }

        /// <summary>办理借书。校验通过返回 null 并生成记录，否则返回错误提示。</summary>
        public static string TryBorrow(User reader, Book book)
        {
            if (reader == null || book == null) return "请选择读者和图书！";
            if (reader.Role != UserRole.Borrower) return "只能为借阅人办理借书！";
            if (GetAvailableCount(book) <= 0) return string.Format("《{0}》当前已无库存！", book.Title);

            int active = GetActiveBorrowCount(reader.Id);
            if (active >= reader.MaxBorrowCount)
            {
                return string.Format("读者【{0}】在借 {1} 本，已达最大可借数量（{2} 本）！", reader.Name, active, reader.MaxBorrowCount);
            }
            if (HasActiveBorrow(reader.Id, book.Id))
            {
                return string.Format("读者【{0}】已借阅《{1}》且尚未归还，不能重复借阅！", reader.Name, book.Title);
            }

            BorrowRecord record = new BorrowRecord();
            record.Id = Data.NextRecordId++;
            record.BookId = book.Id;
            record.UserId = reader.Id;
            record.BorrowDate = DateTime.Now;
            record.DueDate = DateTime.Now.AddDays(BorrowDays);
            record.ReturnDate = DateTime.MinValue;
            record.RenewCount = 0;
            Data.Records.Add(record);
            book.BorrowedCount++;
            Save();
            return null;
        }

        /// <summary>办理还书，返回逾期天数（0 表示未逾期）</summary>
        public static int Return(BorrowRecord record)
        {
            record.ReturnDate = DateTime.Now;
            Book book = FindBook(record.BookId);
            if (book != null && book.BorrowedCount > 0)
            {
                book.BorrowedCount--;
            }
            Save();
            return GetOverdueDays(record);
        }

        /// <summary>续借。校验通过返回 null，否则返回错误提示。</summary>
        public static string TryRenew(BorrowRecord record)
        {
            if (record.ReturnDate != DateTime.MinValue) return "该记录已归还，无需续借！";
            if (IsOverdue(record)) return "该书已逾期，请先归还后再续借！";
            if (record.RenewCount >= MaxRenewTimes)
            {
                return string.Format("该书已续借 {0} 次，不能再续借！", record.RenewCount);
            }
            record.RenewCount++;
            record.DueDate = record.DueDate.AddDays(BorrowDays);
            Save();
            return null;
        }
    }
}
