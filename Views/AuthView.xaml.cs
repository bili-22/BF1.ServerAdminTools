using BF1.ServerAdminTools.Windows;
using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Features.API;
using BF1.ServerAdminTools.Features.API.RespJson;

namespace BF1.ServerAdminTools.Views
{
    /// <summary>
    /// AuthView.xaml 的交互逻辑
    /// </summary>
    public partial class AuthView : UserControl
    {
        private WebView2Window WebView2Window = null;

        public AuthView()
        {
            InitializeComponent();

            MainWindow.ClosingDisposeEvent += MainWindow_ClosingDisposeEvent;
        }

        private void MainWindow_ClosingDisposeEvent()
        {
            WebView2Window?.Close();
        }

        private void Button_GetPlayerSessionID_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (CoreUtil.IsWebView2DependencyInstalled())
            {
                if (WebView2Window == null)
                {
                    WebView2Window = new WebView2Window();
                    WebView2Window.Show();
                }
                else
                {
                    if (WebView2Window.IsVisible)
                    {
                        WebView2Window.Topmost = true;
                        WebView2Window.Topmost = false;
                        WebView2Window.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        WebView2Window = null;
                        WebView2Window = new WebView2Window();
                        WebView2Window.Show();
                    }
                }
            }
            else
            {
                MainWindow.dSetOperatingState(3, "未安装WebView2对应依赖！");
                return;
            }
        }

        private async void Button_VerifyPlayerKey_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (!string.IsNullOrEmpty(Globals.SessionId))
            {
                TextBlock_CheckSessionStatus.Text = "正在验证中，请等待...";
                TextBlock_CheckSessionStatus.Background = Brushes.Gray;
                MainWindow.dSetOperatingState(2, "正在验证中，请等待...");

                await BF1API.SetAPILocale();
                var result = await BF1API.GetWelcomeMessage();

                if (result.IsSuccess)
                {
                    var welcomeMsg = JsonUtil.JsonDese<WelcomeMsg>(result.Message);

                    TextBlock_CheckSessionStatus.Text = welcomeMsg.result.firstMessage;
                    TextBlock_CheckSessionStatus.Background = Brushes.Green;

                    MainWindow.dSetOperatingState(1, $"验证成功 {welcomeMsg.result.firstMessage}  |  耗时: {result.ExecTime:0.00} 秒");
                }
                else
                {
                    TextBlock_CheckSessionStatus.Text = "验证失败";
                    TextBlock_CheckSessionStatus.Background = Brushes.Red;

                    MainWindow.dSetOperatingState(3, $"验证失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
                }
            }
            else
            {
                MainWindow.dSetOperatingState(2, "请先获取玩家SessionID后，再执行本操作");
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ProcessUtil.OpenLink(e.Uri.OriginalString);
            e.Handled = true;
        }
    }
}
