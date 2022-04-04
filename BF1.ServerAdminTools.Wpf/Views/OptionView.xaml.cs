using BF1.ServerAdminTools.BF1API.Core;
using BF1.ServerAdminTools.Common;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Wpf.Utils;

namespace BF1.ServerAdminTools.Wpf.Views
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
            if (!ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
            {
                MsgBoxUtil.ErrorMsgBox("没有检测到游戏进程");
                return;
            }

            if (!Memory.Initialize(CoreUtil.TargetAppName))
            {
                LoggerHelper.Error("战地1内存模块初始化失败");
                MsgBoxUtil.ErrorMsgBox("战地1内存模块初始化失败");
                return;
            }

            LoggerHelper.Info("战地1内存模块初始化成功");
            MsgBoxUtil.InformationMsgBox("检测到游戏运行");
        }
    }
}
