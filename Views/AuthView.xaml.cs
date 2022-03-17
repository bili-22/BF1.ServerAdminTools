using BF1.ServerAdminTools.Windows;
using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Features.API;
using BF1.ServerAdminTools.Features.API.RespJson;
using BF1.ServerAdminTools.Features.API2;
using BF1.ServerAdminTools.Features.API2.RespJson;
using BF1.ServerAdminTools.Features.Utils;
using ScottPlot;

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

            WpfPlot_Main1.Plot.Title("战地1 PC端 亚服周报", true, System.Drawing.Color.Black, 18, "微软雅黑");
            WpfPlot_Main2.Plot.Title("战地1 PC端 全服周报", true, System.Drawing.Color.Black, 18, "微软雅黑");

            UpdateWpfPlot(WpfPlot_Main1, "Asia");
            UpdateWpfPlot(WpfPlot_Main2, "ALL");

            WpfPlot_Main1.RightClicked -= WpfPlot_Main1.DefaultRightClickEvent;
            WpfPlot_Main2.RightClicked -= WpfPlot_Main2.DefaultRightClickEvent;

            WpfPlot_Main1.RightClicked += DeployCustomMenu1;
            WpfPlot_Main2.RightClicked += DeployCustomMenu2;
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

                    var msg = ChsUtil.ToSimplifiedChinese(welcomeMsg.result.firstMessage);

                    TextBlock_CheckSessionStatus.Text = msg;
                    TextBlock_CheckSessionStatus.Background = Brushes.Green;

                    MainWindow.dSetOperatingState(1, $"验证成功 {msg}  |  耗时: {result.ExecTime:0.00} 秒");
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

        private async void UpdateWpfPlot(WpfPlot wpfPlot, string region)
        {
            var result = await GTAPI.GetStatusArray("7", region);

            if (result.IsSuccess)
            {
                var statusArray = JsonUtil.JsonDese<StatusArray>(result.Message);

                int count = statusArray.timeStamps.Count;

                double[] dates = new double[count];
                for (int i = 0; i < count; i++)
                {
                    var d0 = Convert.ToDateTime(statusArray.timeStamps[i]).ToLocalTime();
                    dates[i] = d0.ToOADate();
                }

                double[] as_values = new double[count];
                for (int i = 0; i < count; i++)
                {
                    as_values[i] = statusArray.soldierAmount[i];
                }

                double[] cs_values = new double[count];
                for (int i = 0; i < count; i++)
                {
                    cs_values[i] = statusArray.communitySoldierAmount[i];
                }

                double[] ds_values = new double[count];
                for (int i = 0; i < count; i++)
                {
                    ds_values[i] = statusArray.diceSoldierAmount[i];
                }
                wpfPlot.Plot.Clear();

                wpfPlot.Plot.AxisAuto();
                wpfPlot.Plot.SetOuterViewLimits(dates[0], dates[count - 1], 0, as_values.Max() + 1000);

                wpfPlot.Plot.Style(ScottPlot.Style.Monospace);
                wpfPlot.Plot.Palette = Palette.Category10;

                wpfPlot.Plot.XAxis.Label("时间日期", System.Drawing.Color.Black, 14, false, "微软雅黑");
                wpfPlot.Plot.YAxis.Label("在线人数", System.Drawing.Color.Black, 14, false, "微软雅黑");

                wpfPlot.Plot.AddScatterLines(dates, as_values, System.Drawing.Color.Blue, 2, LineStyle.Solid, "所有玩家");
                wpfPlot.Plot.AddScatterLines(dates, cs_values, System.Drawing.Color.Green, 3, LineStyle.Solid, "私服玩家");
                wpfPlot.Plot.AddScatterLines(dates, ds_values, System.Drawing.Color.Red, 1, LineStyle.Solid, "官服玩家");

                wpfPlot.Plot.Legend(location: Alignment.UpperRight);

                wpfPlot.Plot.XAxis.DateTimeFormat(true);
                wpfPlot.Plot.XAxis.TickLabelFormat("MM.dd HH:mm", true);

                // 定义刻度间隔
                //wpfPlot.Plot.XAxis.ManualTickSpacing(6, ScottPlot.Ticks.DateTimeUnit.Hour);
                wpfPlot.Plot.XAxis.TickLabelStyle(rotation: 45);

                // 为旋转的刻度添加额外的空间
                //wpfPlot.Plot.XAxis.SetSizeLimit(min: 25);

                wpfPlot.Refresh();

                MainWindow.dSetOperatingState(1, $"获取战地1玩家人数成功  |  耗时: {result.ExecTime:0.00} 秒");
            }
            else
            {
                MainWindow.dSetOperatingState(3, $"获取战地1玩家人数失败  |  耗时: {result.ExecTime:0.00} 秒");
            }
        }

        private void DeployCustomMenu1(object sender, EventArgs e)
        {
            MenuItem updateDataMenuItem = new MenuItem() { Header = "更新表格数据" };
            updateDataMenuItem.Click += UpdateData1;

            MenuItem defaultViewMenuItem = new MenuItem() { Header = "恢复默认视图" };
            defaultViewMenuItem.Click += DefaultView1;

            ContextMenu rightClickMenu = new ContextMenu();
            rightClickMenu.Items.Add(updateDataMenuItem);
            rightClickMenu.Items.Add(defaultViewMenuItem);

            rightClickMenu.IsOpen = true;
        }

        private void UpdateData1(object sender, RoutedEventArgs e)
        {
            MainWindow.dSetOperatingState(2, $"正在更新表格数据中...");
            UpdateWpfPlot(WpfPlot_Main1, "Asia");
        }

        private void DefaultView1(object sender, RoutedEventArgs e)
        {
            WpfPlot_Main1.Plot.AxisAuto();
            WpfPlot_Main1.Refresh();
        }

        private void DeployCustomMenu2(object sender, EventArgs e)
        {
            MenuItem updateDataMenuItem = new MenuItem() { Header = "更新表格数据" };
            updateDataMenuItem.Click += UpdateData2;

            MenuItem defaultViewMenuItem = new MenuItem() { Header = "恢复默认视图" };
            defaultViewMenuItem.Click += DefaultView2;

            ContextMenu rightClickMenu = new ContextMenu();
            rightClickMenu.Items.Add(updateDataMenuItem);
            rightClickMenu.Items.Add(defaultViewMenuItem);

            rightClickMenu.IsOpen = true;
        }

        private void UpdateData2(object sender, RoutedEventArgs e)
        {
            MainWindow.dSetOperatingState(2, $"正在更新表格数据中...");
            UpdateWpfPlot(WpfPlot_Main2, "ALL");
        }

        private void DefaultView2(object sender, RoutedEventArgs e)
        {
            WpfPlot_Main1.Plot.AxisAuto();
            WpfPlot_Main1.Refresh();
        }
    }
}
