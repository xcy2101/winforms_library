using System;
using System.Windows.Forms;

namespace WindowsFormsApp_1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// 登录成功进入主界面，主界面选择“退出登录”后返回登录界面，关闭窗体则退出程序。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LibraryStore.Load();

            while (true)
            {
                DialogResult loginResult;
                using (FrmLogin login = new FrmLogin())
                {
                    loginResult = login.ShowDialog();
                }
                if (loginResult != DialogResult.OK)
                {
                    break;
                }

                DialogResult mainResult;
                using (FrmMain main = new FrmMain())
                {
                    mainResult = main.ShowDialog();
                }
                if (mainResult != DialogResult.OK)
                {
                    break;
                }
                LibraryStore.CurrentUser = null; // 退出登录，返回登录界面
            }
        }
    }
}
