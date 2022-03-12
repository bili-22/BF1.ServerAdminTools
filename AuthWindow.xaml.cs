using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Features.API;
using BF1.ServerAdminTools.Features.API2;
using BF1.ServerAdminTools.Features.Core;
using BF1.ServerAdminTools.Features.Data;

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
            Task.Run(() =>
            {
                UpdateState("欢迎来到《BATTLEFIELD 1》...");

                Task.Delay(500).Wait();

                try
                {
                    // 获取版本更新
                    var web = HttpHelper.HttpClientGET(CoreUtil.Version_Address).Result;
                    if (string.IsNullOrEmpty(web))
                    {
                        UpdateState("获取新版本信息失败！程序即将关闭");

                        Task.Delay(2000).Wait();

                        Application.Current.Dispatcher.BeginInvoke(() =>
                        {
                            Application.Current.Shutdown();
                        });

                        return;
                    }

                    Version webV = new Version(web);
                    Version locV = new Version(CoreUtil.LocalVersionInfo);

                    if (webV > locV)
                    {
                        Application.Current.Dispatcher.BeginInvoke(() =>
                        {
                            this.Hide();
                        });

                        if (MessageBox.Show($"检测到新版本已发布，是否立即前往更新？",
                            "发现新版本", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            ProcessUtil.OpenLink(CoreUtil.Download_Address);
                            Application.Current.Shutdown();
                        }
                        else
                        {
                            Application.Current.Shutdown();
                        }
                    }
                    else
                    {
                        UpdateState("正在为您营造个性化体验...");

                        // 初始化
                        Memory.Initialize(CoreUtil.AppName);
                        BF1API.Init();
                        GTAPI.Init();
                        ImageData.InitDict();

                        Task.Delay(500).Wait();

                        UpdateState("连线中...");

                        Task.Delay(500).Wait();

                        Application.Current.Dispatcher.BeginInvoke(() =>
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            Application.Current.MainWindow = mainWindow;

                            this.Close();
                        });
                    }
                }
                catch (Exception)
                {
                    UpdateState("发生了未知异常！程序即将关闭");

                    Task.Delay(2000).Wait();

                    Application.Current.Dispatcher.BeginInvoke(() =>
                    {
                        Application.Current.Shutdown();
                    });
                }
            });
        }

        private void UpdateState(string msg)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                TextBlock_State.Text = msg;
            });
        }
    }
}
