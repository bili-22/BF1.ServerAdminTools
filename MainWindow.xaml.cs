using BF1.ServerAdminTools.Views;
using BF1.ServerAdminTools.Models;
using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Features.Core;
using BF1.ServerAdminTools.Features.Chat;

namespace BF1.ServerAdminTools
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void DSetOperatingState(int index, string str);
        public static DSetOperatingState _dSetOperatingState;

        public delegate void ClosingDispose();
        public static event ClosingDispose ClosingDisposeEvent;

        public delegate void DTabControlSelect();
        public static DTabControlSelect _dTabControlSelect;

        public static MainWindow ThisMainWindow;

        public MainModel MainModel { get; set; }

        // 声明一个变量，用于存储软件开始运行的时间
        private DateTime Origin_DateTime;

        ///////////////////////////////////////////////////////

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Main_Loaded(object sender, RoutedEventArgs e)
        {
            // 提示信息委托
            _dSetOperatingState = SetOperatingState;

            _dTabControlSelect = TabControlSelect;

            MainModel = new MainModel();

            MainModel.AppRunTime = "运行时间 : Loading...";

            ThisMainWindow = this;

            ////////////////////////////////

            Title = CoreUtil.WindowTitle + CoreUtil.LocalVersionInfo + " - 最后编译时间 : " + File.GetLastWriteTime(Process.GetCurrentProcess().MainModule.FileName);

            // 获取当前时间，存储到对于变量中
            Origin_DateTime = DateTime.Now;

            ////////////////////////////////

            var thread0 = new Thread(UpdateState);
            thread0.IsBackground = true;
            thread0.Start();

            var therad1 = new Thread(InitThread);
            therad1.IsBackground = true;
            therad1.Start();

            this.DataContext = this;

            try
            {
                // 创建文件夹
                Directory.CreateDirectory(FileUtil.D_Admin_Path);
                Directory.CreateDirectory(FileUtil.D_Cache_Path);
                Directory.CreateDirectory(FileUtil.D_Config_Path);
                Directory.CreateDirectory(FileUtil.D_DB_Path);
                Directory.CreateDirectory(FileUtil.D_Log_Path);

                // 创建ini文件
                if (!File.Exists(FileUtil.F_Settings_Path))
                    File.Create(FileUtil.F_Settings_Path).Close();

                // 创建txt文件
                if (!File.Exists(FileUtil.F_WeaponList_Path))
                    File.Create(FileUtil.F_WeaponList_Path).Close();
                if (!File.Exists(FileUtil.F_BlackList_Path))
                    File.Create(FileUtil.F_BlackList_Path).Close();
                if (!File.Exists(FileUtil.F_WhiteList_Path))
                    File.Create(FileUtil.F_WhiteList_Path).Close();

                SQLiteHelper.Initialize();

                ChatMsg.AllocateMemory();
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }
        }

        private void Window_Main_Closing(object sender, CancelEventArgs e)
        {
            // 关闭事件
            ClosingDisposeEvent();

            // 写入SessionId
            IniHelper.WriteString("Globals", "SessionId", Globals.SessionId, FileUtil.F_Settings_Path);
            // 写入Remid
            IniHelper.WriteString("Globals", "Remid", Globals.Remid, FileUtil.F_Settings_Path);
            // 写入Sid
            IniHelper.WriteString("Globals", "Sid", Globals.Sid, FileUtil.F_Settings_Path);

            SQLiteHelper.CloseConnection();

            ChatMsg.FreeMemory();
            Memory.CloseHandle();

            Application.Current.Shutdown();
        }

        private void InitThread()
        {
            // 读取SessionId
            Globals.SessionId = IniHelper.ReadString("Globals", "SessionId", "", FileUtil.F_Settings_Path);
            // 读取Remid
            Globals.Remid = IniHelper.ReadString("Globals", "Remid", "", FileUtil.F_Settings_Path);
            // 读取Sid
            Globals.Sid = IniHelper.ReadString("Globals", "Sid", "", FileUtil.F_Settings_Path);

            // 调用加载更新
            AuthView._dAutoRefreshSID();
        }

        private void UpdateState()
        {
            while (true)
            {
                // 获取软件运行时间
                MainModel.AppRunTime = "运行时间 : " + CoreUtil.ExecDateDiff(Origin_DateTime, DateTime.Now);

                if (!ProcessUtil.IsAppRun(CoreUtil.AppName))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.Close();
                    });
                    return;
                }

                Thread.Sleep(1000);
            }
        }

        #region 常用方法
        /// <summary>
        /// 提示信息，绿色信息1，灰色警告2，红色错误3
        /// </summary>
        /// <param name="index">绿色信息1，灰色警告2，红色错误3</param>
        /// <param name="str">消息内容</param>
        private void SetOperatingState(int index, string str)
        {
            if (index == 1)
            {
                Border_OperateState.Background = Brushes.Green;
                TextBlock_OperateState.Text = $"信息 : {str}";
            }
            else if (index == 2)
            {
                Border_OperateState.Background = Brushes.Gray;
                TextBlock_OperateState.Text = $"警告 : {str}";
            }
            else if (index == 3)
            {
                Border_OperateState.Background = Brushes.Red;
                TextBlock_OperateState.Text = $"错误 : {str}";
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ProcessUtil.OpenLink(e.Uri.OriginalString);
            e.Handled = true;
        }
        #endregion

        ///////////////////////////////////////////////////////

        private void TabControlSelect()
        {
            TabControl_Main.SelectedIndex = 1;
        }
    }
}
