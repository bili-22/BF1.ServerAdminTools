using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Netty;

namespace BF1.ServerAdminTools.Common.Views
{
    /// <summary>
    /// OptionView.xaml 的交互逻辑
    /// </summary>
    public partial class OptionView : UserControl
    {
        public OptionView()
        {
            InitializeComponent();

            AudioUtil.ClickSoundIndex = Globals.Config.AudioIndex;

            switch (AudioUtil.ClickSoundIndex)
            {
                case 0:
                    RadioButton_ClickAudioSelect0.IsChecked = true;
                    break;
                case 1:
                    RadioButton_ClickAudioSelect1.IsChecked = true;
                    break;
                case 2:
                    RadioButton_ClickAudioSelect2.IsChecked = true;
                    break;
                case 3:
                    RadioButton_ClickAudioSelect3.IsChecked = true;
                    break;
                case 4:
                    RadioButton_ClickAudioSelect4.IsChecked = true;
                    break;
                case 5:
                    RadioButton_ClickAudioSelect5.IsChecked = true;
                    break;
            }

            MainWindow.ClosingDisposeEvent += MainWindow_ClosingDisposeEvent;

            var obj = NettyCore.GetConfig();
            Server_Port.Text = obj.Port.ToString();
            Server_Key.Text = obj.ServerKey.ToString();
            AutoRun.IsChecked = obj.AutoRun;
            if (obj.AutoRun)
            {
                try
                {
                    NettyCore.StartServer();
                    Button_Server.Content = "关闭";
                }
                catch (Exception ex)
                {
                    Core.LogError("Netty服务器启动出错", ex);
                    MsgBoxUtil.ErrorMsgBox("Netty服务器启动出错", ex);
                }
            }
        }

        private void MainWindow_ClosingDisposeEvent()
        {
            Globals.Config.AudioIndex = AudioUtil.ClickSoundIndex;
        }

        private void RadioButton_ClickAudioSelect_Click(object sender, RoutedEventArgs e)
        {
            string str = (sender as RadioButton).Content.ToString();

            switch (str)
            {
                case "无":
                    AudioUtil.ClickSoundIndex = 0;
                    break;
                case "提示音1":
                    AudioUtil.ClickSoundIndex = 1;
                    AudioUtil.ClickSound();
                    break;
                case "提示音2":
                    AudioUtil.ClickSoundIndex = 2;
                    AudioUtil.ClickSound();
                    break;
                case "提示音3":
                    AudioUtil.ClickSoundIndex = 3;
                    AudioUtil.ClickSound();
                    break;
                case "提示音4":
                    AudioUtil.ClickSoundIndex = 4;
                    AudioUtil.ClickSound();
                    break;
                case "提示音5":
                    AudioUtil.ClickSoundIndex = 5;
                    AudioUtil.ClickSound();
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Core.IsGameRun())
            {
                MsgBoxUtil.ErrorMsgBox("没有检测到游戏进程");
                return;
            }

            if (!Core.HookInit())
            {
                Core.LogError("战地1内存模块初始化失败");
                MsgBoxUtil.ErrorMsgBox("战地1内存模块初始化失败");
                return;
            }

            Core.LogInfo("战地1内存模块初始化成功");
            MsgBoxUtil.InformationMsgBox("检测到游戏运行");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Server_Port.Text))
            {
                MsgBoxUtil.ErrorMsgBox("端口号为空");
                return;
            }
            if (!int.TryParse(Server_Port.Text, out var port))
            {
                MsgBoxUtil.ErrorMsgBox("端口号错误");
                return;
            }
            if (string.IsNullOrWhiteSpace(Server_Key.Text))
            {
                MsgBoxUtil.ErrorMsgBox("服务器密钥为空");
                return;
            }
            if (!long.TryParse(Server_Key.Text, out var key))
            {
                MsgBoxUtil.ErrorMsgBox("服务器密钥错误");
                return;
            }

            NettyCore.SetConfig(new ConfigNettyObj
            {
                Port = port,
                ServerKey = key,
                AutoRun = AutoRun.IsChecked == true
            });
            MainWindow._SetOperatingState(1, "设置成功");
            if (NettyCore.State)
                try
                {
                    NettyCore.StopServer();
                }
                catch (Exception ex)
                {
                    Core.LogError("Netty服务器关闭出错", ex);
                    MsgBoxUtil.ErrorMsgBox("Netty服务器关闭出错", ex);
                }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (NettyCore.State)
            {
                try
                {
                    NettyCore.StopServer();
                    Button_Server.Content = "启动";
                }
                catch (Exception ex)
                {
                    Core.LogError("Netty服务器关闭出错", ex);
                    MsgBoxUtil.ErrorMsgBox("Netty服务器关闭出错", ex);
                }
            }
            else
            {
                try
                {
                    NettyCore.StartServer();
                    Button_Server.Content = "关闭";
                }
                catch (Exception ex)
                {
                    Core.LogError("Netty服务器启动出错", ex);
                    MsgBoxUtil.ErrorMsgBox("Netty服务器启动出错", ex);
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var file = FileSelectUtil.FileSelectPic();
            if (file == null)
                return;

            DataSave.Config.Bg = file;
            ConfigUtil.SaveConfig();

            MainWindow.BG();
        }
    }
}
