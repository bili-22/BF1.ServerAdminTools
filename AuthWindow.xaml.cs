using BF1.ServerAdminTools.Windows;
using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Features.API;
using BF1.ServerAdminTools.Features.API2;
using BF1.ServerAdminTools.Features.Core;
using BF1.ServerAdminTools.Features.Chat;
using BF1.ServerAdminTools.Features.Data;
using Chinese;

namespace BF1.ServerAdminTools
{
    /// <summary>
    /// AuthWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Window_Auth_Loaded(object sender, RoutedEventArgs e)
        {
            // 客户端程序版本号
            TextBlock_VersionInfo.Text = CoreUtil.ClientVersionInfo.ToString();
            // 最后编译时间
            TextBlock_BuildDate.Text = CoreUtil.ClientBuildTime.ToString();

            Task.Run(() =>
            {
                try
                {
                    UpdateState("欢迎来到《BATTLEFIELD 1》...");

                    LoggerHelper.Info("开始初始化程序...");
                    LoggerHelper.Info($"当前程序版本号 {CoreUtil.ClientVersionInfo}");
                    LoggerHelper.Info($"当前程序最后编译时间 {CoreUtil.ClientBuildTime}");

                    CoreUtil.FlushDNSCache();
                    LoggerHelper.Info("刷新DNS缓存成功");

                    UpdateState("正在检测版本更新...");
                    LoggerHelper.Info($"正在检测版本更新...");

                    // 获取版本更新
                    var webConfig = HttpHelper.HttpClientGET(CoreUtil.Config_Address).Result;
                    if (string.IsNullOrEmpty(webConfig))
                    {
                        UpdateState("获取新版本信息失败！程序即将关闭");
                        LoggerHelper.Error($"获取新版本信息失败");
                        Task.Delay(2000).Wait();

                        this.Dispatcher.Invoke(() =>
                        {
                            Application.Current.Shutdown();
                        });

                        return;
                    }

                    var updateInfo = JsonUtil.JsonDese<UpdateInfo>(webConfig);

                    CoreUtil.ServerVersionInfo = new Version(updateInfo.Version);

                    if (CoreUtil.ServerVersionInfo > CoreUtil.ClientVersionInfo)
                    {
                        LoggerHelper.Info($"发现新版本 {CoreUtil.ServerVersionInfo}");

                        CoreUtil.Notice_Address = updateInfo.Address.Notice;
                        CoreUtil.Change_Address = updateInfo.Address.Change;

                        this.Dispatcher.Invoke(() =>
                        {
                            this.Hide();
                        });

                        if (MessageBox.Show($"检测到新版本已发布，是否立即前往更新？                                        " +
                            $"\n\n{updateInfo.Latest.Date}\n{updateInfo.Latest.Change}\n\n强烈建议大家使用最新版本！点否退出程序",
                            "发现新版本", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                var UpdateWindow = new UpdateWindow(updateInfo);
                                UpdateWindow.Owner = MainWindow.ThisMainWindow;
                                UpdateWindow.ShowDialog();

                                this.Close();
                            });
                        }
                        else
                        {
                            // 强制更新版本，否则退出程序
                            this.Dispatcher.Invoke(() =>
                            {
                                Application.Current.Shutdown();
                                return;
                            });
                        }
                    }
                    else
                    {
                        LoggerHelper.Info($"当前已是最新版本 {CoreUtil.ServerVersionInfo}");

                        UpdateState("正在为您营造个性化体验...");

                        // 检测目标程序有没有启动
                        if (!ProcessUtil.IsBf1Run())
                        {
                            UpdateState($"未发现《战地1》游戏进程！程序即将关闭");
                            LoggerHelper.Error("未发现战地1进程");
                            Task.Delay(2000).Wait();

                            this.Dispatcher.Invoke(() =>
                            {
                                Application.Current.Shutdown();
                                return;
                            });
                        }

                        // 初始化
                        if (Memory.Initialize(CoreUtil.TargetAppName))
                        {
                            LoggerHelper.Info("战地1内存模块初始化成功");
                        }
                        else
                        {
                            UpdateState($"战地1内存模块初始化失败！程序即将关闭");
                            LoggerHelper.Error("战地1内存模块初始化失败");
                            Task.Delay(2000).Wait();

                            this.Dispatcher.Invoke(() =>
                            {
                                Application.Current.Shutdown();
                                return;
                            });
                        }

                        BF1API.Init();
                        LoggerHelper.Info("战地1API模块初始化成功");

                        GTAPI.Init();
                        LoggerHelper.Info("GameToolsAPI模块初始化成功");

                        ImageData.InitDict();
                        LoggerHelper.Info("本地图片缓存库初始化成功");

                        ChineseConverter.ToTraditional("免费，跨平台，开源！");
                        LoggerHelper.Info("简繁翻译库初始化成功");

                        UpdateState("连线中...");

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
                        LoggerHelper.Info($"SQLite数据库初始化成功");

                        ChatMsg.AllocateMemory();
                        LoggerHelper.Info($"中文聊天指针分配成功 0x{ChatMsg.GetAllocateMemoryAddress():x}");

                        this.Dispatcher.Invoke(() =>
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            // 转移主程序控制权
                            Application.Current.MainWindow = mainWindow;
                            // 关闭初始化窗口
                            this.Close();
                        });
                    }
                }
                catch (Exception ex)
                {
                    UpdateState("发生了未知异常！程序即将关闭");
                    LoggerHelper.Error($"发生了未知异常", ex);
                    Task.Delay(2000).Wait();

                    this.Dispatcher.Invoke(() =>
                    {
                        Application.Current.Shutdown();
                    });
                }
            });
        }

        private void UpdateState(string msg)
        {
            this.Dispatcher.Invoke(() =>
            {
                TextBlock_State.Text = msg;
            });
        }
    }
}
