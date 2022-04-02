using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Wpf.Utils;
using Microsoft.Web.WebView2.Core;

namespace BF1.ServerAdminTools.Wpf.Windows
{
    /// <summary>
    /// WebView2Window.xaml 的交互逻辑
    /// </summary>
    public partial class WebView2Window : Window
    {
        private const string Uri = "https://companion-api.battlefield.com/companion/home";

        public WebView2Window()
        {
            InitializeComponent();
        }

        #region 加载与关闭
        private async void Window_WebView2_Loaded(object sender, RoutedEventArgs e)
        {
            // 刷新DNS缓存
            CoreUtil.FlushDNSCache();
            LoggerHelper.Info($"启动WebView2成功，已刷新DNS缓存");

            var env = await CoreWebView2Environment.CreateAsync(null, FileUtil.Cache, null);

            await WebView2.EnsureCoreWebView2Async(env);

            // 禁止dev菜单
            WebView2.CoreWebView2.Settings.AreDevToolsEnabled = false;
            // 禁止所有菜单
            WebView2.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            // 禁止缩放
            WebView2.CoreWebView2.Settings.IsZoomControlEnabled = false;
            // 禁止显示状态栏，鼠标悬浮在链接上时右下角没有url地址显示
            WebView2.CoreWebView2.Settings.IsStatusBarEnabled = false;

            // 新窗口打开页面的处理
            WebView2.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;

            WebView2.CoreWebView2.Navigate(Uri);
        }

        private void Window_WebView2_Closing(object sender, CancelEventArgs e)
        {
            WebView2.Dispose();
        }
        #endregion

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            var deferral = e.GetDeferral();
            e.NewWindow = WebView2.CoreWebView2;
            deferral.Complete();
        }

        private async void Button_GetPlayerAccountInfo_Click(object sender, RoutedEventArgs e)
        {
            // 获取remid、sid、sessionId
            var cookies = await WebView2.CoreWebView2.CookieManager.GetCookiesAsync(null);
            if (cookies != null && cookies.Count >= 3)
            {
                foreach (var item in cookies)
                {
                    if (item.Name == "remid")
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                            Globals.Remid = item.Value;
                        continue;
                    }

                    if (item.Name == "sid")
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                            Globals.Sid = item.Value;
                        continue;
                    }

                    if (item.Name == "gatewaySessionId")
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                            Globals.SessionId = item.Value;
                        continue;
                    }
                }

                if (MessageBox.Show($"成功获取到 玩家账号信息\n\n" +
                    $"remid\n{Globals.Remid}\n\n" +
                    $"sid\n{Globals.Sid}\n\n" +
                    $"sessionId\n{Globals.SessionId}\n\n" +
                    $"是否现在就关闭此窗口？",
                    "提示", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    LoggerHelper.Info($"成功获取到 Remid {Globals.Remid}");
                    LoggerHelper.Info($"成功获取到 Sid {Globals.Sid}");
                    LoggerHelper.Info($"成功获取到 SessionId {Globals.SessionId}");
                    this.Close();
                }
            }
            else
            {
                MsgBoxUtil.ErrorMsgBox("获取 玩家账号信息 失败，请重新登录网页后再次尝试");
            }
        }

        private async void Button_ClearCache_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("你确认要清空本地缓存吗，这一般会在 玩家账号信息 失效的情况下使用，你可能需要重新登录小帮手", "警告",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                await WebView2.CoreWebView2.ExecuteScriptAsync("localStorage.clear()");
                WebView2.CoreWebView2.CookieManager.DeleteAllCookies();

                WebView2.Reload();

                LoggerHelper.Info($"清空WebView2缓存成功");
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            WebView2.CoreWebView2.Navigate(e.Uri.OriginalString);
            LoggerHelper.Info($"导航到 {e.Uri.OriginalString} 成功");
        }
    }
}
