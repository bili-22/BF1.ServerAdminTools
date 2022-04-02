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

            var temp = IniHelper.ReadString("Options", "AudioIndex", "", FileUtil.F_Settings_Path);
            if (!string.IsNullOrEmpty(temp))
                AudioUtil.ClickSoundIndex = Convert.ToInt32(temp);

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
            IniHelper.WriteString("Options", "AudioIndex", AudioUtil.ClickSoundIndex.ToString(), FileUtil.F_Settings_Path);
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
    }
}
