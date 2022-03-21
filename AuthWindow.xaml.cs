using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Features.API;
using BF1.ServerAdminTools.Features.API2;
using BF1.ServerAdminTools.Features.Core;
using BF1.ServerAdminTools.Features.Data;
using Chinese;
using RestSharp;

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
                LoggerHelper.Info("开始初始化程序...");
                Task.Delay(1000).Wait();

                UpdateState("正在为您营造个性化体验...");
                Task.Delay(500).Wait();

                // 初始化
                if (Memory.Initialize(CoreUtil.AppName))
                    LoggerHelper.Info("战地1内存模块初始化成功");
                else
                    LoggerHelper.Error("战地1内存模块初始化失败");

                BF1API.Init();
                LoggerHelper.Info("战地1API模块初始化成功");

                GTAPI.Init();
                LoggerHelper.Info("GameTools API模块初始化成功");

                ImageData.InitDict();
                LoggerHelper.Info("本地图片缓存数据初始化成功");

                ChineseConverter.ToTraditional("免费，跨平台，开源！");
                LoggerHelper.Info("简繁翻译库初始化成功");

                try
                {
                    UpdateState("正在验证玩家授权...");
                    Task.Delay(500).Wait();

                    var baseAddress = Player.GetLocalPlayer();
                    if (!Memory.IsValid(baseAddress))
                    {
                        UpdateState($"未获取到玩家数据，请稍后再试！程序即将关闭");
                        LoggerHelper.Error($"玩家基址读取失败");
                        Task.Delay(2000).Wait();

                        Application.Current.Dispatcher.BeginInvoke(() =>
                        {
                            Application.Current.Shutdown();
                        });
                    }
                    else
                    {
                        LoggerHelper.Info($"玩家基址读取成功 0x{baseAddress:x}");
                    }

                    // 带队标 0x2156，不带队标 0x40
                    var personaId = Memory.Read<long>(baseAddress + 0x38);
                    LoggerHelper.Info($"玩家数字ID {personaId}");
                    var offset = Memory.Read<long>(baseAddress + 0x18);
                    var playerName = Memory.ReadString(offset, 64);
                    LoggerHelper.Info($"玩家ID {playerName}");

                    var str = "https://api.battlefield.vip/bf1/checkauth";
                    var options = new RestClientOptions(str)
                    {
                        Timeout = 5000
                    };

                    LoggerHelper.Info($"正在验证玩家 {playerName} 授权");
                    var client = new RestClient(options);
                    var request = new RestRequest()
                        .AddQueryParameter("playername", playerName)
                        .AddQueryParameter("personaid", personaId);

                    var response = client.ExecutePostAsync(request).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        LoggerHelper.Info($"验证玩家授权成功");
                    }
                    else if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        UpdateState($"玩家 {playerName} 未授权！程序即将关闭");
                        LoggerHelper.Error($"玩家 {playerName} 未授权");
                        Task.Delay(2000).Wait();

                        Application.Current.Dispatcher.BeginInvoke(() =>
                        {
                            Application.Current.Shutdown();
                        });

                        return;
                    }
                    else
                    {
                        UpdateState("验证玩家授权失败！程序即将关闭");
                        LoggerHelper.Error($"验证玩家 {playerName} 授权失败");
                        Task.Delay(2000).Wait();

                        Application.Current.Dispatcher.BeginInvoke(() =>
                        {
                            Application.Current.Shutdown();
                        });

                        return;
                    }

                    ////////////////////////////////////////////////////////////////////

                    UpdateState("正在检测版本更新...");
                    LoggerHelper.Info($"正在检测版本更新...");
                    Task.Delay(500).Wait();

                    // 获取版本更新
                    var web = HttpHelper.HttpClientGET(CoreUtil.Version_Address).Result;
                    if (string.IsNullOrEmpty(web))
                    {
                        UpdateState("获取新版本信息失败！程序即将关闭");
                        LoggerHelper.Error($"获取新版本信息失败");
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
                        LoggerHelper.Error($"发现新版本");

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
                        UpdateState("连线中...");
                        LoggerHelper.Info($"当前已是最新版本");
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
                catch (Exception ex)
                {
                    UpdateState("发生了未知异常！程序即将关闭");
                    LoggerHelper.Error($"发生了未知异常", ex);
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
